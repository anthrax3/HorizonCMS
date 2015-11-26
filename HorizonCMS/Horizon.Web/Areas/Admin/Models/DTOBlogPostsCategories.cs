using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Horizon.Web.Areas.Admin.Models
{
    public class DTOBlogPostsCategories
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string CategoryName { get; set; }

        public string Description { get; set; }
    }
}