using booklend.Database;
using booklend.Models;
using booklend.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace booklend.Repository
{

    public class BookRepository(AppDbContext context) : IBookRepository
    {
        private readonly AppDbContext _context = context;

        public async Task<Book> CreateAsync(Book book)
        {
            await _context.Books.AddAsync(book);
            await _context.SaveChangesAsync();
            return book;
        }
        public async Task<List<Book>> GetAllAsync()
        {
            return await _context.Books
            .ToListAsync();
        }


        public async Task<Book?> GetByIdAsync(Guid bookId)
        {
            return await _context.Books
            .FirstOrDefaultAsync(b => b.Id == bookId);
        }

        public async Task<Book?> GetByNameAsync(string name)
        {
            return await _context.Books
             .FirstOrDefaultAsync(b => b.Title == name);
        }


        public async Task UpdateAsync(Book book)
        {
            _context.Books.Update(book);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Book book)
        {
            _context.Books.Update(book);
            await _context.SaveChangesAsync();
        }
    }
}