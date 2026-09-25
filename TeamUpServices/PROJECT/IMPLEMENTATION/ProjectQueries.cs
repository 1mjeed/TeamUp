using Microsoft.EntityFrameworkCore;
using TeamUpData;
using TeamUpServices.PROJECT.ABSTRACTION;
using TeamUpServices.PROJECT.DTOS;
using TeamUpServices.PROJECT.MAPPINGDTOs;

namespace TeamUpServices.PROJECT.IMPLEMENTATION
{
    public class ProjectQueries : IProjectQueries
    {
        #region Dependency Injection
        private readonly TeamUpDbContext _context;

        public ProjectQueries(TeamUpDbContext context)
        {
            _context = context;
        }
        #endregion

        #region Get Project by ID 
        public async Task<ProjectReturnDto> GetProjectByIdAsync(int id)
        {
            var project = await _context.Projects
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);

            return ProjectMapping.MAPPINGDTO(project);
        }
        #endregion

        #region GetAll Project
        public async Task<IEnumerable<ProjectReturnDto>> GetAllProjectsAsync()
        {
            var project = await _context.Projects
                    .AsNoTracking()
                    .ToListAsync();

            return project.Select(x => ProjectMapping.MAPPINGDTO(x)).ToList();
        }
        #endregion
    }
}
