using booklend.Application.DTOs.Bookstore;
using booklend.Models;
using booklend.Repository.Interfaces;

namespace booklend.Application.Services
{
    public class BookstoreService(IBookstoreRepository bookstoreRepository)
    {
        private readonly IBookstoreRepository _bookstoreRepository = bookstoreRepository;

        public async Task<BookstoreReadDto> CreateAsync(BookstoreCreateDto dto, Guid adminId)
        {
            if (await _bookstoreRepository.GetByNameAsync(dto.BookstoreName) is not null) throw new Exception($"Bookstore with name {dto.BookstoreName} already registered");

            var entity = new Bookstore
            {
                Id = Guid.NewGuid(),
                BookstoreName = dto.BookstoreName,
                AdminId = adminId,
                Street = dto.Street,
                City = dto.City,
                State = dto.State,
                ZipCode = dto.ZipCode,
                CreatedAt = DateTime.SpecifyKind(DateTime.UtcNow.AddHours(-3), DateTimeKind.Utc),
                UpdatedAt = null,
                DeletedAt = null
            };

            await _bookstoreRepository.CreateAsync(entity);
            return MapToDto(entity);
        }

        public async Task<BookstoreReadDto> GetByAdminId(Guid adminId)
        {
            var bookstore = await _bookstoreRepository.GetByAdminIdAsync(adminId)
            ?? throw new Exception("Bookstore by Id not found!");

            return MapToDto(bookstore);
        }
        public async Task<IEnumerable<BookstoreReadDto>> GetAllAsync()
        {
            var entities = await _bookstoreRepository.GetAllAsync() ?? throw new Exception("No bookstores available");
            return entities.Select(MapToDto);
        }

        public async Task<BookstoreReadDto> GetByIdAsync(Guid id)
        {
            var entity = await _bookstoreRepository.GetByIdAsync(id)
            ?? throw new Exception($"Bookstore with id: {id} not found");

            return MapToDto(entity);
        }

        public async Task<BookstoreReadDto> GetByNameAsync(string name)
        {
            var dto = await _bookstoreRepository.GetByNameAsync(name)
            ?? throw new Exception($"Bookstore: {name} not founded");
            return MapToDto(dto);
        }

        public async Task<BookstoreReadDto> UpdateAsync(Guid id, BookstoreUpdateDto dto)
        {
            var entity = await _bookstoreRepository.GetByIdAsync(id);

            entity.BookstoreName = dto.BookstoreName;
            entity.Street = dto.Street;
            entity.City = dto.City;
            entity.State = dto.State;
            entity.ZipCode = dto.ZipCode;

            await _bookstoreRepository.UpdateAsync(entity);
            return MapToDto(entity);
        }

        public async Task DeleteAsync(Guid id)
        {
            var entity = await _bookstoreRepository.GetByIdAsync(id);

            entity.DeletedAt = DateTime.SpecifyKind(DateTime.UtcNow.AddHours(-3), DateTimeKind.Utc);
            await _bookstoreRepository.UpdateAsync(entity);
        }
        private static BookstoreReadDto MapToDto(Bookstore b) => new()
        {
            Id = b.Id,
            BookstoreName = b.BookstoreName,
            AdminId = b.AdminId,
            City = b.City,
            State = b.State,
            CreatedAt = b.CreatedAt,
            UpdatedAt = b.UpdatedAt
        };
    }
}