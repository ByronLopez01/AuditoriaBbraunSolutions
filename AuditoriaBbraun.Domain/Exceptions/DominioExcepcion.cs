using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuditoriaBbraun.Domain.Exceptions
{
    public abstract class DominioExcepcion : Exception
    {
        public string CodigoError { get; }
        public DateTime FechaError { get; }

        protected DominioExcepcion(string mensaje): base(mensaje)
        {
            CodigoError = GenerarCodigoError();
            FechaError = DateTime.UtcNow;
        }

        protected DominioExcepcion(string mensaje, Exception innerException): base(mensaje, innerException)
        {
            CodigoError = GenerarCodigoError();
            FechaError = DateTime.UtcNow;
        }

        protected DominioExcepcion(string codigoError, string mensaje): base(mensaje)
        {
            CodigoError = codigoError;
            FechaError = DateTime.UtcNow;
        }

        private string GenerarCodigoError()
        {
            return $"DOM-{GetType().Name.ToUpper()}-{DateTime.UtcNow:yyyyMMddHHmmss}";
        }

        public override string ToString()
        {
            return $"[{CodigoError}] {FechaError:yyyy-MM-dd HH:mm:ss} - {GetType().Name}: {Message}";
        }
    }
}
