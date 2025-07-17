using booklend.Models;

namespace booklend.Repository.Interfaces
{
    public interface IBookRatingRepository
    {
        public Task<BookRating> RateBookAsync(BookRating rating);
        public Task<decimal> GetAverageRatingAsync(Guid id);
    }
}