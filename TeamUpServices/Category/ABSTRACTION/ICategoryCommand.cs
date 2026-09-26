using System;
using System.Collections.Generic;
using System.Text;
using TeamUpServices.Category.DTOS;

namespace TeamUpServices.Category.ABSTRACTION
{
    public interface ICategoryCommand
    {
        Task<bool> CreateCategoryAsync(CategoryCreateDto dto);
        Task<bool> UpdateCategoryAsync(int id, CategoryUpdateDto dto);
        Task<bool> DeleteCategoryAsync(int id);
    }
}
