namespace AuditoriaBbraun.Application.DTOs.Account
{
    public class UpdateUserRequest
    {
        public string Id { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Rol { get; set; } = string.Empty;
        public string? Password { get; set; }
    }
}