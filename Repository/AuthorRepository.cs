using booklend.Database;
using booklend.Models;
using booklend.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace booklend.Repository
{

    public class AuthorRepository(AppDbContext context) : IAuthorRepository
    {
        private readonly AppDbContext _context = context;

        public async Task<Author> CreateAsync(Author author)
        {
            await _context.Authors.AddAsync(author);
            await _context.SaveChangesAsync();
            return author;
        }

        public async Task<List<Author>> GetAllAsync()
        {
            return await _context.Authors
            .ToListAsync();

        }

        public async Task<Author?> GetByIdAsync(Guid authorId)
        {
            return await _context.Authors
            .FirstOrDefaultAsync(a => a.Id == authorId);
        }

        public async Task<Author?> GetByNameAsync(string FirstName, string LastName)
        {
            return await _context.Authors
                  .FirstOrDefaultAsync(a => a.FirstName == FirstName && a.LastName == LastName);

        }

        public async Task UpdateAsync(Author author)
        {
            _context.Update(author);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(Author author)
        {
            _context.Authors.Update(author);
            await _context.SaveChangesAsync();
        }
    }
}