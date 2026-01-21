using AuditoriaBbraun.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuditoriaBbraun.Domain.Interfaces
{
    public interface IRepositorioTransacciones : IRepositorio<Transaccion>
    {
        Task<IEnumerable<Transaccion>> ObtenerPorDispositivoAsync(string numeroSerie);
        Task<IEnumerable<Transaccion>> ObtenerPorFechaAsync(DateTime fechaInicio, DateTime fechaFin);
        Task<Transaccion> ObtenerPorCodigoBarrasAsync(string codigoBarras);
    }
}
