using TeamUpServices.PROJECT.DTOS;

namespace TeamUpServices.PROJECT.ABSTRACTION
{
    public interface IProjectCommand
    {
        Task<bool> CreateProjectAsync(ProjectCreateDto createDto);
        Task<bool> UpdateProjectAsync(int ProjectId, ProjectUpdateDto updateDto);
        Task<bool> DeleteProjectAsync(int ProjectId);

    } 
}
