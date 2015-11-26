using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Horizon.Web.Models
{
    [Table("Applications")]
    public class Application
    {
        [Key]
        [Required]
        [Column("Id", Order = 0)]
        [Display(Name = "Application Id")]
        public Guid Id { get; set; }

        [Key]
        [Required]
        [Column("Name", Order = 1)]
        [Display(Name = "Application Name")]
        [DataType(DataType.Text)]
        [StringLength(255, MinimumLength = 8)]
        public string Name { get; set; }

        [Column("Blog")]
        [Display(Name = "Blog")]
        public virtual Blog Blog { get; set; }

        [Column("Theme")]
        [Display(Name = "Theme")]
        public virtual Theme Theme { get; set; }
    

    }
}