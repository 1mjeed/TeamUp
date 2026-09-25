
using TeamUpCore.Model;
using TeamUpServices.USERS.DTOS;

namespace TeamUpServices.USERS.MAPPINGDTOs
{
    public static class UserMapping
    {
        public static User MAPPINGDTO(UserCreateDto createDto)
        {
            return new User
            {
                UNEnumber = createDto.UNEnumber,
                Name = createDto.Name,
                Email = createDto.Email,
                Password = createDto.Password,
                Phone = createDto.Phone,
                RoleId = 2
            };
        }
    }
}
