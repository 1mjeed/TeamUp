#region Using 
using Microsoft.EntityFrameworkCore;
using TeamUpCore.Model;
using TeamUpData;
using TeamUpServices.USERS.ABSTRACTION;
using TeamUpServices.USERS.DTOS;
using TeamUpServices.USERS.MAPPINGDTOs;
#endregion

namespace TeamUpServices.USERS.IMPLEMENTATION
{
    public class UserCommand : IUserCommand
    {

        #region Dependency Injection
        private readonly TeamUpDbContext _context;

        public UserCommand(TeamUpDbContext context) 
        { 
        _context = context;
        }
        #endregion

        #region Create User
        public async Task<bool> CreateUserAsync(UserCreateDto createDto)
        {
            User newuser = UserMapping.MAPPINGDTO(createDto);

            await _context.Users.AddAsync(newuser);

            var rowsAffected = await _context.SaveChangesAsync();

            return rowsAffected > 0; 
        }
        #endregion

        #region Update User
        public async Task<bool> UpdateUserAsync(int userId , UserUpdateDto updatedto)
        {
            var existingUser = await _context.Users.FindAsync(userId);

            if (existingUser == null) return false;

            UserMapping.MAPPINGDTO(updatedto, existingUser);

            var rowsAffected = await _context.SaveChangesAsync();

            return (rowsAffected > 0);
        }
        #endregion

        #region Delete User
        public async Task<bool> DeleteUserAsync(int userId) 
        {
            
            var rowsAffected = await _context.Users
                .Where(x=>x.Id == userId)
                .ExecuteDeleteAsync();

            return (rowsAffected > 0);
        }
        #endregion
    }
}
