using System.Web.Mvc;
using System.Web.Routing;

namespace Horizon.Web
{
    public static class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapMvcAttributeRoutes();

            routes.MapRoute(
                name: "Default",
                url: "{culture}/{controller}/{page}/{subpage}",
                defaults: new { culture = string.Empty, controller = "Home", action = "Index", page = UrlParameter.Optional, subpage = UrlParameter.Optional }
                );

            routes.MapRoute(
                name: "404-PageNotFound",
                // This will handle any non-existing urls
                url: "{*url}",
                // "Shared" is the name of your error controller, and "Error" is the action/page
                // that handles all your custom errors
                defaults: new { culture = string.Empty, controller = "Error", action = "Index", type = "404" });
        }
    }
}
