using AuditoriaBbraun.Application.DTOs.Account;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Json;

namespace AuditoriaBbraun.BlazorWeb.Services.Authentication
{
    public class AuthClientService : IAuthClientService
    {
        private readonly HttpClient _httpClient;
        private readonly ILocalStorageService _localStorage;
        private readonly AuthenticationStateProvider _authStateProvider;

        public AuthClientService(HttpClient httpClient,
                                 ILocalStorageService localStorage,
                                 AuthenticationStateProvider authStateProvider)
        {
            _httpClient = httpClient;
            _localStorage = localStorage;
            _authStateProvider = authStateProvider;
        }

        public async Task<AuthResponse> Login(LoginRequest loginRequest)
        {
            // URL al puerto
            var result = await _httpClient.PostAsJsonAsync("/api/Account/login", loginRequest);

            var response = await result.Content.ReadFromJsonAsync<AuthResponse>();

            if (response!.Success)
            {
                // Guardamos el token en el navegador
                await _localStorage.SetItemAsync("accessToken", response.Token);

                // Avisamos a Blazor que el usuario cambió de estado (Haremos este provider en el sig. paso)
                ((CustomAuthStateProvider)_authStateProvider).NotifyUserLogin(response.Token);
            }

            return response;
        }

        public async Task Logout()
        {
            await _localStorage.RemoveItemAsync("accessToken");
            ((CustomAuthStateProvider)_authStateProvider).NotifyUserLogout();
        }


        // Implementacion del Registro (enviado con Token de Admin en el HttpClient)
        public async Task<AuthResponse> Register(RegisterRequest registerRequest)
        {
            var result = await _httpClient.PostAsJsonAsync("/api/Account/register", registerRequest);
            return await result.Content.ReadFromJsonAsync<AuthResponse>();
        }

        public async Task<List<UserDto>> GetUsers()
        {
            var result = await _httpClient.GetFromJsonAsync<List<UserDto>>("/api/Account/users");
            return result ?? new List<UserDto>();
        }

        public async Task<List<string>> GetRoles()
        {
            var result = await _httpClient.GetFromJsonAsync<List<string>>("/api/Account/roles");
            return result ?? new List<string>();
        }

        public async Task<UserDto> GetUserById(string id)
        {
            var result = await _httpClient.GetFromJsonAsync<UserDto>($"/api/Account/users/{id}");
            return result ?? throw new Exception("No se pudo obtener el usuario.");
        }

        public async Task<AuthResponse> UpdateUser(UpdateUserRequest request)
        {
            var result = await _httpClient.PutAsJsonAsync("/api/Account/users", request);
            return await result.Content.ReadFromJsonAsync<AuthResponse>();
        }

        public async Task<AuthResponse> DeleteUser(string id)
        {
            var result = await _httpClient.DeleteAsync($"/api/Account/users/{id}");
            return await result.Content.ReadFromJsonAsync<AuthResponse>();
        }
    }
}