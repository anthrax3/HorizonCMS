using System;
using System.ComponentModel.DataAnnotations;

namespace Horizon.Web.Models
{
    public class LoginViewModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [Display(Name = "User name")]
        public string UserName { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }
}