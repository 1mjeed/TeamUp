using Microsoft.EntityFrameworkCore;
using TeamUpCore.Model;
using TeamUpData;
using TeamUpServices.PROJECT.ABSTRACTION;
using TeamUpServices.PROJECT.DTOS;
using TeamUpServices.PROJECT.MAPPINGDTOs;

namespace TeamUpServices.PROJECT.IMPLEMENTATION
{
    public class ProjectCommand :IProjectCommand
    {
        #region Dependency Injection
        private readonly TeamUpDbContext _context;
        public ProjectCommand(TeamUpDbContext context)
        {
            _context = context;
        }
        #endregion

        #region Create Project
        public async Task<bool> CreateProjectAsync(ProjectCreateDto projectdto)
        {
            if (projectdto == null)
                return false;

            Project newproject = ProjectMapping.MAPPINGDTO(projectdto);

            await _context.Projects.AddAsync(newproject);

            var rowsAffected = await _context.SaveChangesAsync();

            return rowsAffected > 0;
        }
        #endregion

        #region Update Project
        public async Task<bool> UpdateProjectAsync(int projectid, ProjectUpdateDto projectUpdateDto)
        {
            if (projectid < 1 || projectUpdateDto == null)
                return false;

            var existingProject = await _context.Projects.FindAsync(projectid);

            if (existingProject == null)
                return false;

            ProjectMapping.MAPPINGDTO(projectUpdateDto, existingProject);

            var rowsAffected = await _context.SaveChangesAsync();

            return (rowsAffected > 0);
        }
        #endregion

        #region Delete Project
        public async Task<bool> DeleteProjectAsync(int projectId)
        {
            var rowsAffected = await _context.Projects.
                Where(x => x.Id == projectId)
                .ExecuteDeleteAsync();

            return (rowsAffected > 0);
        }

        #endregion

    }
}
