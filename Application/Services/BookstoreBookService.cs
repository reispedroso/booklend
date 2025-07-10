using booklend.Application.DTOs.BookstoreBook;
using booklend.Models;
using booklend.Repository.Interfaces;

namespace booklend.Application.Services
{
    public class BookstoreBookService(IBookstoreBookRepository bookstoreBookRepository, BookstoreService bookstoreService)
    {
        private readonly IBookstoreBookRepository _bookstoreBookRepository = bookstoreBookRepository;
        private readonly BookstoreService _bookstoreService = bookstoreService;

        public async Task<BookstoreBookReadDto> CreateAsync(BookstoreBookCreateDto dto, Guid userId)
        {
            var userBookstore = await _bookstoreService.GetByAdminId(userId);

            var entity = new BookstoreBook
            {
                Id = Guid.NewGuid(),
                BookId = dto.BookId,
                BookstoreId = userBookstore.Id,
                Quantity = dto.Quantity,
                CreatedAt = DateTime.SpecifyKind(DateTime.UtcNow.AddHours(-3), DateTimeKind.Utc),
                UpdatedAt = null,
                DeletedAt = null
            };
            await _bookstoreBookRepository.CreateAsync(entity);
            return MapToDto(entity);
        }
        
        public async Task<BookstoreBookReadDto> GetByIdAsync(Guid id)
        {
            var bookstoreBookById = await _bookstoreBookRepository.GetByIdAsync(id)
            ?? throw new Exception($"Book not found in this Bookstore! Book id: {id}");

            return MapToDto(bookstoreBookById);
        }

        public async Task<BookstoreBookReadDto> UpdateAsync(
       Guid bookstoreBookId,
       BookstoreBookUpdateDto dto)
        {

            var entity = await _bookstoreBookRepository.GetByIdAsync(bookstoreBookId);
            
            entity.Quantity = dto.Quantity;
            entity.UpdatedAt = DateTime.SpecifyKind(DateTime.UtcNow.AddHours(-3), DateTimeKind.Utc);

            await _bookstoreBookRepository.UpdateAsync(entity);
            return MapToDto(entity);
        }

        private static BookstoreBookReadDto MapToDto(BookstoreBook b) => new()
        {
            Id = b.Id,
            BookId = b.BookId,
            BookstoreId = b.BookstoreId,
            Quantity = b.Quantity,
            CreatedAt = b.CreatedAt,
            UpdatedAt = b.UpdatedAt,
            DeletedAt = b.DeletedAt
        };

    }
}