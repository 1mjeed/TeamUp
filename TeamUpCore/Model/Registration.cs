using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TeamUpCore.Model
{
    /// <summary>
    /// Registration Entity || كيـــان التسجيـــل
    /// </summary>
    public class Registration
    {
        #region Properties
        /// <summary>
        /// Registration  ID
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// Registration Current Status
        /// </summary>
        [Required]
        public int Status { get; set; }

        #endregion

        #region Relational Database (Foreign Keys)

        /// <summary>
        /// Project ID (Foreign Key)
        /// </summary>
        [Required]
        public int ProjectID { get; set; }

        /// <summary>
        /// User ID (Foreign Key)
        /// </summary>
        [Required]
        public int UserID { get; set; }

        #endregion

        #region Navigation Properties

        [ForeignKey(nameof(ProjectID))]
        public Project Project { get; set; }

        [ForeignKey(nameof(UserID))]
        public User User { get; set; }

        #endregion
    }
}
