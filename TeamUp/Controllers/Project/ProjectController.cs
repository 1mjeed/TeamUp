using Microsoft.AspNetCore.Mvc;
using TeamUpServices.PROJECT.ABSTRACTION;
using TeamUpServices.PROJECT.DTOS;

namespace TeamUpAPI.Controllers.Project
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        #region Dependency Injection
        private readonly IProjectCommand _projectCommand;
        private readonly IProjectQueries _projectQueries;

        public ProjectsController(IProjectCommand projectCommand, IProjectQueries projectQueries)
        {
            _projectCommand = projectCommand;
            _projectQueries = projectQueries;
        }
        #endregion

        #region Get All Projects
        [HttpGet]
        [Route("GetAllProjects")]
        public async Task<IActionResult> GetAllProjects()
        {
            var projects = await _projectQueries.GetAllProjectsAsync();
            return Ok(projects);
        }
        #endregion

        #region Get Project By ID
        [HttpGet]
        [Route("GetProjectById/{id}")]
        public async Task<IActionResult> GetProjectById(int id)
        {
            if (id < 1) return BadRequest();

            var project = await _projectQueries.GetProjectByIdAsync(id);

            if (project == null)
                return NotFound(new { message = $"Project with ID {id} not found." });

            return Ok(project);
        }
        #endregion

        #region Create Project
        [HttpPost]
        [Route("CreateProject")]
        public async Task<IActionResult> CreateProject(ProjectCreateDto projectDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var isCreated = await _projectCommand.CreateProjectAsync(projectDto);

            if (!isCreated)
                return BadRequest(new { message = "Failed to create the project. Please check your data." });

            return Ok(new { message = "Project created successfully." });
        }
        #endregion

        #region Update Project
        [HttpPut]
        [Route("UpdateProject/{id}")]
        public async Task<IActionResult> UpdateProject(int id, ProjectUpdateDto projectUpdateDto)
        {
            if (id < 1) return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var isUpdated = await _projectCommand.UpdateProjectAsync(id, projectUpdateDto);

            if (!isUpdated)
                return BadRequest(new { message = "Failed to update the project. It may not exist or invalid data was provided." });

            return Ok(new { message = "Project updated successfully." });
        }
        #endregion

        #region Delete Project
        [HttpDelete]
        [Route("DeleteProject/{id}")]
        public async Task<IActionResult> DeleteProject(int id)
        {
            if (id < 1) return BadRequest();

            var isDeleted = await _projectCommand.DeleteProjectAsync(id);

            if (!isDeleted)
                return NotFound(new { message = $"Project with ID {id} not found or could not be deleted." });

            return Ok(new { message = "Project deleted successfully." });
        }
        #endregion
    }
}
