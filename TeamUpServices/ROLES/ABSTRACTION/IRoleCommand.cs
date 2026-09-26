using TeamUpServices.ROLES.DTOS;

namespace TeamUpServices.ROLES.ABSTRACTION
{
    public interface IRoleCommand
    {
        Task<bool> CreateRoleAsync(RoleCreateDto createDto);
        Task<bool> UpdateRoleAsync(int RoleId, RoleUpdateDto updateDto);
        Task<bool> DeleteRoleAsync(int RoleId);
    }
}
