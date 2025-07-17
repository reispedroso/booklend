namespace booklend.Application.DTOs.User;

public class UserReadDto
{
    public Guid Id { get; set; }
    public string Username { get; set; } = default!;
    public string FirstName { get; set; } = default!;
    public string? LastName { get; set; }
    public string Email { get; set; } = default!;
    public Guid RoleId { get; set; }
}
