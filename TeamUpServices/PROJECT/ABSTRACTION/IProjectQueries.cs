using TeamUpServices.PROJECT.DTOS;

namespace TeamUpServices.PROJECT.ABSTRACTION
{
    public interface IProjectQueries
    {
          Task<ProjectReturnDto> GetProjectByIdAsync(int id);
          Task<IEnumerable<ProjectReturnDto>> GetAllProjectsAsync();
    }
}
