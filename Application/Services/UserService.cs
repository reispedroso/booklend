using booklend.Models;
using booklend.Repository.Interfaces;
using booklend.Application.DTOs.User;
using booklend.Helpers;

namespace booklend.Application.Services;

public class UserService(IUserRepository userRepository)
{
    private readonly IUserRepository _userRepository = userRepository;

    public async Task<UserReadDto> CreateAsync(UserCreateDto dto)
    {
        if (await _userRepository.GetByEmailAsync(dto.Email) is not null)
            throw new Exception($"E-mail {dto.Email} already registered");

        var entity = new User
        {
            Id = dto.Id,
            Username = dto.Username,
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Email = dto.Email,
            Password = PasswordHasher.Hash(dto.Password),
            RoleId = dto.RoleId,
            CreatedAt = DateTime.UtcNow,
        };

        await _userRepository.CreateAsync(entity);
        return MapToDto(entity);
    }

    public async Task<IEnumerable<UserReadDto>> GetAllAsync()
    {
        var users = await _userRepository.GetAllAsync();
        return users.Select(u => new UserReadDto
        {
            Id = u.Id,
            Username = u.Username!,
            FirstName = u.FirstName!,
            LastName = u.LastName,
            Email = u.Email!,
            RoleId = u.RoleId
        });
    }

    public async Task<UserReadDto> UpdateAsync(Guid id, UserUpdateDto dto)
    {
        var user = await _userRepository.GetByIdAsync(id) ?? throw new Exception("User not found");

        user.FirstName = dto.FirstName;
        user.LastName = dto.LastName;
        user.Email = dto.Email;
        user.UpdatedAt = DateTime.UtcNow;

        if (!string.IsNullOrWhiteSpace(dto.Password))
        {
            user.Password = PasswordHasher.Hash(dto.Password);
        }

        await _userRepository.UpdateAsync(user);

        return new UserReadDto
        {
            Id = user.Id,
            Username = user.Username!,
            FirstName = user.FirstName!,
            LastName = user.LastName,
            Email = user.Email!,
            RoleId = user.RoleId
        };
    }

    public async Task DeleteAsync(Guid id)
    {
        var user = await _userRepository.GetByIdAsync(id) ?? throw new Exception("User not found");

        user.DeletedAt = DateTime.UtcNow;

        await _userRepository.UpdateAsync(user);
    }
    
    public async Task<User?> GetEntityByEmailAsync(string email)
    {
        return await _userRepository.GetByEmailAsync(email);
    }

    public async Task<UserReadDto> GetByIdAsync(Guid id)
    {
        var entity = await _userRepository.GetByIdAsync(id)
            ?? throw new Exception("User not found");

        return MapToDto(entity);
    }

    public async Task<bool> VerifyEmail(string email)
    {
        return await _userRepository.GetByEmailAsync(email) is not null;
    }

    private static UserReadDto MapToDto(User u) => new()
    {
        Id = u.Id,
        Username = u.Username!,
        FirstName = u.FirstName!,
        LastName = u.LastName,
        Email = u.Email!,
        RoleId = u.RoleId
    };
}
