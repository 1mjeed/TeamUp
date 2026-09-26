using TeamUpCore.Model;
using TeamUpServices.ROLES.DTOS;

namespace TeamUpServices.ROLES.MAPPINGDTOs
{
    public class RoleMapping 
    {
        public static Role MAPPINGDTO(RoleCreateDto createDto)
        {
            return new Role
            {
                Name = createDto.Name,
            };
        }

        public static void MAPPINGDTO(RoleUpdateDto updateDto, Role existingRole) 
        {
            existingRole.Name = updateDto.Name;
        }

        public static RoleReturnDto MAPPINGDTO(Role existingRole) 
        {
            if (existingRole == null) return null; 

            return new RoleReturnDto
            {
                Id = existingRole.Id,
                Name = existingRole.Name,
            };
        }
    }
}