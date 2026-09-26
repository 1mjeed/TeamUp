using System;
using System.Collections.Generic;
using System.Text;
using TeamUpServices.Category.DTOS;

namespace TeamUpServices.Category.ABSTRACTION
{
    public interface ICategoryQueries
    {
        Task<IEnumerable<CategoryReturnDto>> GetAllCategoriesAsync();
        Task<CategoryReturnDto> GetCategoryByIdAsync(int id);
    }
}
