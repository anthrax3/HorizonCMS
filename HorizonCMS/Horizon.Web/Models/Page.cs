using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace Horizon.Web.Models
{
    [Table("Pages")]
    public class Page
    {
        public Page()
        {
           
        }

        [Key]
        public Guid Id { get; set; }

        [Display(Name="Page Name")]
        [DataType(DataType.Text)]
        [StringLength(255, MinimumLength = 4, ErrorMessage = "Enter the page's name")]
        [Index(IsClustered = false, IsUnique = true, Order = 1)]
        public string Name { get; set; }


        [Required]
        [Display(Name = "Page Title")]
        [DataType(DataType.Text)]
        [StringLength(255, MinimumLength = 4, ErrorMessage = "Enter the page's title")]
        public string Title { get; set; }

        [Display(Name = "Page Description")]
        [DataType(DataType.MultilineText)]
        [StringLength(140, MinimumLength = 4, ErrorMessage = "Enter the page's description")]
        public string Description { get; set; }


        [Required]
        [Display(Name = "Page Content")]
        [DataType(DataType.Html)]
        [AllowHtml]
        public string Content { get; set; }

        [Display(Name = "Creation Date")]
        [DataType(DataType.DateTime)]
        public DateTime CreateDate { get; set; }

        [Display(Name = "Update Date")]
        public DateTime? UpdateDate { get; set; }
        
        [Required]
        [Index(IsUnique=false)]
        public bool IsDefault { get; set; }

        /// <summary>
        /// Referencia a la plantilla
        /// </summary>
        [Display(Name="Template")]
        public Guid TplId { get; set; }

         [Display(Name = "Template")]
        public virtual Template Template { get; set; }

        [Display(Name = "Language")]
        public Guid LngId { get; set; }

        [Display(Name = "Language")]
        [Index(IsClustered = false, IsUnique = true)]
        public virtual Language Language { get; set; }

        [Display(Name="Parent")]
        public Guid? PrntId { get; set; }

        public virtual Page Parent { get; set; }

        public virtual ICollection<Page> Childs { get; set; }

        public virtual ICollection<Keyword> Keywords { get; set; }

    }
}