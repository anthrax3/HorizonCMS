using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Horizon.Web.DTOModels
{
    public class DTOPage
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime? UpdateDate { get; set; }

        public bool IsDefault { get; set; }

        public string Language { get; set; }
    }
}