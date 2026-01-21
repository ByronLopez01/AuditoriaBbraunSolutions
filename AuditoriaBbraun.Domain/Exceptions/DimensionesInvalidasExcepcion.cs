using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuditoriaBbraun.Domain.Exceptions
{
    public class DimensionesInvalidasExcepcion : Exception
    {
        public DimensionesInvalidasExcepcion() : base("Las dimensiones proporcionadas son inválidas. Deben ser mayores que cero.")
        {
        }

        public DimensionesInvalidasExcepcion(string message) : base(message)
        {
        }

        public DimensionesInvalidasExcepcion(string message, Exception innerException) : base(message, innerException)
        {
        }

        //Metodos estaticos para casos especificos Validar si estan correctos cuando tengamos mas detalles del proyecto 

        public static DimensionesInvalidasExcepcion LargoInvalido(decimal valor)
        {
            return new DimensionesInvalidasExcepcion($"El largo '{valor}' es inválido. Debe ser mayor a cero.");
        }

        public static DimensionesInvalidasExcepcion AnchoInvalido(decimal valor)
        {
            return new DimensionesInvalidasExcepcion($"El ancho '{valor}' es inválido. Debe ser mayor a cero.");
        }

        public static DimensionesInvalidasExcepcion AltoInvalido(decimal valor)
        {
            return new DimensionesInvalidasExcepcion($"El alto '{valor}' es inválido. Debe ser mayor a cero.");
        }

        public static DimensionesInvalidasExcepcion DimensionesFueraDeRango(decimal largo, decimal ancho, decimal alto,decimal maxLargo, decimal maxAncho, decimal maxAlto)
        {
            return new DimensionesInvalidasExcepcion($"Las dimensiones L:{largo} x A:{ancho} x H:{alto} " + $"exceden los límites máximos L:{maxLargo} x A:{maxAncho} x H:{maxAlto}");
        }


    }
}
