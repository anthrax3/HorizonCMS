using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Horizon.Web.Areas.Admin.Models
{
    public class DTOUser
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public bool IsActive { get; set; }

        public bool IsLocked { get; set; }

        public DateTime CreateDate { get; set; }

        public Guid Profile { get; set; }

        public string ProfilePhoto { get; set; }
        
      
    }
}