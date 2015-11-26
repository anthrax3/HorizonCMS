using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Horizon.DAL.Tables.Entities
{
    public class PostType
    {
        [Key]
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public Guid PstId { get; set; }

        public virtual ICollection<Post> Posts { get; set; }
    }
}
