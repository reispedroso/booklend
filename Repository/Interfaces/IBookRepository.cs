using booklend.Models;

namespace booklend.Repository.Interfaces
{
    public interface IBookRepository
    {

        public Task<Book> CreateAsync(Book book);
        public Task<List<Book>> GetAllAsync();
        public Task<Book> GetByIdAsync(Guid bookId);
        public Task<Book> GetByNameAsync(string name);
        public Task UpdateAsync(Book book);
        public Task DeleteAsync(Book book);
    }
}