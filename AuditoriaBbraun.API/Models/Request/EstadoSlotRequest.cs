using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace AuditoriaBbraun.API.Models.Request
{
    public class EstadoSlotRequest
    {
        [JsonPropertyName("slotNo")]
        [Range(1, int.MaxValue, ErrorMessage = "El número de slot debe ser mayor a cero")]
        public int NumeroSlot { get; set; }

        [JsonPropertyName("status")]
        [Required(ErrorMessage = "El estado es requerido")]
        [RegularExpression("^(free|work|done)$", ErrorMessage = "Estado inválido. Use: free, work o done")]
        public string? Estado { get; set; }
    }
}
