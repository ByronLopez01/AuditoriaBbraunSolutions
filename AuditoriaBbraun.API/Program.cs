using AuditoriaBbraun.Application.Contracts.Identity;
using AuditoriaBbraun.Application.UseCases.MaquinaDWS.Commands.ProcesarDatosNegocio;
using AuditoriaBbraun.Domain.Interfaces;
using AuditoriaBbraun.Infrastructure.Data;
using AuditoriaBbraun.Infrastructure.Identity;
using AuditoriaBbraun.Infrastructure.Identity.Models;
using AuditoriaBbraun.Infrastructure.Identity.Services;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(ProcesarDatosNegocioCommand).Assembly);
});

builder.Services.AddValidatorsFromAssemblyContaining<ProcesarDatosNegocioCommandValidator>();

builder.Services.AddDbContext<ContextApplications>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});



//CORS policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("PermitirDWSSystem", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// Configurar DB Context (Conexión SQL)
builder.Services.AddDbContext<AppIdentityDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
    b => b.MigrationsAssembly(typeof(AppIdentityDbContext).Assembly.FullName)));

// Configurar Identity (Usuarios y Roles)
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<AppIdentityDbContext>()
    .AddDefaultTokenProviders();

// Mapear configuración JWT desde appsettings
builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));

// Inyectar el Servicio de Autenticación (Interface -> Implementación)
builder.Services.AddScoped<IAuthService, AuthService>();

// Configurar la Autenticación JWT (Cómo validar el token)
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(o =>
{
    o.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero,
        ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
        ValidAudience = builder.Configuration["JwtSettings:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:Key"]))
    };
});


var app = builder.Build();


using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        // Ejecutamos el sembrador
        await AuditoriaBbraun.Infrastructure.Data.DbSeeder.SeedRolesAndAdminAsync(services);
    }
    catch (Exception ex)
    {
        // Aquí podrías loguear errores si falla la conexión a BD al iniciar
        Console.WriteLine("Ocurrió un error al insertar datos semilla: " + ex.Message);
    }
}


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
