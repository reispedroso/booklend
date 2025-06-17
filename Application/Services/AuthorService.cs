using booklend.Application.DTOs.Author;
using booklend.Models;
using booklend.Repository.Interfaces;

namespace booklend.Application.Services
{
    public class AuthorService(IAuthorRepository authorsRepository)
    {
        private readonly IAuthorRepository _authorsRepository = authorsRepository;

        public async Task<AuthorReadDTO> CreateAsync(AuthorCreateDto dto)
        {
            if (await _authorsRepository.GetByNameAsync(dto.FirstName, dto.LastName) is not null)
                throw new Exception($"Author with '{dto.FirstName} {dto.LastName}' already registered");

            var entity = new Author
            {
                Id = Guid.NewGuid(),
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Nationality = dto.Nationality,
                CreatedAt = DateTime.SpecifyKind(DateTime.UtcNow.AddHours(-3), DateTimeKind.Utc),
                UpdatedAt = null,
                DeletedAt = null
            };

            await _authorsRepository.CreateAsync(entity);
            return MapToDto(entity);
        }

        public async Task<IEnumerable<AuthorReadDTO>> GetAllAsync()
        {
            var entities = await _authorsRepository.GetAllAsync()
            ?? throw new Exception("Not a single author :/");

            return entities.Select(MapToDto);
        }

        public async Task<AuthorReadDTO> GetByIdAsync(Guid id)
        {
            var entity = await _authorsRepository.GetByIdAsync(id)
            ?? throw new Exception($"Author with id: {id} not found");
            return MapToDto(entity);
        }
        private static AuthorReadDTO MapToDto(Author a) => new()
        {
            Id = a.Id,
            FirstName = a.FirstName,
            LastName = a.LastName,
            Nationality = a.Nationality,
            CreatedAt = a.CreatedAt,
            UpdatedAt = a.UpdatedAt,
            DeletedAt = a.DeletedAt
        };
    }
}