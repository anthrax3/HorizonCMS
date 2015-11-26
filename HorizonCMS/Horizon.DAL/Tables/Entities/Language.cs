using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Horizon.DAL.Tables.Entities
{
    [Table("Languages")]
    public class Language
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [StringLength(255, MinimumLength = 4, ErrorMessage = "Enter the Language's name")]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        public bool IsActive { get; set; }

        public int SortOrder { get; set; }
        public virtual ICollection<Page> Pages { get; set; }

        public Guid LngCdId { get; set; }

        public virtual LanguageCode LanguageCode { get; set; }

        public virtual ICollection<Translation> Translations { get; set; }
    }
}
