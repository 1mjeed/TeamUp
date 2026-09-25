using System;
using System.Collections.Generic;
using System.Text;
using TeamUpServices.USERS.DTOS;

namespace TeamUpServices.USERS.ABSTRACTION
{
    public interface IUsersQueries
    {
        Task<UserReturnDto> GetUserByIdAsync(int userId);
        Task<IEnumerable<UserReturnDto>> GetAllUsersAsync();
    }
}
