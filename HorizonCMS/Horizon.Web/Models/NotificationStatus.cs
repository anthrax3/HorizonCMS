using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Horizon.Web.Models
{
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
