using AuditoriaBbraun.Domain.Entities;
using AuditoriaBbraun.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;


namespace AuditoriaBbraun.Infrastructure.Data.Repositories
{
    public class RepositorioClientes : IRepositorioClientes
    {
        private readonly ContextApplications _context;

        public RepositorioClientes(ContextApplications contexto)
        {
            _context = contexto ?? throw new ArgumentNullException(nameof(contexto));
        }


        public Task<Cliente> ObtenerPorIdAsync(int id)
        {
            var cliente = _context.Clientes.Find(id);
            return Task.FromResult(cliente);
        }

        public Task<IEnumerable<Cliente>> ObtenerTodosAsync()
        {
            var clientes = _context.Clientes.ToList();
            return Task.FromResult((IEnumerable<Cliente>)clientes);
        }

        public Task AgregarAsync(Cliente entidad)
        {
            _context.Clientes.Add(entidad);
            return Task.CompletedTask;
        }

        public Task ActualizarAsync(Cliente entidad)
        {
            _context.Clientes.Update(entidad);
            return Task.CompletedTask;
        }

        public Task EliminarAsync(Cliente entidad)
        {
            _context.Clientes.Remove(entidad);
            return Task.CompletedTask;
        }

        public Task<bool> ExisteAsync(int id)
        {
            var existe = _context.Clientes.Any(c => c.Id == id);
            return Task.FromResult(existe);
        }

        // Métodos específicos
        public Task<Cliente> ObtenerPorEmailAsync(string email)
        {
            var cliente = _context.Clientes.FirstOrDefault(c => c.Email == email);
            return Task.FromResult(cliente);
        }

        public Task<Cliente> ObtenerPorIdentificacionAsync(string identificacion)
        {
            var cliente = _context.Clientes.FirstOrDefault(c => c.Identificacion == identificacion);
            return Task.FromResult(cliente);
        }

        public Task<IEnumerable<Cliente>> BuscarPorNombreAsync(string nombre)
        {
            var clientes = _context.Clientes
                .Where(c => c.Nombre.Contains(nombre))
                .ToList();
            return Task.FromResult((IEnumerable<Cliente>)clientes);
        }

        // Implementar método faltante de IRepositorio<T>
        public Task<IEnumerable<Cliente>> ObtenerTodosAsync(System.Linq.Expressions.Expression<Func<Cliente, bool>> filtro)
        {
            var clientes = _context.Clientes.Where(filtro).ToList();
            return Task.FromResult((IEnumerable<Cliente>)clientes);
        }
    }
}
