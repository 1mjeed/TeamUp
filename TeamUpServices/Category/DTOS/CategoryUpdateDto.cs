using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TeamUpServices.Category.DTOS
{
    public class CategoryUpdateDto
    {
        [Required(ErrorMessage = "اسم التصنيف مطلوب")]
        [StringLength(100)]
        public string Name { get; set; }
    }
}
