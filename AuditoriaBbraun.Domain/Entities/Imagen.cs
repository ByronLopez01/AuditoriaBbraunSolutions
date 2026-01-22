using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuditoriaBbraun.Domain.Entities
{
    public class Imagen : BaseEntity
    {
        public int Id { get; set; }
        public long TransaccionId { get; set; }
        public TipoImagen Tipo { get; set; }
        public string RutaArchivo { get; set; } = string.Empty;
        public string NombreArchivo { get; set; } = string.Empty;
        public long TamanioBytes { get; set; }
        public string Formato { get; set; } = string.Empty;
        public DateTime FechaSubida { get; set; } = DateTime.UtcNow;
        public string? Metadata { get; set; } // JSON

        public virtual Transaccion? Transaccion { get; set; }
    }

    public enum TipoImagen
    {
        code,    // barcode image
        depth,   // volume image
        env,     // Panoramic Camera Pictures
        screen,  // screenshot
        label    // label
    }
}

