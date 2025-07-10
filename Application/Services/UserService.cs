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
            Id = Guid.NewGuid(),
            Username = dto.Username,
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Email = dto.Email,
            Password = PasswordHasher.HashPassword(dto.Password),
            RoleId = dto.RoleId,
            CreatedAt = DateTime.SpecifyKind(DateTime.UtcNow.AddHours(-3), DateTimeKind.Utc),
            UpdatedAt = null,
            DeletedAt = null
        };

        await _userRepository.CreateAsync(entity);
        return MapToDto(entity);

    }

    public async Task<IEnumerable<UserReadDto>> GetAllAsync()
    {
        var entities = await _userRepository.GetAllAsync() ?? throw new Exception("No user available");
        return entities.Select(MapToDto);

    }

    public async Task<UserReadDto> GetByEmailAsync(string email)
    {
        var entity = await _userRepository.GetByEmailAsync(email)
        ?? throw new Exception("User with that email was not found");
        return MapToDto(entity);
    }

    public async Task<bool> VerifyEmail(string email)
    {
        var entity = await _userRepository.GetByEmailAsync(email);
        if (entity is null)
        {
            return false;
        }
        return true;
    }

    public async Task<UserReadDto> GetByIdAsync(Guid id)
    {
        var entity = await _userRepository.GetByIdAsync(id)
        ?? throw new Exception($"User with id: {id} not found");

        return MapToDto(entity);
    }

    public async Task<UserReadDto> UpdateAsync(Guid id, UserUpdateDto dto)
    {
        var entity = await _userRepository.GetByIdAsync(id);

        entity.FirstName = dto.FirstName;
        entity.LastName = dto.LastName;
        entity.Email = dto.Email;
        entity.UpdatedAt = DateTime.SpecifyKind(DateTime.UtcNow.AddHours(-3), DateTimeKind.Utc);
        if (!string.IsNullOrWhiteSpace(dto.Password))
            entity.Password = PasswordHasher.HashPassword(dto.Password);

        await _userRepository.UpdateAsync(entity);
        return MapToDto(entity);
    }

    public async Task DeleteAsync(Guid id)
    {
        var entity = await _userRepository.GetByIdAsync(id);

        entity.DeletedAt = DateTime.SpecifyKind(DateTime.UtcNow.AddHours(-3), DateTimeKind.Utc);
        await _userRepository.UpdateAsync(entity);
    }
    private static UserReadDto MapToDto(User u) => new()
    {
        Id = u.Id,
        Username = u.Username!,
        FirstName = u.FirstName!,
        LastName = u.LastName,
        PasswordHash = u.Password,
        Email = u.Email!,
        RoleId = u.RoleId
    };
}