namespace booklend.Application.DTOs.User;

public class UserLoginDto
{
    public string Email { get; set; } = default!;
    public string Password { get; set; } = default!;
}
