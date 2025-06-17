using System.ComponentModel.DataAnnotations;

namespace booklend.Application.DTOs.User;

public class UserUpdateDto
{
    [Required]
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; }  = null!;

    [Required, EmailAddress]
    public string Email { get; set; } = null!;
    [MinLength(6)]
    public string? Password { get; set; }
}
