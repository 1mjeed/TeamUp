using TeamUpCore.Model;
using TeamUpServices.PROJECT.DTOS;
using TeamUpServices.USERS.DTOS;

namespace TeamUpServices.PROJECT.MAPPINGDTOs
{
    public class ProjectMapping
    {
        #region  ProjectCreateDto
        public static Project MAPPINGDTO(ProjectCreateDto createDto)
        {
            return new Project
            {
                Title = createDto.Title,
                Description = createDto.Description,
                CapaCity = createDto.CapaCity,
                CategoryId = createDto.CategoryId,
                UserId = createDto.UserId,
                Status = 1
            };
        }
        #endregion

        #region  Project Update Dto
        public static void MAPPINGDTO(ProjectUpdateDto updateDto, Project existingProject)
        {
            existingProject.Title = updateDto.Title;
            existingProject.Description = updateDto.Description;
            existingProject.CapaCity = updateDto.CapaCity;
            existingProject.CategoryId = updateDto.CategoryId;
        }
        #endregion

        #region Project Return 
        public static ProjectReturnDto MAPPINGDTO(Project project)
        {
            if (project == null) return null;

            return new ProjectReturnDto
            {
                Id=project.Id,
                Title = project.Title,
                Description = project.Description,
                CapaCity = project.CapaCity,
                Status = project.Status,
                // بالمستقبل بنكمله 
                //   CategoryName = project.CategoryName,
                // OwnerName = project.OwnerName,
                // TeamMembersNames = project.TeamMembersNames?.Select(tm => tm.Registrations.name).ToList() ?? new List<string>()
            };
        }
        #endregion

    }
}
