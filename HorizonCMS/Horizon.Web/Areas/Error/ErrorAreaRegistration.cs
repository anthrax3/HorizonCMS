using System.Web.Mvc;

namespace Horizon.Web.Areas.Error
{
    public class ErrorAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Error";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Error_default",
                "{culture}/Error/{type}",
                new { culture = string.Empty, controller = "Error", action="Index", type  = UrlParameter.Optional }
            );
        }
    }
}