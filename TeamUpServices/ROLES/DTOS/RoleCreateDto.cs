using System.ComponentModel.DataAnnotations;

namespace TeamUpServices.ROLES.DTOS
{
    public class RoleCreateDto
    {
        /// <summary>
        /// Name
        /// </summary>

        [Required(ErrorMessage = "الرجاء أدخال الأسم")]
        [StringLength(100)]
        public string Name { get; set; }
    }
}
