using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuditoriaBbraun.Domain.Interfaces
{
    public interface IUnidadTrabajo : IDisposable
    {
        IRepositorioTransacciones RepositorioTransacciones { get; }
        IRepositorioOrdenes RepositorioOrdenes { get; }
        IRepositorioClientes RepositorioClientes { get; }
        IRepositorioDispositivos RepositorioDispositivos { get; }

        Task<int> GuardarCambiosAsync(CancellationToken cancellationToken = default);
        Task<bool> GuardarEntidadesAsync(CancellationToken cancellationToken = default);
    }
}
