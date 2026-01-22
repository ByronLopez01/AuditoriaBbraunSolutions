using System;
using System.Collections.Generic;

namespace AuditoriaBbraun.Domain.Entities
{
    public class Cliente
    {
        public int Id { get; set; }
        public string Codigo { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public string? Email { get; set; }
        public string? Telefono { get; set; }
        public string? Direccion { get; set; }
        public string? Identificacion { get; set; }
        public bool Activo { get; set; } = true;
        public DateTime FechaRegistro { get; set; } = DateTime.UtcNow;

        public virtual Usuario? CreadoPorUsuario { get; set; }
        public virtual ICollection<Orden> Ordenes { get; set; } = new List<Orden>();
    }
}

