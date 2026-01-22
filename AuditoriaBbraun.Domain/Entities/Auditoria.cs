using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuditoriaBbraun.Domain.Entities
{
    public class Auditoria : BaseEntity
    {
        public long Id { get; set; }
        public DateTime Fecha { get; set; } = DateTime.UtcNow;
        public string? UsuarioId { get; set; }
        public string Accion { get; set; } = string.Empty; // Crear, Actualizar, Eliminar, Login, etc.
        public string Entidad { get; set; } = string.Empty; // Transaccion, Orden, Cliente, etc.
        public string? EntidadId { get; set; }
        public string? Descripcion { get; set; }
        public string? Detalles { get; set; } // JSON con datos antes/después
        public string? UserAgent { get; set; }

        public virtual Usuario? Usuario { get; set; }
    }
}
