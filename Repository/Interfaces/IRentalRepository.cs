using booklend.Models;

namespace booklend.Repository.Interfaces
{
    public interface IRentalRepository
    {
        public Task<Rental> CreateAsync(Rental rental);

        public Task<List<Rental>> GetAllAsync();
        public Task<List<Rental>> GetAllByUserIdAsync(Guid userId);
        public Task UpdateAsync(Rental rental);
        public Task DeleteAsync(Rental rental);
    }
}