using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace AuditoriaBbraun.API.Models.Request
{
    /// <summary>
    /// Payload JSON para la interfaz de negocio de DWSSystem.
    /// Campos opcionales (permiten NULL según la especificación).
    /// </summary>
    public class DatosNegocioRequest
    {
        [JsonPropertyName("barcode")]
        [MaxLength(500, ErrorMessage = "El código de barras no puede exceder 500 caracteres")]
        public string? Barcode { get; set; }

        [JsonPropertyName("weight")]
        [Range(0, double.MaxValue, ErrorMessage = "El peso no puede ser negativo")]
        public decimal? Weight { get; set; }

        [JsonPropertyName("length")]
        [Range(0, double.MaxValue, ErrorMessage = "El largo no puede ser negativo")]
        public decimal? Length { get; set; }

        [JsonPropertyName("width")]
        [Range(0, double.MaxValue, ErrorMessage = "El ancho no puede ser negativo")]
        public decimal? Width { get; set; }

        [JsonPropertyName("height")]
        [Range(0, double.MaxValue, ErrorMessage = "El alto no puede ser negativo")]
        public decimal? Height { get; set; }

        [JsonPropertyName("volume")]
        [Range(0, double.MaxValue, ErrorMessage = "El volumen no puede ser negativo")]
        public decimal? Volume { get; set; }

        [JsonPropertyName("image1path")]
        public string? Image1Path { get; set; }

        [JsonPropertyName("image2path")]
        public string? Image2Path { get; set; }

        [JsonPropertyName("image3path")]
        public string? Image3Path { get; set; }

        [JsonPropertyName("imageAllpath")]
        public string? ImageAllPath { get; set; }

        [JsonPropertyName("screenshot")]
        public string? ScreenshotPath { get; set; }

        [JsonPropertyName("image1data")]
        public string? Image1Data { get; set; }

        [JsonPropertyName("image2data")]
        public string? Image2Data { get; set; }

        [JsonPropertyName("image3data")]
        public string? Image3Data { get; set; }

        [JsonPropertyName("imageAlldata")]
        public string? ImageAllData { get; set; }

        [JsonPropertyName("screenshotdata")]
        public string? ScreenshotData { get; set; }

        [JsonPropertyName("timestamp")]
        [RegularExpression(@"^\d{4}-\d{2}-\d{2} \d{2}:\d{2}:\d{2}$",
            ErrorMessage = "Formato inválido. Use: yyyy-MM-dd HH:mm:ss")]
        public string? Timestamp { get; set; }

        [JsonPropertyName("devicesn")]
        [MaxLength(100, ErrorMessage = "El número de serie no puede exceder 100 caracteres")]
        public string? DeviceSn { get; set; }
    }
}
