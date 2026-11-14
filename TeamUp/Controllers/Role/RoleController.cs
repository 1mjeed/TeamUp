using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TeamUpServices.ROLES.ABSTRACTION;
using TeamUpServices.ROLES.DTOS;
namespace TeamUpAPI.Controllers.Role
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class RoleController : ControllerBase
    {
        #region Dependence Injection

        private readonly IRoleCommand _roleCommand;
        private readonly IRoleQueries _roleQuerie;

        public RoleController(IRoleCommand roleCommand, IRoleQueries roleQuerie)
        {
            _roleCommand = roleCommand;
            _roleQuerie = roleQuerie;
        }
        #endregion

        #region Get Role (ALL - By ID)
        [HttpGet]
        public async Task<IActionResult> GetAllRoles()
        {
            var roles = await _roleQuerie.GetAllRolesAsync();

            if (roles == null)
                return NotFound();
            return Ok(roles);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetRolebyId(int id)
        {
            if (id < 1)
                return BadRequest();

            var role = await _roleQuerie.GetRoleByIdAsync(id);

            if (role == null)
                return NotFound();

            return Ok(role);
        }
        #endregion

        #region  Create Role
        [HttpPost]
        public async Task<IActionResult> CreateRole(RoleCreateDto create)
        {
            if (create == null) return BadRequest();

            var newRole = await _roleCommand.CreateRoleAsync(create);

            if (!newRole ) return BadRequest();

            return Ok(newRole);
        }
        #endregion

        #region Update Role
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRole(int id, RoleUpdateDto update)
        {
            if (id < 1) return BadRequest();

            var newstatus = await _roleCommand.UpdateRoleAsync(id, update);

            if (!newstatus) return BadRequest();

            return Ok(newstatus);
        }
        #endregion

        #region Delete Role
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRole(int id)
        {
            if (id < 1) return BadRequest();

            var isdeleteRole = await _roleCommand.DeleteRoleAsync(id);

            if(!isdeleteRole) return BadRequest();

            return Ok(isdeleteRole);
        }
        #endregion
    }
}
