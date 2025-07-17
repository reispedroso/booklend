using booklend.Application.DTOs.BookItem;
using booklend.Models;
using booklend.Repository.Interfaces;

namespace booklend.Application.Services
{
    public class BookItemService(IBookItemRepository bookItemRepository, BookstoreService bookstoreService)
    {
        private readonly IBookItemRepository _bookItemRepository = bookItemRepository;
        private readonly BookstoreService _bookstoreService = bookstoreService;

        public async Task<BookItemReadDto> CreateAsync(BookItemCreateDto dto, Guid userId)
        {
            var userBookstore = await _bookstoreService.GetByAdminId(userId);

            var entity = new BookItem
            {
                Id = Guid.NewGuid(),
                BookId = dto.BookId,
                BookstoreId = userBookstore.Id,
                Quantity = dto.Quantity,
                Condition = dto.Condition,
                CreatedAt = DateTime.SpecifyKind(DateTime.UtcNow.AddHours(-3), DateTimeKind.Utc),
                UpdatedAt = null,
                DeletedAt = null
            };
            await _bookItemRepository.CreateAsync(entity);
            return MapToDto(entity);
        }
        
        public async Task<BookItemReadDto> GetByIdAsync(Guid id)
        {
            var bookItemById = await _bookItemRepository.GetByIdAsync(id)
            ?? throw new Exception($"Book not found in this Bookstore! Book id: {id}");

            return MapToDto(bookItemById);
        }

        public async Task<BookItemReadDto> UpdateAsync(
       Guid bookItemId,
       BookItemUpdateDto dto)
        {

            var entity = await _bookItemRepository.GetByIdAsync(bookItemId);
            
            entity.Quantity = dto.Quantity;
            entity.UpdatedAt = DateTime.SpecifyKind(DateTime.UtcNow.AddHours(-3), DateTimeKind.Utc);

            await _bookItemRepository.UpdateAsync(entity);
            return MapToDto(entity);
        }

        private static BookItemReadDto MapToDto(BookItem b) => new()
        {
            Id = b.Id,
            BookId = b.BookId,
            BookstoreId = b.BookstoreId,
            Quantity = b.Quantity,
            Condition = b.Condition,
            CreatedAt = b.CreatedAt,
            UpdatedAt = b.UpdatedAt,
            DeletedAt = b.DeletedAt
        };

    }
}