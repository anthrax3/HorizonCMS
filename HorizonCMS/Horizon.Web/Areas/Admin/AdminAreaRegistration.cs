using System.Web.Mvc;

namespace Horizon.Web.Areas.Admin
{
    public class AdminAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Admin_default",
                "{culture}/admin/",
                new { culture = string.Empty, area = "admin", controller = "Dashboard", action = "Index", id = UrlParameter.Optional },
                new string[] { "Horizon.Web.Areas.Admin.Controllers" }
            );

            context.MapRoute(
                "Admin_routes",
                "{culture}/admin/{controller}/{action}",
                new { culture = string.Empty, area = "admin", action = "Index" },
                new { controller = "Dashboard|Blogs|Comments|Users|Roles|Languages|Keywords|Pages|Posts|PostCategories|Posttypes|Templates|Translations|Menus|Security" },
                 new string[] { "Horizon.Web.Areas.Admin.Controllers" });

        }
    }
}