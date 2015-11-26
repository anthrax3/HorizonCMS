using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Horizon.Web.Models
{
    [Table("Media")]
    public class Media
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [StringLength(255,MinimumLength=8)]
        [DataType(DataType.Text)]
        public string Name { get; set; }

        [StringLength(500,MinimumLength=20)]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [DataType(DataType.Upload)]
        public string FileUrl { get; set; }

        public string ContentType { get; set; }


    }
}
