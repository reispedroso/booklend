using booklend.Models;

namespace booklend.Repository.Interfaces
{
    public interface IBookItemRepository
    {
        public Task<BookItem> CreateAsync(BookItem bookItem);

        public Task<List<BookItem>> GetAllAsync();
        public Task<BookItem> GetByIdAsync(Guid bookItemId);
        public Task<BookItem> GetByIdAndAdminAsync(Guid bbId, Guid adminId);
        public Task UpdateAsync(BookItem bookItem);
        public Task DeleteAsync(BookItem bookItem);
    }
}