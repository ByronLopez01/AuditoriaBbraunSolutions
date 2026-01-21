using AuditoriaBbraun.Domain.Entities;
using AuditoriaBbraun.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuditoriaBbraun.Domain.Interfaces
{
    public interface IRepositorioOrdenes : IRepositorio<Orden>
    {
        Task<IEnumerable<Orden>> ObtenerPorClienteAsync(int clienteId);
        Task<IEnumerable<Orden>> ObtenerPorEstadoAsync(EstadoOrden estado);
        Task<IEnumerable<Orden>> ObtenerPorFechaAsync(DateTime fecha);
        Task<Orden> ObtenerConTransaccionesAsync(int ordenId);
    }
}
