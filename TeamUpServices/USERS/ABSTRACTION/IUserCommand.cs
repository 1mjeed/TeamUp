using TeamUpServices.USERS.DTOS;

namespace TeamUpServices.USERS.ABSTRACTION
{
    public interface IUserCommand
    {
        Task<bool> CreateUserAsync(UserCreateDto createDto);
        Task<bool> UpdateUserAsync(int userId, UserUpdateDto updateDto);
        Task<bool> DeleteUserAsync(int userId);
    }
}
