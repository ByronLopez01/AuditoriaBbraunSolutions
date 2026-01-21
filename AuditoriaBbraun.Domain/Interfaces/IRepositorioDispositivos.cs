using AuditoriaBbraun.Domain.Entities;
using AuditoriaBbraun.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuditoriaBbraun.Domain.Interfaces
{
    public interface IRepositorioDispositivos : IRepositorio<Dispositivo>
    {
        Task<Dispositivo> ObtenerPorNumeroSerieAsync(string numeroSerie);
        Task<IEnumerable<Dispositivo>> ObtenerActivosAsync();
        Task<bool> ExisteNumeroSerieAsync(string numeroSerie);
    }
}
