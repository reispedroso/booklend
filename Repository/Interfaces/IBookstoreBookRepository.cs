using booklend.Models;

namespace booklend.Repository.Interfaces
{

    public interface IBookstoreBookRepository
    {
        public Task<BookstoreBook> CreateAsync(BookstoreBook bookstoreBook);

        public Task<List<BookstoreBook>> GetAllAsync();
        public Task<BookstoreBook> GetByIdAsync(Guid bookstoreBookId);
        public Task UpdateAsync(BookstoreBook bookstoreBook);
        public Task DeleteAsync(BookstoreBook bookstoreBook);
    }
}