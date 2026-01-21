using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuditoriaBbraun.Domain.Exceptions
{
    public class OrdenNoEncontradaExcepcion : DominioExcepcion
    {
        public int? OrdenId { get; }
        public string NumeroOrden { get; }

        public OrdenNoEncontradaExcepcion(int ordenId): base($"No se encontró la orden con ID {ordenId}.")
        {
            OrdenId = ordenId;
            NumeroOrden = null;
        }

        public OrdenNoEncontradaExcepcion(string numeroOrden): base($"No se encontró la orden con número {numeroOrden}.")
        {
            OrdenId = null;
            NumeroOrden = numeroOrden;
        }

        public OrdenNoEncontradaExcepcion(int ordenId, string mensaje): base(mensaje)
        {
            OrdenId = ordenId;
            NumeroOrden = null;
        }

        public OrdenNoEncontradaExcepcion(string numeroOrden, string mensaje): base(mensaje)
        {
            OrdenId = null;
            NumeroOrden = numeroOrden;
        }
    }
}
