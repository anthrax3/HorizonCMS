using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Horizon.Web.Models
{
    public class Translation
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [StringLength(255, MinimumLength = 4, ErrorMessage = "Introduzca nombre de traducción")]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [StringLength(255,MinimumLength=4, ErrorMessage="INtroduzca la traducción")]
        public string Content { get; set; }

        [Display(Description="Idioma")]
        public Guid LngId { get; set; }

        public virtual Language Language { get; set; }


    }
}
