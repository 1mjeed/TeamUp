using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TeamUpCore.Model
{
    /// <summary>
    /// Project Entity || كيـــان المشـــروع
    /// </summary>
    public class Project
    {
        #region Properties 
        /// <summary>
        /// Project ID
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

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

        /// <summary>
        /// Project Current Status
        /// </summary>
        [Required]
        public int Status { get; set; }

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
        public int UserId { get; set; }

        #endregion

        #region Navigation Properties

        [ForeignKey(nameof(CategoryId))]
        public Category Category { get; set; }

        [ForeignKey(nameof(UserId))]
        public User User { get; set; }

        public ICollection<Registration> Registrations { get; set; } = new List<Registration>();
        #endregion

        #region NotMapped
       // [NotMapped]
       // public string CategoryName { get; set; }
        //[NotMapped]
        //public string OwnerName { get; set; }
       // [NotMapped]
        //public List<string> TeamMembersNames { get; set; } = new List<string>();
        #endregion
    }
}
