using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Horizon.Web.Models
{
    [Table("Users")]
    public class User
    {

        public User()
        {
        }

        [Key]
        [Required]
        public Guid Id { get; set; }

        [Required]
        [DataType(DataType.Text,ErrorMessage="The user's name is required")]
        [MaxLength(64)]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.Text, ErrorMessage = "The user's email is required")]
        [MaxLength(254)]
        [EmailAddress()]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Text, ErrorMessage = "The user's password is required")]
        public string Password { get; set; }

        public bool IsActive { get; set; }

    
        public bool IsLocked { get; set; }

        public DateTime CreateDate { get; set; }

        public virtual Profile Profile { get; set; }

        [Required]
        public virtual ICollection<Role> Roles { get; set; }

        public virtual ICollection<UserLogin> UserLogin { get; set; }

        public virtual ICollection<Notification> Notifications { get; set; }
    }
}