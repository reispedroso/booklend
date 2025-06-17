using booklend.Application.Services;
using Microsoft.AspNetCore.Mvc;
using booklend.Application.DTOs.User;

namespace booklend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController(AuthService authService) : ControllerBase
    {
        private readonly AuthService _authService = authService;

        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync([FromBody] LoginRequest login)
        {
            var token = await _authService.LoginAsync(login.Email, login.Password);

            if (token is null)
            {
                return Unauthorized("Not valid credentials");
            }

            return Ok(new { token });
        }

    }
}