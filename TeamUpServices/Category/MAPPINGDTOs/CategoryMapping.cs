using TeamUpServices.Category.DTOS;
namespace TeamUpServices.Category.MAPPINGDTOs
{
    public class CategoryMapping
    {
        public static TeamUpCore.Model.Category MAPPINGDTO(CategoryCreateDto createDto)
        {
            return new TeamUpCore.Model.Category { Name = createDto.Name };
        }

        public static void MAPPINGDTO(CategoryUpdateDto updateDto, TeamUpCore.Model.Category existingCategory)
        {
            existingCategory.Name = updateDto.Name;
        }

        public static CategoryReturnDto MAPPINGDTO(TeamUpCore.Model.Category category)
        {
            if (category == null) return null;
            return new CategoryReturnDto { Id = category.Id, Name = category.Name };
        }
    }
}
