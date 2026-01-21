using System.ComponentModel.DataAnnotations;

namespace AuditoriaBbraun.API.Models.Request
{
    public class EstadoSlotRequest
    {
        [Required(ErrorMessage = "El número de slot es requerido")]
        [Range(1, int.MaxValue, ErrorMessage = "El número de slot debe ser mayor a cero")]
        public int NumeroSlot { get; set; }

        [Required(ErrorMessage = "El estado es requerido")]
        [RegularExpression("^(free|work|done)$", ErrorMessage = "Estado inválido. Use: free, work, done")]
        public string Estado { get; set; }
    }
}
