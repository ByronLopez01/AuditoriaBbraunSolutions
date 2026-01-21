using System.ComponentModel.DataAnnotations;

namespace AuditoriaBbraun.API.Models.Request
{

    //Modelo para la maquina 
    public class DatosNegocioRequest
    {
        [Required(ErrorMessage = "El código de barras es requerido")]
        public string CodigoBarras { get; set; }

        [Required(ErrorMessage = "El peso es requerido")]
        [Range(0.01, double.MaxValue, ErrorMessage = "El peso debe ser mayor a cero")]
        public decimal Peso { get; set; }

        [Required(ErrorMessage = "El largo es requerido")]
        [Range(0.1, double.MaxValue, ErrorMessage = "El largo debe ser mayor a cero")]
        public decimal Largo { get; set; }

        [Required(ErrorMessage = "El ancho es requerido")]
        [Range(0.1, double.MaxValue, ErrorMessage = "El ancho debe ser mayor a cero")]
        public decimal Ancho { get; set; }

        [Required(ErrorMessage = "El alto es requerido")]
        [Range(0.1, double.MaxValue, ErrorMessage = "El alto debe ser mayor a cero")]
        public decimal Alto { get; set; }

        [Required(ErrorMessage = "El volumen es requerido")]
        public decimal Volumen { get; set; }

        public string RutaImagen1 { get; set; }
        public string RutaImagen2 { get; set; }
        public string RutaImagen3 { get; set; }
        public string RutaImagenCompleta { get; set; }
        public string RutaCapturaPantalla { get; set; }

        [Required(ErrorMessage = "La fecha y hora son requeridas")]
        [RegularExpression(@"^\d{4}-\d{2}-\d{2} \d{2}:\d{2}:\d{2}$",
            ErrorMessage = "Formato de fecha inválido. Use: yyyy-MM-dd HH:mm:ss")]
        public string FechaHora { get; set; }

        [Required(ErrorMessage = "El número de serie del dispositivo es requerido")]
        public string NumeroSerieDispositivo { get; set; }
    }
}
