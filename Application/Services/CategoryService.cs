using System.Runtime.Serialization;
using booklend.Application.DTOs.Category;
using booklend.Models;
using booklend.Repository.Interfaces;

namespace booklend.Application.Services;

public class CategoryService(ICategoryRepository categoryRepository)
{
    private readonly ICategoryRepository _categoryRepository = categoryRepository;

    public async Task<CategoryReadDto> CreateAsync(CategoryCreateDto dto)
    {
        if (await _categoryRepository.GetByNameAsync(dto.Name.ToLower()) is not null)
            throw new Exception($"Category with {dto.Name} already registered");

        var entity = new Category
        {
            Id = Guid.NewGuid(),
            Name = dto.Name,
            CreatedAt = DateTime.SpecifyKind(DateTime.UtcNow.AddHours(-3), DateTimeKind.Utc),
            UpdatedAt = null,
            DeletedAt = null
        };

        await _categoryRepository.CreateAsync(entity);
        return MapToDto(entity);
    }

    public async Task<IEnumerable<CategoryReadDto>> GetAllAsync()
    {
        var entities = await _categoryRepository.GetAllAsync()
        ?? throw new Exception("Not a single category :/");

        return entities.Select(MapToDto);
    }

    public async Task<CategoryReadDto> GetByIdAsync(Guid id)
    {
        var entity = await _categoryRepository.GetByIdAsync(id)
        ?? throw new Exception($"Category with id: {id} not found");
        return MapToDto(entity);
    }
    private static CategoryReadDto MapToDto(Category c) => new()
    {
        Id = c.Id,
        Name = c.Name
    };
}