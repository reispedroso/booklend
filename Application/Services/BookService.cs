using System.Security.Cryptography.X509Certificates;
using booklend.Application.DTOs.Book;
using booklend.Models;
using booklend.Repository.Interfaces;

namespace booklend.Application.Services
{
    public class BookService(IBookRepository bookRepository)
    {
        private readonly IBookRepository _bookRepository = bookRepository;

        public async Task<BookReadDto> CreateAsync(BookCreateDto dto)
        {
            if (await _bookRepository.GetByNameAsync(dto.Title) is not null) throw new Exception($"Book with name {dto.Title} already registered");

            var entity = new Book
            {
                Id = Guid.NewGuid(),
                Title = dto.Title,
                Description = dto.Description,
                AuthorId = dto.AuthorId,
                CategoryId = dto.CategoryId,
                CreatedAt = DateTime.SpecifyKind(DateTime.UtcNow.AddHours(-3), DateTimeKind.Utc),
                UpdatedAt = null,
                DeletedAt = null
            };

            await _bookRepository.CreateAsync(entity);
            return MapToDto(entity);
        }
        public async Task<IEnumerable<BookReadDto>> GetAllAsync()
        {
            var entities = await _bookRepository.GetAllAsync() ?? throw new Exception("No bookstores available");
            return entities.Select(MapToDto);
        }

        public async Task<BookReadDto> GetByIdAsync(Guid id)
        {
            var dto = await _bookRepository.GetByIdAsync(id)
            ?? throw new Exception($"Book with {id} not found");
            return MapToDto(dto);
        }

        public async Task<BookReadDto> GetByNameAsync(string name)
        {
            var dto = await _bookRepository.GetByNameAsync(name)
            ?? throw new Exception($"Book: {name} not founded");
            return MapToDto(dto);
        }

        public async Task<BookReadDto> UpdateAsync(Guid id, BookUpdateDto dto)
        {
            var entity = await _bookRepository.GetByIdAsync(id);

            entity.Title = dto.Title;
            entity.Description = dto.Description;
            entity.AuthorId = dto.AuthorId;
            entity.CategoryId = dto.CategoryId;
            entity.UpdatedAt = DateTime.SpecifyKind(DateTime.UtcNow.AddHours(-3), DateTimeKind.Utc);

            await _bookRepository.UpdateAsync(entity);
            return MapToDto(entity);
        }
        public async Task DeleteAsync(Guid id)
        {
            var entity = await _bookRepository.GetByIdAsync(id);

            entity.DeletedAt = DateTime.SpecifyKind(DateTime.UtcNow.AddHours(-3), DateTimeKind.Utc);
            await _bookRepository.UpdateAsync(entity);
        }

        private static BookReadDto MapToDto(Book b) => new()
        {
            Id = b.Id,
            Title = b.Title,
            Description = b.Description,
            AuthorId = b.AuthorId,
            CategoryId = b.CategoryId,
            CreatedAt = b.CreatedAt,
            UpdatedAt = b.UpdatedAt
        };
    }
}