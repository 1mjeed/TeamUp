using TeamUpServices.ROLES.DTOS;

namespace TeamUpServices.ROLES.ABSTRACTION
{
    public interface IRoleQueries
    {
        Task<RoleReturnDto> GetRoleByIdAsync(int userId);
        Task<IEnumerable<RoleReturnDto>> GetAllRolesAsync();
    }
}
