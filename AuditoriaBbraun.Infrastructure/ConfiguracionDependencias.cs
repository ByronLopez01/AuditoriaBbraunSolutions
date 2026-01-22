using AuditoriaBbraun.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuditoriaBbraun.Infrastructure
{
    public static class ConfiguracionDependencias
    {
        public static IServiceCollection AgregarInfraestructura(
            this IServiceCollection servicios,
            IConfiguration configuracion)
        {
            // Configurar DbContext con Identity
            servicios.AddDbContext<ContextApplications>(options =>
                options.UseSqlServer(
                    configuracion.GetConnectionString("DefaultConnection"),
                    b => b.MigrationsAssembly(typeof(ContextApplications).Assembly.FullName)));

            return servicios;
        }
    }
}
