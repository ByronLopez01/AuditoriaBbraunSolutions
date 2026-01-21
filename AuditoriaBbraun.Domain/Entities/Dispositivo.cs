using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuditoriaBbraun.Domain.Entities
{
    public class Dispositivo
    {
        public int Id { get; set; }
        public string NumeroSerie { get; set; }
        public string Tipo { get; set; }
        public bool Activo { get; set; } = true;
        public DateTime FechaRegistro { get; set; } = DateTime.UtcNow;
    }
}
