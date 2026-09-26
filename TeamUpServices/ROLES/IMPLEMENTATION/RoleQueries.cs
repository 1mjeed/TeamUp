using Microsoft.EntityFrameworkCore;
using TeamUpData;
using TeamUpServices.ROLES.ABSTRACTION;
using TeamUpServices.ROLES.DTOS;
using TeamUpServices.ROLES.MAPPINGDTOs;

namespace TeamUpServices.ROLES.IMPLEMENTATION
{
    public class RoleQueries : IRoleQueries
    {
        private readonly TeamUpDbContext _context;

        public RoleQueries(TeamUpDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<RoleReturnDto>> GetAllRolesAsync()
        {
            var roles = await _context.Roles
                .AsNoTracking()
                .ToListAsync();
            return roles.Select(x=>RoleMapping.MAPPINGDTO(x)).ToList();
        }

        public async Task<RoleReturnDto> GetRoleByIdAsync(int id)
        {
            if (id <= 0) return null;

            var role = await _context.Roles
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);

            return RoleMapping.MAPPINGDTO(role);
        }
    }
}