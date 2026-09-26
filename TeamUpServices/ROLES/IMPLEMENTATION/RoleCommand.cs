using Microsoft.EntityFrameworkCore;
using TeamUpData;
using TeamUpServices.ROLES.ABSTRACTION;
using TeamUpServices.ROLES.DTOS;
using TeamUpServices.ROLES.MAPPINGDTOs;

namespace TeamUpServices.ROLES.IMPLEMENTATION
{
    public class RoleCommand : IRoleCommand
    {
        private readonly TeamUpDbContext _context;

        public RoleCommand(TeamUpDbContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateRoleAsync(RoleCreateDto roleDto)
        {
            if (roleDto == null) return false;

            var newRole = RoleMapping.MAPPINGDTO(roleDto);

            await _context.Roles.AddAsync(newRole);
            var rowsAffected = await _context.SaveChangesAsync();

            return rowsAffected > 0;
        }

        public async Task<bool> UpdateRoleAsync(int roleId, RoleUpdateDto roleUpdateDto)
        {
            if (roleId < 1 || roleUpdateDto == null) return false;

            var existingRole = await _context.Roles.FindAsync(roleId);

            if (existingRole == null) return false;

            RoleMapping.MAPPINGDTO(roleUpdateDto, existingRole);

            var rowsAffected = await _context.SaveChangesAsync();

            return rowsAffected > 0;
        }

        public async Task<bool> DeleteRoleAsync(int roleId)
        {
            if (roleId <= 0) return false;

            var rowsAffected = await _context.Roles
                .Where(r => r.Id == roleId)
                .ExecuteDeleteAsync();

            return rowsAffected > 0;
        }
    }
}