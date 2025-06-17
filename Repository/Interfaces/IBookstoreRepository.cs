using booklend.Models;

namespace booklend.Repository.Interfaces
{
    public interface IBookstoreRepository
    {
        public Task<Bookstore> CreateAsync(Bookstore bookstore);
        public Task<List<Bookstore>> GetAllAsync();
        public Task<Bookstore> GetByIdAsync(Guid bookstoreId);
        public Task<Bookstore> GetByAdminIdAsync(Guid adminId);
        
        public Task<Bookstore> GetByNameAsync(string name);
        public Task UpdateAsync(Bookstore bookstore);
        public Task DeleteAsync(Bookstore bookstore);
    }
}