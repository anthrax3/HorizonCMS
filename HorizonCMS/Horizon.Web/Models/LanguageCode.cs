using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Horizon.Web.Models
{
    [Table("LanguageCode")]
    public class LanguageCode
    {
        public LanguageCode()
        {
         
        }

        [Key]
        public Guid Id { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [StringLength(5, MinimumLength = 5, ErrorMessage = "Enter the language Code (language-culture)")]
        public string Code { get; set; }

        public Guid LngId { get; set; }

        public virtual ICollection<Language> Languages { get; set; }


    }
}
