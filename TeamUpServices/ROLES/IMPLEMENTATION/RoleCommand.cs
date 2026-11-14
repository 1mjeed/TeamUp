using Microsoft.EntityFrameworkCore;
using TeamUpData;
using TeamUpServices.ROLES.ABSTRACTION;
using TeamUpServices.ROLES.DTOS;
using TeamUpServices.ROLES.MAPPINGDTOs;

namespace TeamUpServices.ROLES.IMPLEMENTATION
{
    public class RoleCommand : IRoleCommand
    {
        #region Dependency Injection

        private readonly TeamUpDbContext _context;

        public RoleCommand(TeamUpDbContext context)
        {
            _context = context;
        }
        #endregion

        #region Create Role

        public async Task<bool> CreateRoleAsync(RoleCreateDto roleDto)
        {
            if (roleDto == null) return false;

            var newRole = RoleMapping.MAPPINGDTO(roleDto);

            await _context.Roles.AddAsync(newRole);
            var rowsAffected = await _context.SaveChangesAsync();

            return rowsAffected > 0;
        }

        #endregion

        #region Update Role
        public async Task<bool> UpdateRoleAsync(int roleId, RoleUpdateDto roleUpdateDto)
        {
            if (roleId < 1 || roleUpdateDto == null) return false;

            var existingRole = await _context.Roles.FindAsync(roleId);

            if (existingRole == null) return false;

            RoleMapping.MAPPINGDTO(roleUpdateDto, existingRole);

            var rowsAffected = await _context.SaveChangesAsync();

            return rowsAffected > 0;
        }

        #endregion

        #region Delete  Role
        public async Task<bool> DeleteRoleAsync(int roleId)
        {
            var role = await _context.Roles.FindAsync(roleId);
            if (role == null) return false;

            _context.Roles.Remove(role);// هاي حسب فهمي انها مثل الختم انه تم حذفها بس هي مانحذفت عشان ال تراكنق يشوفها بعرف على طول انها المقصد حذف وال اوفر رايد الي عملناه بلعب لهبته 

            return await _context.SaveChangesAsync() > 0;
        }
        #endregion
    }
}