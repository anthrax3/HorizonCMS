using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Horizon.Web.DTOModels
{
    public class DTONotification
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Name { get; set; }

       public int Priority { get; set; }

        public bool Read { get; set; }

        public DateTime CreateDate { get; set; }

        public string Status { get; set; }
    }
}