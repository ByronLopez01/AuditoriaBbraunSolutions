using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuditoriaBbraun.Domain.Exceptions
{
    public class ValidacionExcepcion : DominioExcepcion
    {
        public Dictionary<string, string[]> Errores { get; }

        public ValidacionExcepcion(Dictionary<string, string[]> errores): base("Se encontraron errores de validación.")
        {
            Errores = errores ?? new Dictionary<string, string[]>();
        }

        public ValidacionExcepcion(string propiedad, string mensajeError): base($"Error de validación en la propiedad '{propiedad}': {mensajeError}")
        {
            Errores = new Dictionary<string, string[]>
            {
                [propiedad] = new[] { mensajeError }
            };
        }

        public override string ToString()
        {
            if (Errores == null || !Errores.Any())
                return base.ToString();

            var erroresStr = string.Join("; ",
                Errores.Select(e => $"{e.Key}: {string.Join(", ", e.Value)}"));

            return $"{base.ToString()} - Errores: {erroresStr}";
        }
    }
}
