using Microsoft.AspNetCore.Mvc;
using TeamUpServices.USERS.ABSTRACTION;
using TeamUpServices.USERS.DTOS;

namespace TeamUpAPI.Controllers.User
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        #region Dependence Injection
        private readonly IUserCommand _usersCommand;
        private readonly IUsersQueries _usersQueries;

        public UsersController(IUserCommand usersCommand, IUsersQueries usersQueries)
        {
            _usersCommand = usersCommand;
            _usersQueries = usersQueries;
        }
        #endregion

        #region Get Users (ALL - By ID)
        [HttpGet]
        [Route("GetAllUsers")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _usersQueries.GetAllUsersAsync();
            if (users == null) return NotFound();

            return Ok(users);
        }

        [HttpGet]
        [Route("GetUserById/{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            if (id < 1) return BadRequest();

            var user = await _usersQueries.GetUserByIdAsync(id);

            if (user == null) return NotFound();

            return Ok(user);
        }
        #endregion

        #region  Create User
        [HttpPost]
        [Route("CreateUser")]
        public async Task<IActionResult> CreateUser(UserCreateDto createDto)
        {
            var isCreated = await _usersCommand.CreateUserAsync(createDto);
            if (!isCreated) return BadRequest();
            return Ok();
        }
        #endregion

        #region Update User

        [HttpPut]
        [Route("UpdateUser/{id}")]
        public async Task<IActionResult> UpdateUser(int id, UserUpdateDto updateDto)
        {
            if(id <1) return BadRequest();

            var isUpdated = await _usersCommand.UpdateUserAsync(id, updateDto);

            if (!isUpdated) return NotFound();

            return Ok();
        }
        #endregion

        #region Delete User
        [HttpDelete]
        [Route("UpdateUser/{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            if (id < 1) return BadRequest();

            var isDeleted = await _usersCommand.DeleteUserAsync(id);

            if (!isDeleted) return NotFound();

            return Ok();
        }
        #endregion

    }
}
