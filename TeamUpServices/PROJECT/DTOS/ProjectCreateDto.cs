using System.ComponentModel.DataAnnotations;

namespace TeamUpServices.PROJECT.DTOS
{
    public class ProjectCreateDto
    {
        #region Properties 
        /// <summary>
        /// Project Title
        /// </summary>
        [Required]
        [StringLength(50)]
        public string Title { get; set; }

        /// <summary>
        /// Project Description Details
        /// </summary>
        [Required]
        [StringLength(250)]
        public string Description { get; set; }

        /// <summary>
        /// Maximum Team Capacity
        /// </summary>
        [Required]
        public int CapaCity { get; set; }
        #endregion

        #region Relational Database (Foreign Keys)
        /// <summary>
        /// Category ID (Foreign Key)
        /// </summary>
        [Required]
        public int CategoryId { get; set; }

        /// <summary>
        /// User ID (Foreign Key)
        /// </summary>
        [Required]
        public int UserId { get; set; } //  رح أحذفه بالمستقبل ان شاء لله 
        #endregion
    }
}
