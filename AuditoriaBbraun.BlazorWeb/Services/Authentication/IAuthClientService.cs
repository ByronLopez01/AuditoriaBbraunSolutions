using AuditoriaBbraun.Application.DTOs.Account;

namespace AuditoriaBbraun.BlazorWeb.Services.Authentication
{
    public interface IAuthClientService
    {
        Task<AuthResponse> Login(LoginRequest loginRequest);
        Task Logout();

        // --- METODOS CRUD ADMIN ---

        // Registrar nuevo usuario
        Task<AuthResponse> Register(RegisterRequest registerRequest);

        // Obtener usuarios
        Task<List<UserDto>> GetUsers();

        // Obtener roles para el ComboBox
        Task<List<string>> GetRoles();

        // Obtener uno por ID
        Task<UserDto> GetUserById(string id);

        // Actualizar
        Task<AuthResponse> UpdateUser(UpdateUserRequest request);

        // Eliminar
        Task<AuthResponse> DeleteUser(string id);
    }
}