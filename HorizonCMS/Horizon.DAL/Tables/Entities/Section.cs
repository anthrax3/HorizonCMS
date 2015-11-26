using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Horizon.DAL.Tables.Entities
{
    [Table("Sections")]
    public class Section : Interfaces.ITable
    {
        [Key]
        [Required]
        public Guid Id { get; set; }

        [Required]
        [Display(Name = "Section.Title")]
        [StringLength(255, MinimumLength = 4)]
        [DataType(DataType.Text)]
        [Index(IsUnique = true)]
        public string Title { get; set; }

        [Required]
        [Display(Name = "Section.Name")]
        [StringLength(255, MinimumLength = 4)]
        [DataType(DataType.Text)]
        [Index(IsUnique = true)]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Section.Glyphicon")]
        [StringLength(40, MinimumLength = 4)]
        [DataType(DataType.Text)]
        public string Glyphicon { get; set; }

        [Required]
        [Display(Name = "Section.Controller")]
        [StringLength(40, MinimumLength = 1)]
        [DataType(DataType.Text)]
        public string Controller { get; set; }

        [Required]
        [Display(Name = "SortOrder")]
        public int SortOrder { get; set; }

        public virtual ICollection<Role> Roles { get; set; }
    }
}