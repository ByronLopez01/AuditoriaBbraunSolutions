using AuditoriaBbraun.Application.Interfaces.Identity;
using AuditoriaBbraun.Application.DTOs.Account;
using AuditoriaBbraun.Infrastructure.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace AuditoriaBbraun.Infrastructure.Identity.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly JwtSettings _jwtSettings;

        public AuthService(UserManager<ApplicationUser> userManager,
                           SignInManager<ApplicationUser> signInManager,
                           RoleManager<IdentityRole> roleManager,
                           IOptions<JwtSettings> jwtSettings)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _jwtSettings = jwtSettings.Value;
        }

        public async Task<AuthResponse> LoginAsync(LoginRequest request)
        {
            var user = await _userManager.FindByNameAsync(request.UserName);
            if (user == null)
            {
                return new AuthResponse { Success = false, Message = "Usuario no encontrado." };
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);

            if (!result.Succeeded)
            {
                return new AuthResponse { Success = false, Message = "Credenciales inválidas." };
            }

            // Si llegamos aqui, el usuario es valido. Generamos Token.
            return await GenerateAuthResponseAsync(user);
        }

        public async Task<AuthResponse> RegisterAsync(RegisterRequest request)
        {
            var existingUser = await _userManager.FindByEmailAsync(request.Email);
            if (existingUser != null)
                return new AuthResponse { Success = false, Message = "El email ya está registrado." };

            // Validación: El rol debe existir
            if (!await _roleManager.RoleExistsAsync(request.Rol))
            {
                return new AuthResponse { Success = false, Message = $"El rol '{request.Rol}' no existe." };
            }

            var user = new ApplicationUser
            {
                Email = request.Email,
                UserName = request.UserName,
                Nombre = request.Nombre,
                EmailConfirmed = true,
                Rol = request.Rol
            };

            var result = await _userManager.CreateAsync(user, request.Password);

            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                return new AuthResponse { Success = false, Message = errors };
            }

            // Asignar Rol en base de datos de Identity
            await _userManager.AddToRoleAsync(user, request.Rol);

            return new AuthResponse { Success = true, Message = "Usuario registrado correctamente" };
        }

        public async Task<List<string>> GetRolesAsync()
        {
            return await _roleManager.Roles
            .Select(r => r.Name!)
            .ToListAsync();
        }

        private async Task<AuthResponse> GenerateAuthResponseAsync(ApplicationUser user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSettings.Key);

            // Obtenemos los roles del usuario para meterlos en el token
            var roles = await _userManager.GetRolesAsync(user);

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                new Claim(JwtRegisteredClaimNames.Email, user.Email ?? ""),
                new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName ?? ""),
                new Claim("uid", user.Id)
            };

            // Agregamos los roles como Claims
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(_jwtSettings.DurationInMinutes),
                Audience = _jwtSettings.Audience,
                Issuer = _jwtSettings.Issuer,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return new AuthResponse
            {
                Success = true,
                Token = tokenHandler.WriteToken(token),
                Email = user.Email ?? "",
                UserName = user.UserName ?? "",
                Id = user.Id,
                Rol = roles.FirstOrDefault() ?? "SinRol"
            };
        }

        public async Task<List<UserDto>> GetUsersAsync()
        {
            var users = await _userManager.Users.ToListAsync();
            var userDtos = new List<UserDto>();

            foreach (var user in users)
            {
                userDtos.Add(new UserDto
                {
                    Id = user.Id,
                    UserName = user.UserName ?? string.Empty,
                    Email = user.Email ?? string.Empty,
                    Nombre = user.Nombre,
                    Rol = user.Rol
                });
            }

            return userDtos;
        }

        public async Task<UserDto?> GetUserByIdAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return null;

            return new UserDto
            {
                Id = user.Id,
                UserName = user.UserName ?? string.Empty,
                Email = user.Email ?? string.Empty,
                Nombre = user.Nombre,
                Rol = user.Rol
            };
        }

        public async Task<AuthResponse> DeleteUserAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return new AuthResponse { Success = false, Message = "Usuario no encontrado." };
            }

            var result = await _userManager.DeleteAsync(user);
            if (!result.Succeeded)
            {
                return new AuthResponse { Success = false, Message = "Error al eliminar usuario." };
            }

            return new AuthResponse { Success = true, Message = "Usuario eliminado correctamente." };
        }

        public async Task<AuthResponse> UpdateUserAsync(UpdateUserRequest request)
        {
            var user = await _userManager.FindByIdAsync(request.Id);
            if (user == null)
            {
                return new AuthResponse { Success = false, Message = "Usuario no encontrado." };
            }

            // Validación: El rol debe existir antes de intentar actualizarlo
            if (!await _roleManager.RoleExistsAsync(request.Rol))
            {
                return new AuthResponse { Success = false, Message = $"El rol '{request.Rol}' no existe en el sistema." };
            }

            user.Nombre = request.Nombre;
            user.Email = request.Email;

            // Actualizar Password si viene informado (no nulo ni vacío)
            if (!string.IsNullOrEmpty(request.Password))
            {
                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                var passResult = await _userManager.ResetPasswordAsync(user, token, request.Password);

                if (!passResult.Succeeded)
                {
                    return new AuthResponse { Success = false, Message = "Error al actualizar contraseña: " + string.Join(", ", passResult.Errors.Select(e => e.Description)) };
                }
            }

            // Actualizar Rol
            // Verificamos si el rol cambió respecto al que tiene guardado
            if (user.Rol != request.Rol)
            {
                // Remover roles anteriores de Identity
                var currentRoles = await _userManager.GetRolesAsync(user);
                if (currentRoles.Any())
                {
                    await _userManager.RemoveFromRolesAsync(user, currentRoles);
                }

                // Agregar nuevo rol a Identity
                await _userManager.AddToRoleAsync(user, request.Rol);

                // Actualizar la columna redundante 'Rol' en la tabla Users
                user.Rol = request.Rol;
            }

            // Guardar cambios en la entidad User
            var result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded)
            {
                return new AuthResponse { Success = false, Message = "Error al actualizar usuario: " + string.Join(", ", result.Errors.Select(e => e.Description)) };
            }

            return new AuthResponse { Success = true, Message = "Usuario actualizado correctamente." };
        }
    }
}