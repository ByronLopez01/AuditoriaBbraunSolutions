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
    }
}