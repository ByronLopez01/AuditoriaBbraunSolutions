using AuditoriaBbraun.Domain.Enums;
using AuditoriaBbraun.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuditoriaBbraun.Domain.Services
{
    public interface IServicioEnrutamientoRodillo
    {
        DireccionRodillo DeterminarDireccion(Peso peso, Dimensiones dimensiones);
    }
}
