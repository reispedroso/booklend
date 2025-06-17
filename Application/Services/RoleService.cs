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
            var entity = await _roleRepository.GetNameById(id)
            ?? throw new Exception("Role not found");

            return MapToDto(entity);
        }

        private static RoleReadDTO MapToDto(Role r) => new()
        {
            RoleName = r.RoleName
        };
    }
}