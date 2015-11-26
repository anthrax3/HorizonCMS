using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Horizon.Web.Models
{
    [Table("Themes")]
    public class Theme
    {

        [Key]
        public Guid Id { get; set; }

        [Required]
        [Index(IsUnique=true)]
        [Display(Name="Theme Name")]
        [DataType(DataType.Text)]
        [StringLength(255,MinimumLength=4)]
        public string Name { get; set; }

        [Required]
        [Display(Name="Active")]
        public bool IsActive { get; set; }

        public virtual Application Application { get; set; }
        
    }
}
