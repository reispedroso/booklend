using System.Security.Claims;
using System.Text;
using booklend.Application.Interfaces;
using booklend.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace booklend.Application.Services.Token;

public sealed class JwtGenerator(IOptions<JwtSettings> opt, RoleService roleService) : IJwtGenerator
{
    private readonly JwtSettings _settings = opt.Value;
    private readonly RoleService _roleService = roleService;

    public string Generate(User user)
    {
        var roleDto = _roleService.GetNameById(user.RoleId).GetAwaiter().GetResult();

        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Email, user.Email ?? string.Empty),
            new Claim(ClaimTypes.Role, roleDto.RoleName ?? "")
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.Key));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _settings.Issuer,
            audience: _settings.Audience,
            claims: claims,
            expires: DateTime.SpecifyKind(DateTime.UtcNow.AddDays(_settings.ExpiresInDays), DateTimeKind.Utc),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
