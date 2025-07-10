using booklend.Database;
using booklend.Models;
using booklend.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace booklend.Repository
{
    public class RoleRepository(AppDbContext context) : IRoleRepository
    {
        private readonly AppDbContext _context = context;

        public async Task<Role?> GetByNameAsync(string name)
        {
            return await _context.Roles.FirstOrDefaultAsync(rl => rl.RoleName == name);
        }

        public async Task<Role?> GetNameByIdAsync(Guid id)
        {
             return await _context.Roles.FirstOrDefaultAsync(rl => rl.Id == id);
        }



      
    }
}