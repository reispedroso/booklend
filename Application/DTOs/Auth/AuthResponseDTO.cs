using booklend.Application.DTOs.User;

public class AuthResponseDTO
{
    public string? Token { get; set; }
    public UserReadDto? User { get; set; }
}