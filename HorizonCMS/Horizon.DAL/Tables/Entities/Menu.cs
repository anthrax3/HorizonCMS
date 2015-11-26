using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Horizon.DAL.Tables.Entities
{
    [Table("Menus")]
    public class Menu
    {
        [Key]
        public Guid Id { get; set; }

        [Index(IsUnique=true)]
        [DataType(DataType.Text)]
        [StringLength(255, MinimumLength=4)]
        public string Name { get; set; }

        public Guid TplId { get; set; }

        public virtual Template Template { get; set; }

        public Guid LngId { get; set; }

        public virtual Language Language { get; set; }

        public virtual ICollection<Page> Pages { get; set; }

    }
}
