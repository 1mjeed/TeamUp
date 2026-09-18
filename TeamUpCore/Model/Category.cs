using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TeamUpCore.Model
{    
    /// <summary>
    /// Category Entity || كيـــان الفئة
    /// </summary>
    public class Category
    {
        #region Properties

        /// <summary>
        /// Category ID
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// Category Name
        /// </summary>
        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        #endregion

        #region Navigation Properties
        /// <summary>
        /// List of Projects under this Category (One-to-Many)
        /// </summary>
        public ICollection<Project> Projects { get; set; } = new List<Project>();
        #endregion
    }
}
