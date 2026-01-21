using System.Text.Json.Serialization;
using System.Text.Json;
using AuditoriaBbraun.Application.UseCases.MaquinaDWS.Commands.ProcesarDatosNegocio;
using AuditoriaBbraun.Domain.Interfaces;
using FluentValidation;
using AuditoriaBbraun.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

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

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
