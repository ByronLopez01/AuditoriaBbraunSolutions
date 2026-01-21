using AuditoriaBbraun.Domain.Enums;
using AuditoriaBbraun.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuditoriaBbraun.Domain.Services.Implementations
{
    public class ServicioEnrutamientoRodillo : IServicioEnrutamientoRodillo
    {
        public DireccionRodillo DeterminarDireccion(Peso peso, Dimensiones dimensiones)
        {
            if (peso.Valor > 50)
                return DireccionRodillo.ExitPort1;

            if (dimensiones.Volumen > 100)
                return DireccionRodillo.Router2;

            //Logica adicional agregar mas si nos falta logica

            if (peso.Valor > 20 && dimensiones.Volumen > 50)
                return DireccionRodillo.Router1;

            //por defecto 
            return DireccionRodillo.Router0;
        }
    }
}
