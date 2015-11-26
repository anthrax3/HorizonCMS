using System.Web.Mvc;
using System.Linq;
using Horizon.Web.Models;
using Horizon.Web.DTOModels;
using System.Linq.Dynamic;
using System;

namespace Horizon.Web.Areas.Admin.Controllers
{
    [Authorize]
    public class DashboardController : BaseController
    {
        //private Horizon.Web.DAL.HorizonContext db = new DAL.HorizonContext();
        public DashboardController()
            : base()
        {

        }

        public JsonResult getAdminStatistics()
        {
            JsonResult result = null;


            result = new JsonResult()
            {
                Data = new
                {
                    totalLanguages = db.Language.Count(),
                    languages = db.Language.Where(l => l.IsActive == true).Count(),
                    totalPages = db.Page.Count(),
                    totalUsers = db.User.Count(),
                    users = db.User.Where(u => u.IsActive).Count()
                }
            };

            return result;
        }

        public JsonResult getNotification(Guid id)
        {
            JsonResult result = null;

            var query = db.Notification.Where(n => n.Id.Equals(id));

            result = new JsonResult()
            {
                Data = new
                {
                    Notifications = query.SingleOrDefault()

                }
            };

            return result;
        }

        public JsonResult getNotifications(int page, int records, string orderBy, string orderDirection)
        {
            JsonResult result = null;

            var query = (from n in db.Notification
                         where n.Users.Any(u => u.Name.Equals(User.Identity.Name))
                         orderby n.Priority ascending
                         select new DTONotification
                         {
                             Id = n.Id,
                             Name = n.Name,
                             CreateDate = n.CreateDate,
                             Priority = n.Priority,
                             Status = n.Status.Name,
                             Read = n.Read,
                             Title = n.Title
                         }).OrderBy(string.Format("{0} {1}", orderBy, orderDirection)).Skip((records * page) - records).Take(records);

            result = new JsonResult()
             {
                 Data = new
                 {
                     Notifications = query.ToList(),
                     Count = (from n in db.Notification
                              where n.Users.Any(u => u.Name.Equals(User.Identity.Name))
                              orderby n.Priority ascending
                              select new DTONotification
                              {
                                  Id = n.Id,
                                  Name = n.Name,
                                  CreateDate = n.CreateDate,
                                  Priority = n.Priority,
                                  Status = n.Status.Name,
                                  Read = n.Read,
                                  Title = n.Title
                              }).Count()
                 }
             };

            return result;
        }


        public JsonResult removeNotifications(string[] notificationsId)
        {
            JsonResult result = null;
            bool bResult = true;

            if (notificationsId != null && notificationsId.Length > 0)
            {
                System.Collections.Generic.List<Notification> notifications = new System.Collections.Generic.List<Notification>();

                notifications = db.Notification.Where(n => notificationsId.Contains(n.Id.ToString())).ToList();

                notifications.ForEach(notif => db.Notification.Remove(notif));
                db.SaveChanges();
            }
            else
                bResult = false;

            result = new JsonResult()
            {
                Data = new
                {
                    Result = bResult
                }
            };

            return result;
        }



        //
        // GET: /Admin/Dashboard/
        public ActionResult Index()
        {
            return View();
        }
    }
}