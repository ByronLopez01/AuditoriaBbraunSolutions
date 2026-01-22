using AuditoriaBbraun.Domain.Exceptions;
using AuditoriaBbraun.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace AuditoriaBbraun.Domain.Entities
{
    public class Transaccion : BaseEntity
    {
        public long Id { get; set; }
        public string CodigoBarras { get; set; } = string.Empty;
        public decimal Peso { get; set; } // en kg
        public decimal Largo { get; set; } // en cm
        public decimal Ancho { get; set; } // en cm
        public decimal Alto { get; set; } // en cm
        public decimal Volumen { get; set; } // en cm³
        public string DireccionRodillo { get; set; } = string.Empty;

        // Relación con dispositivo
        public int DispositivoId { get; set; }
        public string NumeroSerieDispositivo { get; set; } = string.Empty;

        // Rutas de imágenes
        public string? RutaImagen1 { get; set; }
        public string? RutaImagen2 { get; set; }
        public string? RutaImagen3 { get; set; }
        public string? RutaImagenCompleta { get; set; }
        public string? RutaCapturaPantalla { get; set; }

        
        public DateTime FechaHora { get; set; } // Fecha de la transacción
        public DateTime FechaRecepcion { get; set; } = DateTime.UtcNow; // Fecha que llegó a la API
        public EstadoTransaccion Estado { get; set; } = EstadoTransaccion.Procesado;
        public string? MensajeError { get; set; }

        // Relación con orden 
        public int? OrdenId { get; set; }

      
        public virtual Dispositivo? Dispositivo { get; set; }
        public virtual Orden? Orden { get; set; }
        public virtual Usuario? CreadoPorUsuario { get; set; }
        public virtual ICollection<Imagen> Imagenes { get; set; } = new List<Imagen>();
    }

    public enum EstadoTransaccion
    {
        Procesado,
        Error,
        Pendiente
    }
}

