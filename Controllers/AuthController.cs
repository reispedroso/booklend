using booklend.Application.Services;
using Microsoft.AspNetCore.Mvc;
using booklend.Application.DTOs.User;
using Microsoft.AspNetCore.Authorization;
using booklend.Models;

namespace booklend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController(AuthService authService) : ControllerBase
    {
        private readonly AuthService _authService = authService;

        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync([FromBody] UserLoginDto login)
        {
            var token = await _authService.LoginAsync(login.Email, login.Password);

            if (token is null)
            {
                return Unauthorized("Not valid credentials");
            }
            return Ok(new
            {
                message = "Login realizado com sucesso",
                token
            }
            );
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserCreateDto dto)
        {
            var authResponse = await _authService.RegisterAsync(dto);
            return Ok(new
            {
                message = "Registro realizado com sucesso",
                token = authResponse.Token,
                user = authResponse.User
            });
        }


        [Authorize(Roles = "Admin,Client")]
        [HttpGet("me")]
        public async Task<IActionResult> Me()
        {
            var authHeader = Request.Headers["Authorization"].FirstOrDefault();
            if (string.IsNullOrWhiteSpace(authHeader) || !authHeader.StartsWith("Bearer "))
                return Unauthorized("Theres is no token here");

            var token = authHeader.Substring("Bearer ".Length).Trim();
            var user = await _authService.GetUserFromToken(token);
            if (user == null)
                return Unauthorized("Token inválido");

            return Ok(new
            {
                user.Id,
                user.RoleId,
                user.Email,
                user.FirstName,
                user.LastName
            });
        }

    }
}