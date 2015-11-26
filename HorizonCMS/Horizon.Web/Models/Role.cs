using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Horizon.Web.Models
{
    [Table("Roles")]
    public class Role
    {
        public Role() {
           
        }

        [Key]
        [Required]
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        //public Guid UserId { get; set; }

        public virtual ICollection<User> Users { get; set; }

        public virtual ICollection<Section> Sections { get; set; }
      
    }
}
