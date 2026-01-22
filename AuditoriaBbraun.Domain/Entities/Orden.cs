using AuditoriaBbraun.Domain.Enums;
using System;
using System.Collections.Generic;

namespace AuditoriaBbraun.Domain.Entities
{
    public class Orden
    {
        public int Id { get; set; }
        public string NumeroOrden { get; set; } = string.Empty;
        public int ClienteId { get; set; }
        public string? Descripcion { get; set; }
        public EstadoOrden Estado { get; set; } = EstadoOrden.Pendiente;
        public int Prioridad { get; set; } = 1;
        public string? DireccionEnvio { get; set; }
        // public string? CiudadEnvio { get; set; } validar si se ocupa en bbraun
        public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;
        public DateTime? FechaCompletada { get; set; }
        public DateTime? FechaActualizacion { get; set; }
        public decimal? TotalPeso { get; set; }
        public decimal? TotalVolumen { get; set; }
        public int CantidadItems { get; set; } = 0;
        public string? AsignadoA { get; set; }

        public virtual Cliente? Cliente { get; set; }
        public virtual Usuario? CreadoPorUsuario { get; set; }
        public virtual Usuario? AsignadoAUsuario { get; set; }
        public virtual ICollection<Transaccion> Transacciones { get; set; } = new List<Transaccion>();
    }
}

