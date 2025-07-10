using booklend.Models;

namespace booklend.Repository.Interfaces
{
    public interface IRoleRepository
    {
        public Task<Role> GetNameByIdAsync(Guid id);
        public Task<Role> GetByNameAsync(string name);
    }
}