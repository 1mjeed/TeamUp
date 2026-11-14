using TeamUpData;
using TeamUpServices.Category.ABSTRACTION;
using TeamUpServices.Category.DTOS;
using TeamUpServices.Category.MAPPINGDTOs;

namespace TeamUpServices.Category.IMPLEMENTATION
{
    public class CategoryCommand : ICategoryCommand
    {
        private readonly TeamUpDbContext _context;
        public CategoryCommand(TeamUpDbContext context) => _context = context;

        public async Task<bool> CreateCategoryAsync(CategoryCreateDto dto)
        {
            if (dto == null) return false;
            await _context.Categories.AddAsync(CategoryMapping.MAPPINGDTO(dto));
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateCategoryAsync(int id, CategoryUpdateDto dto)
        {
            if (id <= 0 || dto == null) return false;
            var category = await _context.Categories.FindAsync(id);
            if (category == null) return false;
            CategoryMapping.MAPPINGDTO(dto, category);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteCategoryAsync(int Categoryid)
        {
            var category = await _context.Categories.FindAsync(Categoryid);

            if (category == null) return false;

            _context.Categories.Remove(category);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
