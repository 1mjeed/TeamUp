using Microsoft.AspNetCore.Mvc;
using TeamUpServices.Category.ABSTRACTION;
using TeamUpServices.Category.DTOS;

namespace TeamUpAPI.Controllers.Category
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryCommand _command;
        private readonly ICategoryQueries _queries;

        public CategoriesController(ICategoryCommand command, ICategoryQueries queries)
        {
            _command = command;
            _queries = queries;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _queries.GetAllCategoriesAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            if (id <= 0) return BadRequest();
            var category = await _queries.GetCategoryByIdAsync(id);
            if (category == null) return NotFound();
            return Ok(category);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CategoryCreateDto dto)
        {
            return await _command.CreateCategoryAsync(dto) ? Ok(new { message = "تم الإنشاء" }) : BadRequest();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, CategoryUpdateDto dto)
        {
            if (id <= 0) return BadRequest();
            return await _command.UpdateCategoryAsync(id, dto) ? Ok(new { message = "تم التعديل" }) : BadRequest();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0) return BadRequest();
            return await _command.DeleteCategoryAsync(id) ? Ok(new { message = "تم الحذف" }) : NotFound();
        }
    }
}