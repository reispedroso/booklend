using booklend.Application.DTOs.BookRating;
using booklend.Models;
using booklend.Repository.Interfaces;

namespace booklend.Application.Services
{
    public class BookRatingService(IBookRatingRepository repository)
    {
        private readonly IBookRatingRepository _repository = repository;

        public async Task<BookRatingReadDto> RateBook(BookRatingCreateDto rating)
        {
            var entity = new BookRating
            {
                BookId = rating.BookId,
                Note = rating.Note
            };

            await _repository.RateBookAsync(entity);
            return MapToDto(entity);

        }

        public async Task<decimal> GetAverageRating(Guid bookId)
        {
            var note = await _repository.GetAverageRatingAsync(bookId);
            return note;
        }

        private static BookRatingReadDto MapToDto(BookRating b) => new()
        {
            Id = b.Id,
            BookId = b.BookId,
            Note = b.Note
        };
    }
}