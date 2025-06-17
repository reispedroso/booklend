using booklend.Models;

namespace booklend.Repository.Interfaces
{
    public interface IUserRepository
    {
        public Task<User> CreateAsync(User user);
        public Task<List<User>> GetAllAsync();
        public Task<User> GetByIdAsync(Guid userId);
        public Task<User> GetByEmailAsync(string email);
        public Task UpdateAsync(User user);
        public Task DeleteAsync(User user);
    }
}