using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Horizon.DAL.Tables.Entities
{
    [Table("NotificationStatus")]
    public class NotificationStatus
    {
        [Key]
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Type { get; set; }

        public Guid NtfId { get; set; }

        public virtual ICollection<Notification> Notification { get; set; }
    }
}
