using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TeamUpCore.Model
{
    /// <summary>
    /// Role Entity || كيـــان الصلاحيـــات
    /// </summary>
    public class Role
    {
        #region Properties
        /// <summary>
        /// Role ID
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// Role Name
        /// </summary>
        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        #endregion

        #region Navigation Properties

        /// <summary>
        /// List of Users with this Role (One-to-Many)
        /// </summary>
        public ICollection<User> Users { get; set; } = new List<User>();

        #endregion
    }
}
