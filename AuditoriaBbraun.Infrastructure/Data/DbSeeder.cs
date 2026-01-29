using AuditoriaBbraun.Application.Constants;
using AuditoriaBbraun.Infrastructure.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace AuditoriaBbraun.Infrastructure.Data
{
    public static class DbSeeder
    {
        public static async Task SeedRolesAndAdminAsync(IServiceProvider service)
        {
            // Obtener gestores de usuarios y roles
            var userManager = service.GetService(typeof(UserManager<ApplicationUser>)) as UserManager<ApplicationUser>;
            var roleManager = service.GetService(typeof(RoleManager<IdentityRole>)) as RoleManager<IdentityRole>;

            var loggerFactory = service.GetService(typeof(ILoggerFactory)) as ILoggerFactory;
            var logger = loggerFactory?.CreateLogger("DbSeeder");

            // VALIDACIÓN DE NULOS
            if (userManager == null || roleManager == null)
            {
                logger?.LogCritical("ERROR: No se pudieron resolver los servicios de Identity (UserManager o RoleManager). El sembrado de datos no se ejecutara.");

                return;
            }

            // 1. Crear Roles si no existen
            await CreateRoleAsync(roleManager, CustomRoles.Admin);
            await CreateRoleAsync(roleManager, CustomRoles.Operario);
            await CreateRoleAsync(roleManager, CustomRoles.Auditor);

            // 2. Crear Usuarios por defecto para cada Rol

            // Usuario ADMIN
            var adminUser = new ApplicationUser
            {
                UserName = "admin",
                Email = "admin@bbraun.com",
                Nombre = "Administrador Sistema",
                EmailConfirmed = true,
                Rol = CustomRoles.Admin
            };
            await CreateUserAsync(userManager, adminUser, "Admin123!", CustomRoles.Admin);

            // Usuario OPERARIO
            var operarioUser = new ApplicationUser
            {
                UserName = "operario",
                Email = "operario@bbraun.com",
                Nombre = "Operario Planta",
                EmailConfirmed = true,
                Rol = CustomRoles.Operario
            };
            await CreateUserAsync(userManager, operarioUser, "Operario123!", CustomRoles.Operario);

            // Usuario AUDITOR
            var auditorUser = new ApplicationUser
            {
                UserName = "auditor",
                Email = "auditor@bbraun.com",
                Nombre = "Auditor Externo",
                EmailConfirmed = true,
                Rol = CustomRoles.Auditor
            };
            await CreateUserAsync(userManager, auditorUser, "Auditor123!", CustomRoles.Auditor);
        }

        private static async Task CreateRoleAsync(RoleManager<IdentityRole> roleManager, string roleName)
        {
            if (!await roleManager.RoleExistsAsync(roleName))
            {
                await roleManager.CreateAsync(new IdentityRole(roleName));
            }
        }

        private static async Task CreateUserAsync(UserManager<ApplicationUser> userManager, ApplicationUser user, string password, string role)
        {
            var userInDb = await userManager.FindByEmailAsync(user.Email);
            if (userInDb == null)
            {
                var result = await userManager.CreateAsync(user, password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, role);
                }
            }
        }
    }
}