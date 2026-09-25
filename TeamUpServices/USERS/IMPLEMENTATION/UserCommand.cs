using TeamUpData;
using TeamUpServices.USERS.DTOS;
namespace TeamUpServices.USERS.IMPLEMENTATION
{
    public class UserCommand : IUsersCommand
    {
        private readonly TeamUpDbContext _context;

        public UserCommand(TeamUpDbContext context) 
        { 
        _context = context;
        }
        public async Task<bool> CreateUserAsync(UserCreateDto createDto)
        {
        
        }
    }
}
