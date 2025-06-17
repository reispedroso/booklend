using booklend.Models;

namespace booklend.Repository.Interfaces
{
    public interface IAuthorRepository
    {
        public Task<Author> CreateAsync(Author author);
        public Task<List<Author>> GetAllAsync();
        public Task<Author> GetByIdAsync(Guid authorId);
        public Task<Author> GetByNameAsync(string FirstName, string LastName);
        public Task UpdateAsync(Author author);
        public Task DeleteAsync(Author author);
    }
}