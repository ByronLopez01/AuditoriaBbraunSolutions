using AuditoriaBbraun.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuditoriaBbraun.Domain.Interfaces
{
    public interface IRepositorioClientes : IRepositorio<Cliente>
    {
        Task<Cliente> ObtenerPorEmailAsync(string email);
        Task<Cliente> ObtenerPorIdentificacionAsync(string identificacion);
        Task<IEnumerable<Cliente>> BuscarPorNombreAsync(string nombre);
    }
}
