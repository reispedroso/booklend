using booklend.Database;
using booklend.Models;
using booklend.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace booklend.Repository
{

    public class BookstoreBookRepository(AppDbContext context) : IBookstoreBookRepository
    {
        private readonly AppDbContext _context = context;

        public async Task<BookstoreBook> CreateAsync(BookstoreBook bookstoreBook)
        {
            await _context.BookstoreBooks.AddAsync(bookstoreBook);
            await _context.SaveChangesAsync();
            return bookstoreBook;
        }

        public async Task<List<BookstoreBook>> GetAllAsync()
        {
            return await _context.BookstoreBooks
                .ToListAsync();
        }

        public async Task<BookstoreBook?> GetByIdAsync(Guid bookstoreBookId)
        {
            return await _context.BookstoreBooks
            .FirstOrDefaultAsync(bb => bb.Id == bookstoreBookId);
        }

        public async Task UpdateAsync(BookstoreBook bookstoreBook)
        {
            _context.BookstoreBooks.Update(bookstoreBook);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(BookstoreBook bookstoreBook)
        {
            _context.Update(bookstoreBook);
            await _context.SaveChangesAsync();
        }
    }
}