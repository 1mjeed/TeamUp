using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TeamUpCore.Model
{
    /// <summary>
    /// User Entity || كيـــان المستخـــدم
    /// </summary>
    public class User : GlobalEntity
    {
        #region Properties

        /// <summary>
        /// User ID
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// University Number / Identifier
        /// </summary>
        
        [StringLength(50)]
        public string UNEnumber { get; set; }

        /// <summary>
        ///  Name
        /// </summary>
        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        /// <summary>
        /// Phone Number
        /// </summary>
        [Required]
        [Phone]
        [StringLength(20)]
        public string Phone { get; set; }

        /// <summary>
        /// Email Address
        /// </summary>
        [Required]
        [EmailAddress]
        [StringLength(150)]
        public string Email { get; set; }

        /// <summary>
        /// Encrypted Password
        /// </summary>
        [Required]
        [StringLength(255)]
        public string PasswordHash { get; set; }

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

        #region Relational Database (Foreign Keys)

        /// <summary>
        /// Role ID (Foreign Key)
        /// </summary>
        [Required]
        public int RoleId { get; set; }

        #endregion

        #region Navigation Properties

        /// <summary>
        /// The Role associated with this User
        /// </summary>
        [ForeignKey(nameof(RoleId))]
        public Role Role { get; set; }

        /// <summary>
        /// Projects owned/created by the User
        /// </summary>
        public ICollection<Project> Projects { get; set; } = new List<Project>();

        /// <summary>
        /// Registrations the User has joined
        /// </summary>
        public ICollection<Registration> Registrations { get; set; } = new List<Registration>();

        #endregion
    }
}