using booklend.Application.Interfaces;
using booklend.Helpers;
using booklend.Models;

namespace booklend.Application.Services
{
    public class AuthService(UserService userService, IJwtGenerator jwtGenerator)
    {
        private readonly UserService _userService = userService;
        private readonly IJwtGenerator _jwtGenerator = jwtGenerator;

        public async Task<string?> LoginAsync(string email, string password)
        {
            var user = await _userService.GetByEmailAsync(email);

            var isPasswordValid = PasswordHasher.VerifyPassword(password, user.PasswordHash!);
            if (!isPasswordValid)
            {
                throw new Exception("Password invalid");
            }


            var userModel = new User
            {
                Id = user.Id,
                Email = user.Email,
                RoleId = user.RoleId
            };

            return _jwtGenerator.Generate(userModel);
        }
    }
}