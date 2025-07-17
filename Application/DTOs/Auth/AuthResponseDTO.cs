namespace booklend.Application.DTOs.User;

public class AuthResponseDTO
{
    public string Token { get; set; } = default!;
    public UserReadDto User { get; set; } = default!;
}
