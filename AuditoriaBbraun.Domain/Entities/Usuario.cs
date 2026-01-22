using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuditoriaBbraun.Domain.Entities
{
    public class Usuario : IdentityUser 
    {
        public string? NombreCompleto { get; set; }
        public string? Departamento { get; set; }
        public bool Activo { get; set; } = true;
        public DateTime FechaRegistro { get; set; } = DateTime.UtcNow;


        //Relaciones
        public virtual ICollection<Dispositivo> Dispositivos { get; set; } = new List<Dispositivo>();
        public virtual ICollection<Transaccion> TransaccionesCreadas { get; set; } = new List<Transaccion>();
        public virtual ICollection<Orden> OrdenesCreadas { get; set; } = new List<Orden>();
        public virtual ICollection<Orden> OrdenesAsignadas { get; set; } = new List<Orden>();
    }
}
