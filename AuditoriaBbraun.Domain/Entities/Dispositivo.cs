using AuditoriaBbraun.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuditoriaBbraun.Domain.Entities
{
    public class Dispositivo : BaseEntity
    {
        public int Id { get; set; }
        public string NumeroSerie { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public TipoDispositivo Tipo { get; set; }
        public string? Ubicacion { get; set; }
        public EstadoDispositivo Estado { get; set; } = EstadoDispositivo.Activo;
        public bool Activo { get; set; } = true;
        public string? UsuarioId { get; set; }
        public string? Configuracion { get; set; }
        public virtual Usuario? Usuario { get; set; }
        public virtual Usuario? CreadoPorUsuario { get; set; }
        public virtual Usuario? ModificadoPorUsuario { get; set; }
        public virtual ICollection<Transaccion> Transacciones { get; set; } = new List<Transaccion>();
        public virtual ICollection<EstadoLed> EstadoLEDs { get; set; } = new List<EstadoLed>();
    }

    public enum TipoDispositivo
    {
        Z,
        Standard,
        Premium
    }

    public enum EstadoDispositivo
    {
        Activo,
        Inactivo,
        Mantenimiento,
        Error
    }
}

