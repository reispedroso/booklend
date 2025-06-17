using booklend.Models;

namespace booklend.Repository.Interfaces
{
    public interface ICategoryRepository
    {

        public Task<Category> CreateAsync(Category category);
        public Task<List<Category>> GetAllAsync();
        public Task<Category> GetByIdAsync(Guid id);
        public Task<Category> GetByNameAsync(string name);
        public Task UpdateAsync(Category category);
        public Task DeleteAsync(Category category);
    }
}