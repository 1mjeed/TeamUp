using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
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
        // [Authorize]
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _usersQueries.GetAllUsersAsync();
            return Ok(users);
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            if (id < 1) return BadRequest();

            var user = await _usersQueries.GetUserByIdAsync(id);

            if (user == null) return NotFound();

            return Ok(user);
        }
        #endregion

        #region  Create User
        [Authorize(Roles = "Admin")]// الادمن بس الي مسموحله يعمل خسابات للاخرين 
        [HttpPost]
        public async Task<IActionResult> CreateUser(UserCreateDto createDto)
        {
            var isCreated = await _usersCommand.CreateUserAsync(createDto);
            if (!isCreated) return BadRequest();
            return Ok();
        }
        #endregion

        #region Update User
        [Authorize]
        [HttpPut("{id}")]//Ownership Policy
        public async Task<IActionResult> UpdateUser(int id, UserUpdateDto updateDto)
        {
            if (id < 1) return BadRequest();

            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out int authenticatedUserId))
                return Unauthorized();

            // authenticatedUserId = int.Parse(userIdClaim);
            bool isAdmin = User.IsInRole("Admin");

            if (!isAdmin && authenticatedUserId != id)
                return Forbid();

            var isUpdated = await _usersCommand.UpdateUserAsync(id, updateDto);

            if (!isUpdated) return NotFound();

            return Ok();
        }
        #endregion

        #region Delete User
        [Authorize] // Ownership Policy  
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            if (id < 1) return BadRequest();

            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            int authenticatedUserId = int.Parse(userIdClaim);
            bool isAdmin = User.IsInRole("Admin");

            if (!isAdmin && authenticatedUserId != id)
                return Forbid();

            var isDeleted = await _usersCommand.DeleteUserAsync(id);

            if (!isDeleted) return NotFound();

            return Ok();
        }
        #endregion

    }
}
