using System.Security.Claims;
using booklend.Application.DTOs.User;
using booklend.Application.Interfaces;
using booklend.Helpers;
namespace booklend.Application.Services
{

    public class AuthService(UserService userService, IJwtGenerator jwtGenerator, RoleService roleService)
    {
        private readonly UserService _userService = userService;
        private readonly IJwtGenerator _jwtGenerator = jwtGenerator;
        private readonly RoleService _roleService = roleService;

        public async Task<AuthResponseDTO> RegisterAsync(UserCreateDto dto)
        {
            var emailExists = await _userService.VerifyEmail(dto.Email);
            if (emailExists) throw new Exception("Email already registered");

            var clientRole = await _roleService.GetRoleEntityByNameAsync("Client");
            dto.Id = Guid.NewGuid();
            dto.RoleId = clientRole.Id;

            var user = await _userService.CreateAsync(dto);

            var token = await _jwtGenerator.GenerateTokenAsync(user);

            return new AuthResponseDTO
            {
                Token = token,
                User = user
            };
        }

        public async Task<string> LoginAsync(string email, string password)
        {
            var userEntity = await _userService.GetEntityByEmailAsync(email)
                ?? throw new Exception("User not found");

            var isValid = PasswordHasher.Verify(password, userEntity.Password);
            if (!isValid) throw new Exception("Invalid password");

            var userDto = new UserReadDto
            {
                Id = userEntity.Id,
                Username = userEntity.Username!,
                FirstName = userEntity.FirstName!,
                LastName = userEntity.LastName,
                Email = userEntity.Email!,
                RoleId = userEntity.RoleId
            };

            return await _jwtGenerator.GenerateTokenAsync(userDto);
        }

        public async Task<UserReadDto?> GetUserFromToken(string token)
        {
            var claims = _jwtGenerator.ValidateToken(token);
            if (claims == null) return null;

            var userIdStr = claims.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!Guid.TryParse(userIdStr, out var userId)) return null;

            return await _userService.GetByIdAsync(userId);
        }
    }
}