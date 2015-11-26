using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace Horizon.DAL.Tables.Entities
{
    [Table("Notifications")]
    public class Notification
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name="Título")]
        [StringLength(255,MinimumLength=8)]
        public string Title { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Nombre")]
        [StringLength(255, MinimumLength = 8)]
        public string Name { get; set; }

        [Required]
        [AllowHtml]
        [DataType(DataType.Html)]
        public string Content {get;set;}


        [Required]
        [Display(Name = "Prioridad")]
        public int Priority { get; set; }

        [Required]
        [Display(Name="Leido")]
        public bool Read { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime CreateDate { get; set; }

        public virtual NotificationStatus Status { get; set; }

        public virtual ICollection<User> Users{ get; set; }

    }
}