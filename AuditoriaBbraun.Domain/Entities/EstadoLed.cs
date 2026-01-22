using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuditoriaBbraun.Domain.Entities
{
    public class EstadoLed : BaseEntity
    {
        public int Id { get; set; }
        public int DispositivoId { get; set; }
        public int NumeroSlot { get; set; }
        public EstadoLEDValor Estado { get; set; }
        public DateTime FechaActualizacion { get; set; } = DateTime.UtcNow;
        public string? ActualizadoPor { get; set; }

        public virtual Dispositivo? Dispositivo { get; set; }
        public virtual Usuario? ActualizadoPorUsuario { get; set; }
    }

    public enum EstadoLEDValor
    {
        free,  // apagado
        work,  // verde encendido
        done   // rojo encendido
    }
}