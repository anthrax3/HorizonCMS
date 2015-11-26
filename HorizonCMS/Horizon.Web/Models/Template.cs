using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace Horizon.Web.Models
{
    [Table("Templates")]
    public class Template
    {
        public Template()
        {
          
        }

        [Key]
        [Display(Name = "Template Id")]
        public Guid Id { get; set; }

        [Required]
        [Display(Name = "Template Name")]
        [DataType(DataType.Text)]
        [StringLength(255, MinimumLength = 4, ErrorMessage = "Please, enter a name for the template")]
        [Index(IsUnique=true)]       
        public string Name { get; set; }

        [AllowHtml]
        [Display(Name = "Template Content")]
        [DataType(DataType.Html)]
        public string Content { get; set; }

        [Required]
        [Display(Name = "Template Creation Date")]
        [DataType(DataType.DateTime)]
        public DateTime CreateDate { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? UpdateDate { get; set; }

        [Required]
        public bool IsActive { get; set; }

        public Guid MnuId { get; set; }

        public virtual Menu Menu { get; set; }

        public virtual ICollection<Page> Pages { get; set; }



    }
}