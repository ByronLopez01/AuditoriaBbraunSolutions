using AuditoriaBbraun.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuditoriaBbraun.Domain.Entities
{
    public class Orden
    {
        public int Id { get; set; }
        public string NumeroOrden { get; set; }
        public int ClienteId { get; set; }
        public EstadoOrden Estado { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime? FechaActualizacion { get; set; }

        public Orden()
        {
            FechaCreacion = DateTime.UtcNow;
            Estado = EstadoOrden.Pendiente;
        }
    }
}
