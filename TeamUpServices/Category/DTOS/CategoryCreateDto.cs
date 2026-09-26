using System.ComponentModel.DataAnnotations;

namespace TeamUpServices.Category.DTOS
{
    public class CategoryCreateDto
    {
        [Required(ErrorMessage = "اسم التصنيف مطلوب")]
        [StringLength(100)]
        public string Name { get; set; }
    }

}
