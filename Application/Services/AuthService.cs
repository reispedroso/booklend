using System.Security.Claims;
using booklend.Application.DTOs.User;
using booklend.Application.Interfaces;
using booklend.Helpers;
using booklend.Models;
using booklend.Repository;

namespace booklend.Application.Services
{
    public class AuthService(UserService userService, IJwtGenerator jwtGenerator, RoleService roleService)
    {
        private readonly UserService _userService = userService;
        private readonly IJwtGenerator _jwtGenerator = jwtGenerator;
        private readonly RoleService _roleService = roleService;

        public async Task<string?> LoginAsync(string email, string password)
        {
            var user = await _userService.GetByEmailAsync(email);

            var isPasswordValid = PasswordHasher.VerifyPassword(password, user.PasswordHash!);
            if (!isPasswordValid)
            {
                throw new Exception("Password invalid");
            }

            var UserReadDto = new UserReadDto
            {
                Id = user.Id,
                Email = user.Email,
                RoleId = user.RoleId
            };

            return await _jwtGenerator.GenerateTokenAsync(UserReadDto);
        }

        public async Task<AuthResponseDTO> RegisterAsync(UserCreateDto dto)
        {
            var emailExist = await _userService.VerifyEmail(dto.Email);
            if (emailExist)
                throw new Exception("Email already registered");

            var clientRole = await _roleService.GetRoleEntityByNameAsync("Client");

            var newUserDto = new UserCreateDto
            {
                Id = Guid.NewGuid(),
                Username = dto.Username,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                Password = PasswordHasher.HashPassword(dto.Password),
                RoleId = clientRole.Id
            };

            await _userService.CreateAsync(newUserDto);

            var userReadDto = new UserReadDto
            {
                Id = newUserDto.Id,
                Username = newUserDto.Username,
                RoleId = newUserDto.RoleId
            };

            var token = await _jwtGenerator.GenerateTokenAsync(userReadDto);

            return new AuthResponseDTO
            {
                Token = token,
                User = userReadDto
            };

        }


        public async Task<UserReadDto?> GetUserFromToken(string token)
        {
            var claims = _jwtGenerator.ValidateToken(token);
            if (claims == null) return null;

            var userIdStr = claims.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdStr)) return null;

            if (!Guid.TryParse(userIdStr, out var userId)) return null;

            var user = await _userService.GetByIdAsync(userId);
            return user;
        }

    }
}