using System.Security.Claims;
using booklend.Application.DTOs.User;
using booklend.Models;

namespace booklend.Application.Interfaces
{
   public interface IJwtGenerator
{
    Task<string> GenerateTokenAsync(UserReadDto userReadDto);
    ClaimsPrincipal? ValidateToken(string token);
}

}