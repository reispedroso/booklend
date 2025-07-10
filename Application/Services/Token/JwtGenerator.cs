using System.Security.Claims;
using System.Text;
using booklend.Application.Interfaces;
using booklend.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using booklend.Application.DTOs.User;

namespace booklend.Application.Services.Token;

public sealed class JwtGenerator(IOptions<JwtSettings> opt, RoleService roleService) : IJwtGenerator
{
    private readonly JwtSettings _settings = opt.Value;
    private readonly RoleService _roleService = roleService;

  public async Task<string> GenerateTokenAsync(UserReadDto userReadDto)
{
    var roleDto = await _roleService.GetNameById(userReadDto.RoleId);

    var claims = new[]
    {
        new Claim(ClaimTypes.NameIdentifier, userReadDto.Id.ToString()),
        new Claim(ClaimTypes.Email, userReadDto.Email ?? string.Empty),
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

    public ClaimsPrincipal? ValidateToken(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(_settings.Key);

        try
        {
            var principal = tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = _settings.Issuer,
                ValidateAudience = false,
                ValidateLifetime = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuerSigningKey = true
            }, out SecurityToken validatedToken);

            return principal;
        }
        catch
        {
            return null;
        }
    }
}