using booklend.Database;
using booklend.Models;
using booklend.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace booklend.Repository
{

    public class BookstoreRepository(AppDbContext context) : IBookstoreRepository
    {
        private readonly AppDbContext _context = context;

        public async Task<Bookstore> CreateAsync(Bookstore bookstore)
        {
            await _context.Bookstores.AddAsync(bookstore);
            await _context.SaveChangesAsync();

            return bookstore;
        }

        public async Task<List<Bookstore>> GetAllAsync()
        {
            return await _context.Bookstores.ToListAsync();
        }

        public async Task<Bookstore?> GetByIdAsync(Guid bookstoreId)
        {
            return await _context.Bookstores.FirstOrDefaultAsync(bs => bs.Id == bookstoreId);
        }

        public async Task<Bookstore?> GetByNameAsync(string name)
        {
            return await _context.Bookstores.FirstOrDefaultAsync(bs => bs.BookstoreName.ToLower() == name.ToLower());
        }

        public async Task UpdateAsync(Bookstore bookstore)
        {
            _context.Bookstores.Update(bookstore);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(Bookstore bookstore)
        {
            _context.Update(bookstore);
            await _context.SaveChangesAsync();
        }
    }
}