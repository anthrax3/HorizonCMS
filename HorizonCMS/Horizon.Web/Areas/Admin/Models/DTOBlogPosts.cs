using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Horizon.Web.Areas.Admin.Models
{
    public class DTOBlogPosts
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public string Excerpt { get; set; }

        public int Viewed { get; set; }

        public int Loved { get; set; }

        public int Shared { get; set; }

        public string Url { get; set; }

        public DateTime CreateDate { get; set; }

        public Guid BlgId { get; set; }

        public Guid AuthId { get; set; }

        public Guid CatId { get; set; }

        public Guid PstId { get; set; }
    }
}