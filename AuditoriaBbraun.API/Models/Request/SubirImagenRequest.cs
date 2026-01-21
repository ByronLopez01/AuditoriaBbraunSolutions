using System.ComponentModel.DataAnnotations;

namespace AuditoriaBbraun.API.Models.Request
{
    public class SubirImagenRequest
    {
        [Required(ErrorMessage = "El código de barras es requerido")]
        public string CodigoBarras { get; set; }

        [Required(ErrorMessage = "El tipo de imagen es requerido")]
        public string Tipo { get; set; } // code, depth, env, screen, label

        [Required(ErrorMessage = "La imagen es requerida")]
        public IFormFile Imagen { get; set; }
    }
}
