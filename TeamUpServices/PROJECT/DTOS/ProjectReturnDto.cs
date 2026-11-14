using System.ComponentModel.DataAnnotations;
using TeamUpServices.USERS.DTOS;

namespace TeamUpServices.PROJECT.DTOS
{
    public class ProjectReturnDto
    {
        #region Properties 
        /// <summary>
        /// Project ID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Project Title
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Project Description Details
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Maximum Team Capacity
        /// </summary>
        public int CapaCity { get; set; }

        /// <summary>
        /// Project Current Status
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// User ID (Foreign Key)
        /// </summary>
       
        [Required]
        public int UserId { get; set; }

        #endregion

        #region For Join
        public string CategoryName { get; set; }
        public string OwnerName { get; set; }
        public List<String> TeamMembersNames { get; set; } = new List<String>();
        #endregion
    }
}
