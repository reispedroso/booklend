using booklend.Models;

namespace booklend.Repository.Interfaces
{
    public interface IRoleRepository
    {
        public Task<Role> GetNameById(Guid id);
    }
}