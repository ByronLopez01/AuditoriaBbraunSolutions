using AuditoriaBbraun.Domain.Entities;
using AuditoriaBbraun.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuditoriaBbraun.Infrastructure.Data.Repositories
{
    public class RepositorioTransacciones : IRepositorioTransacciones
    {
        private readonly ContextApplications _context;

        public RepositorioTransacciones(ContextApplications contexto)
        {
            _context = contexto;
        }

        public Task<Transaccion> ObtenerPorIdAsync(int id)
        {
            var transaccion = _context.Transacciones.Find(id);
            return Task.FromResult(transaccion);
        }

        public Task<IEnumerable<Transaccion>> ObtenerTodosAsync()
        {
            var transacciones = _context.Transacciones.ToList();
            return Task.FromResult((IEnumerable<Transaccion>)transacciones);
        }

        public Task AgregarAsync(Transaccion entidad)
        {
            _context.Transacciones.Add(entidad);
            return Task.CompletedTask;
        }

        public Task ActualizarAsync(Transaccion entidad)
        {
            _context.Transacciones.Update(entidad);
            return Task.CompletedTask;
        }

        public Task EliminarAsync(Transaccion entidad)
        {
            _context.Transacciones.Remove(entidad);
            return Task.CompletedTask;
        }

        public Task<bool> ExisteAsync(int id)
        {
            var existe = _context.Transacciones.Any(t => t.Id == id);
            return Task.FromResult(existe);
        }

        // Implementación de métodos específicos
        public Task<IEnumerable<Transaccion>> ObtenerPorDispositivoAsync(string numeroSerie)
        {
            var transacciones = _context.Transacciones
                .Where(t => t.NumeroSerieDispositivo == numeroSerie)
                .OrderByDescending(t => t.FechaHora)
                .ToList();
            return Task.FromResult((IEnumerable<Transaccion>)transacciones);
        }

        public Task<IEnumerable<Transaccion>> ObtenerPorFechaAsync(DateTime fechaInicio, DateTime fechaFin)
        {
            var transacciones = _context.Transacciones
                .Where(t => t.FechaHora >= fechaInicio && t.FechaHora <= fechaFin)
                .OrderByDescending(t => t.FechaHora)
                .ToList();
            return Task.FromResult((IEnumerable<Transaccion>)transacciones);
        }

        public Task<Transaccion> ObtenerPorCodigoBarrasAsync(string codigoBarras)
        {
            var transaccion = _context.Transacciones
                .FirstOrDefault(t => t.CodigoBarras == codigoBarras);
            return Task.FromResult(transaccion);
        }

        // Implementar método de IRepositorio<T> que falta
        public Task<IEnumerable<Transaccion>> ObtenerTodosAsync(System.Linq.Expressions.Expression<Func<Transaccion, bool>> filtro)
        {
            var transacciones = _context.Transacciones.Where(filtro).ToList();
            return Task.FromResult((IEnumerable<Transaccion>)transacciones);
        }
    }
}
