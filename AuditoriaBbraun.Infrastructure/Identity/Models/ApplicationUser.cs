using Microsoft.AspNetCore.Identity;

namespace AuditoriaBbraun.Infrastructure.Identity.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Nombre { get; set; } = string.Empty;
        public string Rol { get; set; } = string.Empty;
    }
}