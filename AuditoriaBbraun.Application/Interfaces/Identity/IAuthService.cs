using AuditoriaBbraun.Application.DTOs.Account;
using System.Threading.Tasks;

namespace AuditoriaBbraun.Application.Contracts.Identity
{
    public interface IAuthService
    {
        // Método para login: Recibe user/pass y devuelve Token
        Task<AuthResponse> LoginAsync(LoginRequest request);

        // Método para registro (usado para inicializar datos semilla)
        Task<AuthResponse> RegisterAsync(RegisterRequest request);
    }
}