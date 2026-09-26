using Microsoft.EntityFrameworkCore;
using TeamUpData;
using TeamUpServices.Category.ABSTRACTION;
using TeamUpServices.Category.DTOS;
using TeamUpServices.Category.MAPPINGDTOs;

namespace TeamUpServices.Category.IMPLEMENTATION
{
    public class CategoryQueries : ICategoryQueries
    {
        private readonly TeamUpDbContext _context;
        public CategoryQueries(TeamUpDbContext context) => _context = context;

        public async Task<IEnumerable<CategoryReturnDto>> GetAllCategoriesAsync()
        {
            var categories = await _context.Categories.AsNoTracking().ToListAsync();
            return categories.Select(CategoryMapping.MAPPINGDTO).ToList();
        }

        public async Task<CategoryReturnDto> GetCategoryByIdAsync(int id)
        {
            if (id <= 0) return null;

            var category = await _context.Categories.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);
            return CategoryMapping.MAPPINGDTO(category);
        }
    }
}
