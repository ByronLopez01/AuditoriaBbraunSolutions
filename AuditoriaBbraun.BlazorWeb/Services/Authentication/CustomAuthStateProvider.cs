using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
using System.Text.Json;

namespace AuditoriaBbraun.BlazorWeb.Services.Authentication
{
    public class CustomAuthStateProvider : AuthenticationStateProvider
    {
        private readonly ILocalStorageService _localStorageService;
        private readonly HttpClient _http;

        public CustomAuthStateProvider(ILocalStorageService localStorageService, HttpClient http)
        {
            _localStorageService = localStorageService;
            _http = http;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            string token = "";
            Console.WriteLine("DEBUG: Iniciando GetAuthenticationStateAsync...");
            try
            {
                // Buscamos si existe un token guardado
                token = await _localStorageService.GetItemAsStringAsync("accessToken");
                Console.WriteLine($"DEBUG: Token leído de LocalStorage: {(string.IsNullOrEmpty(token) ? "VACIO" : "ENCONTRADO")}");
            }
            catch (InvalidOperationException)
            {
                Console.WriteLine("DEBUG: Error JS Interop (Prerendering), ignorando.");
            }

            var identity = new ClaimsIdentity();
            _http.DefaultRequestHeaders.Authorization = null;

            if (!string.IsNullOrEmpty(token))
            {
                try
                {
                    // Si hay token, creamos las Claims (leemos roles, email, etc.)
                    identity = new ClaimsIdentity(ParseClaimsFromJwt(token), "jwt");
                    // Le asignamos el token al header del HttpClient para futuras peticiones
                    _http.DefaultRequestHeaders.Authorization =
                        new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                }
                catch
                {
                    // Si el token está dañado, intentamos borrarlo de forma segura
                    try
                    {
                        await _localStorageService.RemoveItemAsync("accessToken");
                    }
                    catch
                    {
                    }
                    identity = new ClaimsIdentity();
                }
            }

            var user = new ClaimsPrincipal(identity);

            return new AuthenticationState(user);
        }

        // Metodo publico para avisar login exitoso
        public void NotifyUserLogin(string token)
        {
            var identity = new ClaimsIdentity(ParseClaimsFromJwt(token), "jwt");
            var user = new ClaimsPrincipal(identity);
            var state = new AuthenticationState(user);

            // Adjuntamos el token automatico a las siguientes peticiones
            _http.DefaultRequestHeaders.Authorization =
                       new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            NotifyAuthenticationStateChanged(Task.FromResult(state));
        }

        // Metodo publico para avisar logout
        public void NotifyUserLogout()
        {
            _http.DefaultRequestHeaders.Authorization = null;
            var identity = new ClaimsIdentity();
            var user = new ClaimsPrincipal(identity);
            var state = new AuthenticationState(user);
            NotifyAuthenticationStateChanged(Task.FromResult(state));
        }


        // ==========================================
        // UTILIDADES INTERNAS (DECODIFICACION)
        // ==========================================
        private IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
        {
            var claims = new List<Claim>();
            var payload = jwt.Split('.')[1];
            var jsonBytes = ParseBase64WithoutPadding(payload);

            // Permitimos que el diccionario sea nullable
            var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);

            // Si es nulo, devolvemos lista vacia
            if (keyValuePairs == null)
            {
                return claims;
            }

            keyValuePairs.TryGetValue("role", out object? roles); // object? nullable

            if (roles != null)
            {
                if (roles.ToString()!.Trim().StartsWith("[")) // ! asegura no nulo
                {
                    var parsedRoles = JsonSerializer.Deserialize<string[]>(roles.ToString()!);
                    if (parsedRoles != null)
                    {
                        foreach (var parsedRole in parsedRoles)
                        {
                            claims.Add(new Claim(ClaimTypes.Role, parsedRole));
                        }
                    }
                }
                else
                {
                    claims.Add(new Claim(ClaimTypes.Role, roles.ToString()!));
                }

                keyValuePairs.Remove(ClaimTypes.Role);
            }

            // Aseguramos conversion a string segura
            claims.AddRange(keyValuePairs
                .Where(kvp => kvp.Value != null)
                .Select(kvp => new Claim(kvp.Key, kvp.Value.ToString()!)));

            return claims;
        }

        private byte[] ParseBase64WithoutPadding(string base64)
        {
            switch (base64.Length % 4)
            {
                case 2: base64 += "=="; break;
                case 3: base64 += "="; break;
            }
            return Convert.FromBase64String(base64);
        }
    }
}