using TeamUpCore.Model;
using TeamUpServices.USERS.DTOS;

namespace TeamUpServices.USERS.MAPPINGDTOs
{

    public static class UserMapping
    {
        #region  UserCreateDto
        public static User MAPPINGDTO(UserCreateDto createDto)
        {
            return new User
            {
                UNEnumber = createDto.UNEnumber,
                Name = createDto.Name,
                Email = createDto.Email,
                Password = createDto.Password,
                Phone = createDto.Phone,
                RoleId = 1
            };
        }
        #endregion

        #region  User Update Dto
        public static void MAPPINGDTO(UserUpdateDto updateDto, User existingUser)
        {
            existingUser.Name = updateDto.Name;
            existingUser.Phone = updateDto.Phone;
            existingUser.Description = updateDto.Description;
            existingUser.ImgUrl = updateDto.ImgUrl;
        }
        #endregion

        #region User Return 
        public static UserReturnDto MAPPINGDTO(User user)
        {
            if (user == null) return null;

            return new UserReturnDto
            {
                Id = user.Id,
                Name = user.Name,
                Phone = user.Phone,
                Description = user.Description,
                ImgUrl = user.ImgUrl //  هون بدي اضيف بالمستقبل انه اذا ما كان عنده صوره ارسل صوره افتراضيه ان شاء الله 
                                     // بهاض الاسلوب ان شاء الله 
                                     //ImgUrl = user.ImgUrl ?? "https://yourdomain.com/images/default-avatar.png"
            };
        }
        #endregion

    }
}
