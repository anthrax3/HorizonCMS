using Horizon.Web.DAL;
using System;
using System.Data.Entity;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Linq;

namespace Horizon.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {

        /// <summary>
        /// 
        /// </summary>
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(new RazorViewEngine());

            Database.SetInitializer<HorizonContext>(new HorizonInitializer());

            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        /// <summary>
        /// 
        /// </summary>
        protected void Application_Error()
        {
            System.Exception exception = Server.GetLastError();
            Response.Clear();

            if (exception != null)
            {

                using (HorizonContext db = new HorizonContext())
                {
                    if (!Horizon.Web.Helpers.DatabaseHelper.IsDown(db))
                    {
                        //var notification = new Horizon.Web.Models.Notification()
                        //{
                        //    Id = Guid.NewGuid(),
                        //    Content = exception.Message,
                        //    CreateDate = DateTime.Now,
                        //    Name = string.Format("Error {0}", (exception as System.Web.HttpException).GetHttpCode()),
                        //    Read = false,
                        //    Title = string.Format("Error {0}", (exception as System.Web.HttpException).GetHttpCode()),
                        //    Users = db.User.Where(u => u.Roles.Any(r => r.Name.Contains("SuperAdmin"))).ToList(),
                        //    Priority = 0,
                        //    Status = db.NotificationStatus.Where(n => n.Name.Equals("Unread")).SingleOrDefault()
                        //};

                        //db.Notification.Add(notification);
                        //db.SaveChanges();
                    }
                }

                System.Web.HttpException httpException = exception as System.Web.HttpException;

                if (httpException != null)
                {

                    // clear error on server
                    Server.ClearError();

                    Response.Redirect(string.Format("/{0}/Error/{1}", Helpers.CultureHelper.GetCurrentCulture, httpException.GetHttpCode()), true);
                }
            }
        }
    }
}