using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Horizon.DAL.Tables.Entities
{
    public class Keyword
    {
        public Keyword()
        {
          
        }

        [Key]
        public Guid Id { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [StringLength(255,MinimumLength=4, ErrorMessage="Introduzca la palabra clave")]
        public string KeywordName { get; set; }

        public virtual ICollection<Page> Pages { get; set; }
    }
}
