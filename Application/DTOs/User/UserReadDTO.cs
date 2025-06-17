namespace booklend.Application.DTOs.User;

public class UserReadDto
{
    public Guid Id { get; set; }
    public string Username { get; set; } = null!;
    public string FirstName { get; set; } = null!;
    public string? LastName { get; set; }
    public string? PasswordHash { get; set;}
    public string Email { get; set; } = null!;
    public Guid RoleId { get; set; }

    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }
}
