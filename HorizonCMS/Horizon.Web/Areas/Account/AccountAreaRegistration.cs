using System.Web.Mvc;

namespace Horizon.Web.Areas.Account
{
    public class AccountAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Account";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Account_default",
                "{culture}/Account/{action}/{id}",
                new { culture= string.Empty, controller="Account", action = "Index", id = UrlParameter.Optional },
                new { controller="Account"},
                 new string[] { "Horizon.Web.Areas.Account.Controllers" }
            );
        }
    }
}