using AuditoriaBbraun.Domain.Entities;
using AuditoriaBbraun.Domain.Enums;
using AuditoriaBbraun.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AuditoriaBbraun.Infrastructure.Data.Repositories
{
    public class RepositorioOrdenes : IRepositorioOrdenes
    {
        private readonly ContextApplications _context;

        public RepositorioOrdenes(ContextApplications contexto)
        {
            _context = contexto ?? throw new ArgumentNullException(nameof(contexto));
        }

        public async Task ActualizarAsync(Orden entidad)
        {
            if (entidad == null)
                throw new ArgumentNullException(nameof(entidad));

            entidad.FechaActualizacion = DateTime.UtcNow;
            _context.Ordenes.Update(entidad);
            await Task.CompletedTask;
        }

        public async Task AgregarAsync(Orden entidad)
        {
            if (entidad == null)
                throw new ArgumentNullException(nameof(entidad));

            // Asignar fecha de creación si no existe
            if (entidad.FechaCreacion == default)
                entidad.FechaCreacion = DateTime.UtcNow;

            await _context.Ordenes.AddAsync(entidad);
        }

        public async Task EliminarAsync(Orden entidad)
        {
            if (entidad == null)
                throw new ArgumentNullException(nameof(entidad));

            _context.Ordenes.Remove(entidad);
            await Task.CompletedTask;
        }

        public async Task<bool> ExisteAsync(int id)
        {
            return await _context.Ordenes.AnyAsync(o => o.Id == id);
        }

        public async Task<Orden> ObtenerConTransaccionesAsync(int ordenId)
        {
            // Versión simple por ahora (sin transacciones)
            return await _context.Ordenes.FindAsync(ordenId);
        }

        public async Task<IEnumerable<Orden>> ObtenerPorClienteAsync(int clienteId)
        {
            return await _context.Ordenes
                .Where(o => o.ClienteId == clienteId)
                .OrderByDescending(o => o.FechaCreacion)
                .ToListAsync();
        }

        public async Task<IEnumerable<Orden>> ObtenerPorEstadoAsync(EstadoOrden estado)
        {
            return await _context.Ordenes
               .Where(o => o.Estado == estado)
               .OrderByDescending(o => o.FechaCreacion)
               .ToListAsync();
        }

        public async Task<IEnumerable<Orden>> ObtenerPorFechaAsync(DateTime fecha)
        {
            return await _context.Ordenes
                .Where(o => o.FechaCreacion.Date == fecha.Date)
                .OrderByDescending(o => o.FechaCreacion)
                .ToListAsync();
        }

        public async Task<Orden> ObtenerPorIdAsync(int id)
        {
            return await _context.Ordenes.FindAsync(id);
        }

        public async Task<IEnumerable<Orden>> ObtenerTodosAsync()
        {
            return await _context.Ordenes
                .OrderByDescending(o => o.FechaCreacion)
                .ToListAsync();
        }

        public async Task<IEnumerable<Orden>> ObtenerTodosAsync(Expression<Func<Orden, bool>> filtro)
        {
            return await _context.Ordenes
               .Where(filtro)
               .OrderByDescending(o => o.FechaCreacion)
               .ToListAsync();
        }

        public async Task<bool> ExisteNumeroOrdenAsync(string numeroOrden)
        {
            return await _context.Ordenes
                .AnyAsync(o => o.NumeroOrden == numeroOrden);
        }

        public async Task ActualizarEstadoAsync(int ordenId, EstadoOrden nuevoEstado)
        {
            var orden = await _context.Ordenes.FindAsync(ordenId);
            if (orden != null)
            {
                orden.Estado = nuevoEstado;
                orden.FechaActualizacion = DateTime.UtcNow;
                _context.Ordenes.Update(orden);
            }
        }
    }
}
