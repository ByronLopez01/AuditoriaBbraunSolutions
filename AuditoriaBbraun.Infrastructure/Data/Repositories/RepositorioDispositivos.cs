using AuditoriaBbraun.Domain.Entities;
using AuditoriaBbraun.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace AuditoriaBbraun.Infrastructure.Data.Repositories
{
    public class RepositorioDispositivos : IRepositorioDispositivos
    {
        private readonly ContextApplications _context;

        public RepositorioDispositivos(ContextApplications contexto)
        {
            _context = contexto ?? throw new ArgumentNullException(nameof(contexto));
        }

        public async Task<Dispositivo> ObtenerPorIdAsync(int id)
        {
            return await _context.Dispositivos.FindAsync(id);
        }

        public async Task<IEnumerable<Dispositivo>> ObtenerTodosAsync()
        {
            return await _context.Dispositivos.ToListAsync();
        }

        public async Task AgregarAsync(Dispositivo entidad)
        {
            await _context.Dispositivos.AddAsync(entidad);
        }

        public async Task ActualizarAsync(Dispositivo entidad)
        {
            _context.Dispositivos.Update(entidad);
            await Task.CompletedTask;
        }

        public async Task EliminarAsync(Dispositivo entidad)
        {
            _context.Dispositivos.Remove(entidad);
            await Task.CompletedTask;
        }

        public async Task<bool> ExisteAsync(int id)
        {
            return await _context.Dispositivos.AnyAsync(d => d.Id == id);
        }

        public async Task<Dispositivo> ObtenerPorNumeroSerieAsync(string numeroSerie)
        {
            return await _context.Dispositivos
                .FirstOrDefaultAsync(d => d.NumeroSerie == numeroSerie);
        }

        public async Task<IEnumerable<Dispositivo>> ObtenerActivosAsync()
        {
            return await _context.Dispositivos
                .Where(d => d.Activo)
                .ToListAsync();
        }

        public async Task<bool> ExisteNumeroSerieAsync(string numeroSerie)
        {
            return await _context.Dispositivos
                .AnyAsync(d => d.NumeroSerie == numeroSerie);
        }

        public Task<IEnumerable<Dispositivo>> ObtenerTodosAsync(Expression<Func<Dispositivo, bool>> filtro)
        {
            throw new NotImplementedException();
        }
    }
}
