using System.ComponentModel.DataAnnotations;

namespace TeamUpServices.USERS.DTOS
{
    public class UserUpdateDto
    {
        #region Properties

        /// <summary>
        /// Name
        /// </summary>
        [Required(ErrorMessage = "الاسم مطلوب")]
        [StringLength(100)]
        public string Name { get; set; }

        /// <summary>
        /// Phone Number
        /// </summary>
        [Phone]
        public string Phone { get; set; }

        /// <summary>
        /// User Bio / Description
        /// </summary>
        [StringLength(500)]
        public string? Description { get; set; }

        /// <summary>
        /// Profile Image URL
        /// </summary>
        [StringLength(500)]
        public string? ImgUrl { get; set; }

        #endregion
    }
}
