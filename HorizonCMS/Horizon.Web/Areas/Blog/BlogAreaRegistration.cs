using System.Web.Mvc;

namespace Horizon.Web.Areas.Blog
{
    public class BlogAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Blog";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Blog_default",
                "{culture}/blog/",
                new { culture = string.Empty, area = "blog", controller = "Blog", action = "Index", id = UrlParameter.Optional },
                new { controller = "blog" },
                new string[] { "Horizon.Web.Areas.Blog.Controllers" });

            context.MapRoute(
                "blog-categories",
                "{culture}/blog/categories)",
                new { culture = string.Empty, area = "blog", controller = "blog", action = "Categories" },
                new { controller = "blog" },
                new string[] { "Horizon.Web.Areas.Blog.Controllers" });

            context.MapRoute(
              "blog-category-view",
              "{culture}/blog/category/{categoryname}",
              new { culture = string.Empty, area = "blog", Controller = "Blog", action = "Category", categoryname = UrlParameter.Optional },
               new { controller = "blog" },
                new string[] { "Horizon.Web.Areas.Blog.Controllers" });

            context.MapRoute(
               "blog-post-view",
               "{culture}/blog/{year}/{month}/{day}/{postname}",
               new { culture = string.Empty, area = "blog", controller = "blog", action = "Post", year = UrlParameter.Optional, month = UrlParameter.Optional, day = UrlParameter.Optional },
               new { controller = "blog" },
               new string[] { "Horizon.Web.Areas.Blog.Controllers" });
        }
    }
}