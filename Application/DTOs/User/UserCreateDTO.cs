using System.ComponentModel.DataAnnotations;

namespace booklend.Application.DTOs.User;

public class UserCreateDto
{
    public Guid Id { get; set; }
    [Required]
    public string Username { get; set; } = null!;

    [Required]
    public string FirstName { get; set; } = null!;

    public string? LastName { get; set; }

    [Required, EmailAddress]
    public string Email { get; set; } = null!;

    [Required, MinLength(6)]
    public string Password { get; set; } = null!;
    
    [Required]
    public Guid RoleId { get; set; }
}
