using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Horizon.Web.Startup))]
namespace Horizon.Web
{
    public static partial class Startup
    {
        public static void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}