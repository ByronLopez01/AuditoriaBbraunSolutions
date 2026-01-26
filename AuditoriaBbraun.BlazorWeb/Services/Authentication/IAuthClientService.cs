using AuditoriaBbraun.Application.DTOs.Account;

namespace AuditoriaBbraun.BlazorWeb.Services.Authentication
{
    public interface IAuthClientService
    {
        Task<AuthResponse> Login(LoginRequest loginRequest);
        Task Logout();
    }
}