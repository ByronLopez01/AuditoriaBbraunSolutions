using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuditoriaBbraun.Domain.Exceptions
{
    public class PesoInvalidoExcepcion :Exception
    {
        public decimal PesoInvalido { get; }

        public PesoInvalidoExcepcion(): base("El peso proporcionado es inválido. Debe ser mayor a cero.")
        {
            PesoInvalido = 0;
        }

        public PesoInvalidoExcepcion(string mensaje): base(mensaje)
        {
            PesoInvalido = 0;
        }

        public PesoInvalidoExcepcion(decimal pesoInvalido): base($"El peso '{pesoInvalido}' es inválido. Debe ser mayor a cero.")
        {
            PesoInvalido = pesoInvalido;
        }

        public PesoInvalidoExcepcion(string mensaje, decimal pesoInvalido): base(mensaje)
        {
            PesoInvalido = pesoInvalido;
        }

        public PesoInvalidoExcepcion(string mensaje, Exception innerException): base(mensaje, innerException)
        {
            PesoInvalido = 0;
        }

        //Metodos estaticos para casos especificos Validar si estan correctos cuando tengamos mas detalles del proyecto

        public static PesoInvalidoExcepcion PesoCero()
        {
            return new PesoInvalidoExcepcion("El peso no puede ser cero.");
        }

        public static PesoInvalidoExcepcion PesoNegativo(decimal peso)
        {
            return new PesoInvalidoExcepcion($"El peso '{peso}' no puede ser negativo.", peso);
        }

        public static PesoInvalidoExcepcion ExcedePesoMaximo(decimal peso, decimal pesoMaximo)
        {
            return new PesoInvalidoExcepcion($"El peso '{peso}' excede el peso máximo permitido de '{pesoMaximo}'.",peso);
        }

        public static PesoInvalidoExcepcion PesoInsuficiente(decimal peso, decimal pesoMinimo)
        {
            return new PesoInvalidoExcepcion($"El peso '{peso}' es menor al peso mínimo requerido de '{pesoMinimo}'.",peso);
        }

    }
}
