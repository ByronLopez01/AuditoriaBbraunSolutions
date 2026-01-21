using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuditoriaBbraun.Domain.Enums
{
    public enum EstadoOrden
    {
        Pendiente = 1,
        Procesando = 2,
        Completada = 3,
        Cancelada = 4,
        Error = 5
    }
}
