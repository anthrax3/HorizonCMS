using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace Horizon.DAL.Tables.Entities
{
    [Table("BlogPost")]
    public class Post
    {
        public Post()
        {
          
           
        }

        [Key]
        public Guid Id { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [StringLength(255,MinimumLength=8)]
        public string Title { get; set; }

        [Required]
        [DataType(DataType.Html)]
        [AllowHtml]
        public string Content { get; set; }

        [Required]
        [DataType(DataType.Html)]
        [AllowHtml]
        public string Excerpt { get; set; }

        public int Viewed { get; set; }

        public int Loved { get; set; }

        public int Shared { get; set; }

        [Index(IsUnique=true)]
        [StringLength(255)]
        public string Url { get; set; }

        public DateTime CreateDate { get; set; }

        public Guid BlgId { get; set; }

        public virtual Blog Blog { get; set; }

        public Guid AuthId { get; set; }

        public virtual User Author { get; set; }

        public Guid CatId { get; set; }

        public virtual PostCategory Category { get; set; }

        public Guid PstId { get; set; }

        public virtual PostType PostType { get; set; }

        public virtual ICollection<Media> Images { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }

        public virtual ICollection<Tag> Tags { get; set; }
    }
}
