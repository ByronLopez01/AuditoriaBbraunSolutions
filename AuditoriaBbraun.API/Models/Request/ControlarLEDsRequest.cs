using System.ComponentModel.DataAnnotations;

namespace AuditoriaBbraun.API.Models.Request
{
    public class ControlarLEDsRequest
    {
        [Required(ErrorMessage = "El estado del slot es requerido")]
        public EstadoSlotRequest EstadoSlot { get; set; }

        
    }
    
}
