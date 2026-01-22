using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace AuditoriaBbraun.API.Models.Request
{
    /// <summary>
    /// Payload JSON para controlar LEDs (slotStatus).
    /// </summary>
    public class ControlarLEDsRequest
    {
        [JsonPropertyName("slotStatus")]
        [Required(ErrorMessage = "El estado del slot es requerido")]
        public EstadoSlotRequest? EstadoSlot { get; set; }
    }
}
