using AuditoriaBbraun.BlazorWeb.Components;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// HttpClient hacia la API
// Ajustar ApiBaseUrl en appsettings.json para cambiar el endpoint o puerto de la API!!!!!
builder.Services.AddHttpClient("api", client =>
{
    var baseUrl = builder.Configuration["ApiBaseUrl"] ?? "/";
    client.BaseAddress = new Uri(baseUrl, UriKind.RelativeOrAbsolute);
});
builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("api"));

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
