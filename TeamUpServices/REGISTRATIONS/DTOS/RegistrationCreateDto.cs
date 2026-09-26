using System.ComponentModel.DataAnnotations;

namespace TeamUpServices.REGISTRATIONS.DTOS
{
    public class RegistrationCreateDto
    {
        [Required(ErrorMessage = "رقم المشروع مطلوب")]
        public int ProjectId { get; set; }

        [Required(ErrorMessage = "رقم المستخدم مطلوب")]
        public int UserId { get; set; }
    }
}