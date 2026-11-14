using System.ComponentModel.DataAnnotations;

namespace TeamUpServices.USERS.DTOS
{
    public class UserCreateDto
    {
        #region Properties

        /// <summary>
        /// University Number / Identifier
        /// </summary>
        
         public string? UNEnumber { get; set; }

        /// <summary>
        /// Name
        /// </summary>
        
        [Required(ErrorMessage ="الرجاء أدخال الأسم")]
        [StringLength(100)]
        public string Name { get; set; }

        /// <summary>
        /// Email Address
        /// </summary>
        
        [Required(ErrorMessage ="الرجاء أدخال البريد الألكتروني ")]
        [EmailAddress(ErrorMessage = "صيغة البريد الإلكتروني غير صحيحة")]
        public string Email {  get; set; }

        /// <summary>
        /// Encrypted Password
        /// </summary>
        
        [Required(ErrorMessage = "الرجاء أدخال كلمة المرور")]
        [MinLength(8, ErrorMessage = "كلمة المرور يجب أن لا تقل عن 8 أحرف")]
        public string Password { get; set; }

        /// <summary>
        /// Phone Number
        /// </summary>
        [Required(ErrorMessage = "الرجاء أدخال رقم الهاتف ")]
        [MinLength(10, ErrorMessage = " رقم الهاتف غير صحيح")]
        [Phone]
        public string Phone { get; set; }


        #endregion
    }
}
