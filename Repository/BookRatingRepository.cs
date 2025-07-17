using booklend.Database;
using booklend.Models;
using booklend.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace booklend.Repository
{
    public class BookRatingRepository(AppDbContext context) : IBookRatingRepository
    {
        private readonly AppDbContext _context = context;
        public async Task<BookRating> RateBookAsync(BookRating rating)
        {
            await _context.BookRatings.AddAsync(rating);
            await _context.SaveChangesAsync();
            return rating;
        }

        public async Task<decimal> GetAverageRatingAsync(Guid bookId)
        {
            var averageNote = await _context.BookRatings.Where(br => br.BookId == bookId).AverageAsync(br => br.Note);
            return averageNote;
        }
    }
}