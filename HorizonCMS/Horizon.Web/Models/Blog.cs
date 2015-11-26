using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Horizon.Web.Models
{
    [Table("Blog")]
    public class Blog
    {
        public Blog()
        {
          
        }

        [Key]
        [Display(Name="Blog Id")]
        public Guid Id { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [StringLength(255,MinimumLength=10, ErrorMessage="Introduzca un mensaje de longitud mínima {2}")]
        [Display(Name = "Blog Name")]
        [Index(IsUnique=true,Order=1)]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Blog Description")]
        public string Description { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [StringLength(255, MinimumLength = 10, ErrorMessage = "Introduzca un mensaje de longitud mínima {2}")]
        public string Title { get; set; }

        public bool IsActive { get; set; }

        public Guid AppId { get; set; }
        public virtual Application Application { get; set; }
        public virtual ICollection<Post> Posts { get; set; }
    }
}