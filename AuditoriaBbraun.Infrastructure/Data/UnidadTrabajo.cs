using AuditoriaBbraun.Domain.Interfaces;
using AuditoriaBbraun.Infrastructure.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuditoriaBbraun.Infrastructure.Data
{
    public class UnidadTrabajo : IUnidadTrabajo
    {
        private readonly ContextApplications _contexto;
        private bool _disposed = false;

        private IRepositorioTransacciones _repositorioTransacciones;
        private IRepositorioOrdenes _repositorioOrdenes;
        private IRepositorioClientes _repositorioClientes;
        private IRepositorioDispositivos _repositorioDispositivos;


        public UnidadTrabajo(ContextApplications contexto)
        {
            _contexto = contexto ?? throw new ArgumentNullException(nameof(contexto));
        }


        public IRepositorioTransacciones RepositorioTransacciones
            => _repositorioTransacciones ??= new RepositorioTransacciones(_contexto);

        public IRepositorioOrdenes RepositorioOrdenes
            => _repositorioOrdenes ??= new RepositorioOrdenes(_contexto);

        public IRepositorioClientes RepositorioClientes
            => _repositorioClientes ??= new RepositorioClientes(_contexto);

        public IRepositorioDispositivos RepositorioDispositivos
            => _repositorioDispositivos ??= new RepositorioDispositivos(_contexto);

        public async Task<int> GuardarCambiosAsync(CancellationToken cancellationToken = default)
        {
            return await _contexto.SaveChangesAsync(cancellationToken);
        }

        public async Task<bool> GuardarEntidadesAsync(CancellationToken cancellationToken = default)
        {
            return await _contexto.SaveChangesAsync(cancellationToken) > 0;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _contexto.Dispose();
                }
                _disposed = true;
            }
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
