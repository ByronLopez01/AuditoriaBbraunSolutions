using AuditoriaBbraun.Application.DTOs.Account;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AuditoriaBbraun.Application.Interfaces.Identity
{
    public interface IAuthService
    {
        // Método para login: Recibe user/pass y devuelve Token
        Task<AuthResponse> LoginAsync(LoginRequest request);

        // Método para registro
        Task<AuthResponse> RegisterAsync(RegisterRequest request);

        // Obtener listado de todos los usuarios
        Task<List<UserDto>> GetUsersAsync();

        // Obtener un usuario por ID (para cargar el formulario de edición)
        Task<UserDto?> GetUserByIdAsync(string userId);

        // Actualizar usuario
        Task<AuthResponse> UpdateUserAsync(UpdateUserRequest request);

        // Eliminar usuario
        Task<AuthResponse> DeleteUserAsync(string userId);

        // Obtener lista de roles disponibles (para ComboBox)
        Task<List<string>> GetRolesAsync();
    }
}