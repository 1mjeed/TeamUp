using System.ComponentModel.DataAnnotations;

namespace TeamUpServices.PROJECT.DTOS
{
    public class ProjectUpdateDto
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
        public int CapaCity { get; set; }
        #endregion

        [Required]
        public int CategoryId { get; set; }
    }
}
