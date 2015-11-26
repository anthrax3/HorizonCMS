using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Horizon.DAL.Tables.Entities
{
    [Table("PostCategories")]
    public class PostCategory
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public string Name { get; set; }

        public string CategoryName { get; set; }


        [Required]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        public virtual ICollection<Post> Posts { get; set; }
    }
}