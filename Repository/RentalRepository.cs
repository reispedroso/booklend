using booklend.Database;
using booklend.Models;
using booklend.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace booklend.Repository
{
    public class RentalRepository(AppDbContext context) : IRentalRepository
    {
        private readonly AppDbContext _context = context;

        public async Task<Rental> CreateAsync(Rental rental)
        {
            await _context.AddAsync(rental);
            await _context.SaveChangesAsync();
            return rental;
        }

        public async Task<List<Rental>> GetAllAsync()
        {
            return await _context.Rentals.ToListAsync();
        }

        public async Task<List<Rental>> GetAllByUserIdAsync(Guid userId)
        {
            return await _context.Rentals
                .Where(r => r.UserId == userId)
                .ToListAsync();
        }

        public async Task UpdateAsync(Rental rental)
        {
            _context.Rentals.Update(rental);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Rental rental)
        {
            _context.Rentals.Update(rental);
            await _context.SaveChangesAsync();
        }
    }
}