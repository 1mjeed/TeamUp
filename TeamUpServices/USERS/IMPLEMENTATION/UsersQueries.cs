using Microsoft.EntityFrameworkCore;
using TeamUpData;
using TeamUpServices.USERS.ABSTRACTION;
using TeamUpServices.USERS.DTOS;
using TeamUpServices.USERS.MAPPINGDTOs;

namespace TeamUpServices.USERS.IMPLEMENTATION
{

    public class UsersQueries : IUsersQueries
    {
        #region Dependency Injection
        private readonly TeamUpDbContext _context;

        public UsersQueries(TeamUpDbContext context)
        {
            _context = context;
        }
        #endregion

        #region Get User by ID 
        public async Task<UserReturnDto> GetUserByIdAsync(int id)
        {
            var user = await _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);

            return UserMapping.MAPPINGDTO(user);
        }
        #endregion

        #region GetAll User
        public async Task<IEnumerable<UserReturnDto>> GetAllUsersAsync()
        {
            var users = await _context.Users
                    .AsNoTracking()
                    .ToListAsync();

            return users.Select(user => UserMapping.MAPPINGDTO(user)).ToList();
        }
        #endregion
    }
}
