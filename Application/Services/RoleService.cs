using booklend.Application.DTOs.Role;
using booklend.Models;
using booklend.Repository.Interfaces;

namespace booklend.Application.Services
{
    public class RoleService(IRoleRepository roleRepository)
    {
        private readonly IRoleRepository _roleRepository = roleRepository;

        public async Task<RoleReadDTO> GetNameById(Guid id)
        {
            var entity = await _roleRepository.GetNameByIdAsync(id)
            ?? throw new Exception("Role not found");

            return MapToDto(entity);
        }

        public async Task<Role> GetRoleEntityByNameAsync(string name)
        {
            return await _roleRepository.GetByNameAsync(name)
                ?? throw new Exception($"Role with name: {name} not found");
        }

        /* public async Task<RoleReadDTO> GetByNameAsync(String name)
           {
               var entity = await _roleRepository.GetByNameAsync(name)
               ?? throw new Exception($"Role with name: {name} not found");

               return MapToDto(entity);
           }*/

        private static RoleReadDTO MapToDto(Role r) => new()
        {
            Id = r.Id,
            RoleName = r.RoleName
        };
    }
}