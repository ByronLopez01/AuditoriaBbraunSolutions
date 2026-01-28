using AuditoriaBbraun.BlazorWeb.Components;
using AuditoriaBbraun.BlazorWeb.Services.Authentication;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Components.Authorization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents(options =>
    {
        options.DetailedErrors = true;
    });

// HttpClient hacia la API
// Ajustar ApiBaseUrl en appsettings.json para cambiar el endpoint o puerto de la API!!!!!
builder.Services.AddHttpClient("api", client =>
{
    var baseUrl = builder.Configuration["ApiBaseUrl"] ?? "/";
    client.BaseAddress = new Uri(baseUrl, UriKind.RelativeOrAbsolute);
});

builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("api"));

// Guardar token
builder.Services.AddBlazoredLocalStorage();

// Sistema de autorización base de .NET
builder.Services.AddAuthorizationCore();
builder.Services.AddCascadingAuthenticationState();

builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
})
.AddCookie(options =>
{
    options.LoginPath = "/login";
    options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
});

builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();

// Servicio de Login
builder.Services.AddScoped<IAuthClientService, AuthClientService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
