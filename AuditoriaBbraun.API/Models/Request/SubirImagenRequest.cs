using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http;

namespace AuditoriaBbraun.API.Models.Request
{
    /// <summary>
    /// Payload multipart/form-data para subir imágenes.
    /// </summary>
    public class SubirImagenRequest
    {
        [JsonPropertyName("barcode")]
        [Required(ErrorMessage = "El código de barras es requerido")]
        [MaxLength(500, ErrorMessage = "El código de barras no puede exceder 500 caracteres")]
        public string? CodigoBarras { get; set; }

        [JsonPropertyName("type")]
        [Required(ErrorMessage = "El tipo de imagen es requerido")]
        [RegularExpression("^(code|depth|env|screen|label)$", ErrorMessage = "Tipo inválido. Use: code, depth, env, screen o label")]
        public string? Tipo { get; set; }

        [JsonPropertyName("image")]
        [Required(ErrorMessage = "La imagen es requerida")]
        public IFormFile? Imagen { get; set; }
    }
}
