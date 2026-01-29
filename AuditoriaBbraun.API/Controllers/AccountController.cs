using AuditoriaBbraun.Application.Interfaces.Identity;
using AuditoriaBbraun.Application.DTOs.Account;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace AuditoriaBbraun.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AccountController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<ActionResult<AuthResponse>> Login(LoginRequest request)
        {
            var result = await _authService.LoginAsync(request);
            if (!result.Success)
            {
                return Unauthorized(result);
            }
            return Ok(result);
        }

        //  ENDPOINTS DE ADMINISTRACION DE USUARIOS


        [Authorize(Roles = "Admin")]
        [HttpPost("register")]
        public async Task<ActionResult<AuthResponse>> Register(RegisterRequest request)
        {
            var result = await _authService.RegisterAsync(request);
            return Ok(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("users")]
        public async Task<ActionResult<List<UserDto>>> GetUsers()
        {
            var users = await _authService.GetUsersAsync();
            return Ok(users);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("roles")]
        public async Task<ActionResult<List<string>>> GetRoles()
        {
            var roles = await _authService.GetRolesAsync();
            return Ok(roles);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("users/{id}")]
        public async Task<ActionResult<UserDto>> GetUserById(string id)
        {
            var user = await _authService.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound("Usuario no encontrado.");
            }
            return Ok(user);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("users")]
        public async Task<ActionResult<AuthResponse>> UpdateUser(UpdateUserRequest request)
        {
            var result = await _authService.UpdateUserAsync(request);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("users/{id}")]
        public async Task<ActionResult<AuthResponse>> DeleteUser(string id)
        {
            var result = await _authService.DeleteUserAsync(id);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
    }
}