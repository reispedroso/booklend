using booklend.Database;
using booklend.Models;
using booklend.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace booklend.Repository
{

    public class BookItemRepository(AppDbContext context) : IBookItemRepository
    {
        private readonly AppDbContext _context = context;

        public async Task<BookItem> CreateAsync(BookItem BookItem)
        {
            await _context.BookItems.AddAsync(BookItem);
            await _context.SaveChangesAsync();
            return BookItem;
        }

        public async Task<List<BookItem>> GetAllAsync()
        {
            return await _context.BookItems
                .ToListAsync();
        }

        public async Task<BookItem?> GetByIdAsync(Guid BookItemId)
        {
            return await _context.BookItems
            .FirstOrDefaultAsync(bb => bb.Id == BookItemId);
        }

           public async Task<BookItem?> GetByIdAndAdminAsync(Guid bbId, Guid adminId)
        {
            return await _context.BookItems
                .Include(bb => bb.Bookstore)
                .FirstOrDefaultAsync(bb =>
                    bb.Id == bbId &&
                    bb.Bookstore.AdminId == adminId);
        }

        public async Task UpdateAsync(BookItem BookItem)
        {
            _context.BookItems.Update(BookItem);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(BookItem BookItem)
        {
            _context.Update(BookItem);
            await _context.SaveChangesAsync();
        }
    }
}