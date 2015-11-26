using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Horizon.DAL.Tables.Entities
{
    [Table("Tags")]
    public class Tag
    {
        [Key]
        public Guid Id { get; set; }
        
        [Required]
        [Display(Name="Post Tags")]
        [StringLength(255,MinimumLength=3)]
        [Index(IsUnique=true)]
        public string Name { get; set; }

        public ICollection<Post> Posts { get; set; }
    }
}
