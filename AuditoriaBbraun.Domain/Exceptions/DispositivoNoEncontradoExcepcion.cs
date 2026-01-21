using AuditoriaBbraun.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuditoriaBbraun.Domain.Exceptions
{
    public class DispositivoNoEncontradoExcepcion : DominioExcepcion
    {
        public string NumeroSerie { get; }
        public int? DispositivoId { get; }

        public DispositivoNoEncontradoExcepcion(string numeroSerie): base($"No se encontró el dispositivo con número de serie {numeroSerie}.")
        {
            NumeroSerie = numeroSerie;
            DispositivoId = null;
        }

        public DispositivoNoEncontradoExcepcion(int dispositivoId): base($"No se encontró el dispositivo con ID {dispositivoId}.")
        {
            NumeroSerie = null;
            DispositivoId = dispositivoId;
        }

        public DispositivoNoEncontradoExcepcion(string numeroSerie, string mensaje): base(mensaje)
        {
            NumeroSerie = numeroSerie;
            DispositivoId = null;
        }

    }
}
