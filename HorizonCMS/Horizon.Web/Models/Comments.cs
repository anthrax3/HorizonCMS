using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Horizon.Web.Models
{
    [Table("BlogPostComments")]
    public class Comment
    {
        [Key]
        public Guid Id { get; set; }

        public string Content { get; set; }

        public bool IsActive { get; set; }

        public DateTime CreateDate { get; set; }

        public virtual User Author { get; set; }

        public Guid PstId { get; set; }
        public virtual Post Post { get; set; }
    }
}
