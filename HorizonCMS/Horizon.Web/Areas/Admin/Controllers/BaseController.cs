using Horizon.Web.Helpers;
using Horizon.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web.Mvc;

namespace Horizon.Web.Areas.Admin.Controllers
{

    [Authorize]
    public class BaseController : Controller
    {
        #region Fields

        private static Dictionary<string, string> _translations;
        private static Uri _currentUrl;
        private static string _currentLanguage;
        protected Horizon.Web.DAL.HorizonContext db = new DAL.HorizonContext();

        #endregion

        #region Properties



        /// <summary>
        /// 
        /// </summary>
        public static string CurrentLanguage
        {
            get
            {
                return _currentLanguage;
            }
            set
            {
                _currentLanguage = value;
            }
        }

        public static Dictionary<string, string> Translations
        {
            get
            {
                return _translations;
            }
            set
            {
                _translations = value;
            }
        }


        /// <summary>
        /// 
        /// </summary>
        public static Uri CurrentUrl
        {
            get { return _currentUrl; }
            set { _currentUrl = value; }
        }

        public static string CurrentController
        {
            get;
            set;
        }

        #endregion

        #region Ctor.

        /// <summary>
        /// 
        /// </summary>
        public BaseController()
        {



            //




        }

        private void loadNotifications(string userName)
        {
            if (!string.IsNullOrEmpty(userName))
            {
                List<Notification> notifications = HttpContext.Cache[string.Format("NOTIFICATIONS-{0}", userName)] as List<Notification>;

                if (notifications == null)
                {
                    notifications = new List<Notification>();

                    notifications = (from notification in db.Notification
                                     where notification.Users.Any(nu => nu.Id.Equals(db.User.Where(u => u.Name.Equals(userName)).FirstOrDefault().Id))
                                     select notification).ToList();

                    HttpContext.Cache.Add(string.Format("NOTIFICATIONS-{0}", userName), notifications, null, System.Web.Caching.Cache.NoAbsoluteExpiration, new TimeSpan(0, 30, 0), System.Web.Caching.CacheItemPriority.Default, null);
                }

                ViewBag.Notifications = notifications;
            }
        }

        private static ICollection<Section> loadSections(string userName)
        {
            if (!string.IsNullOrEmpty(userName))
            {
                using (DAL.SectionManager dal = new DAL.SectionManager())
                {
                    return dal.getSectionsByUser(userName);
                }

            }
            else
                return null;
        }
        #endregion


        /// <summary>
        /// 
        /// </summary>
        /// <param name="callback"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        /// 
        protected override IAsyncResult BeginExecuteCore(AsyncCallback callback, object state)
        {
            string cultureName = RouteData.Values["culture"] as string;
            CurrentController = RouteData.Values["controller"] as string;

            // Attempt to read the culture cookie from Request
            if (cultureName == string.Empty)
                cultureName = Request.UserLanguages != null && Request.UserLanguages.Length > 0 ? Request.UserLanguages[0] : null; // obtain it from HTTP header AcceptLanguages

            // Validate culture name
            cultureName = CultureHelper.GetImplementedCulture(cultureName); // This is safe


            if (RouteData.Values["culture"] as string != cultureName)
            {

                // Force a valid culture in the URL
                RouteData.Values["culture"] = cultureName; //.ToUpperInvariant(); //ToLowerInvariant(); // lower case too

                // Redirect user
                Response.RedirectToRoute(RouteData.Values);
            }

            // Modify current thread's cultures            
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo(cultureName);
            Thread.CurrentThread.CurrentUICulture = Thread.CurrentThread.CurrentCulture;

            CurrentLanguage = cultureName;

            CurrentUrl = new Uri(Request.Url.ToString());

            ViewBag.Sections = loadSections(User.Identity.Name);
            loadNotifications(User.Identity.Name);
            return base.BeginExecuteCore(callback, state);

        }



        /// <summary>
        /// Loads languages into TempData space
        /// </summary>
        protected static Dictionary<string, string> LoadTranslations(string page, string language)
        {
            Translations = new Dictionary<string, string>();

            //using (ITranslationsRepository TranslationsService = new TranslationsRepository())
            //{
            //    using (ILanguagesRepository LanguagesService = new LanguagesRepository())
            //    {
            //        foreach (var translation in TranslationsService.GetAllByPageId(page, LanguagesService.GetByCode(language)))
            //        {
            //            if (!Translations.ContainsKey(translation.Name))
            //                Translations.Add(translation.Name, translation.Content);
            //        }
            //    }
            //}
            return Translations;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected static IReadOnlyCollection<Language> LoadLanguages()
        {
            using (DAL.HorizonContext db = new DAL.HorizonContext())
            {
                return (from languages in db.Language select languages).ToList();
            }
        }

        [ChildActionOnly]
        [ActionName("IsActive")]
        public string IsActive(string section)
        {
            return section == CurrentController ? "active" : "";
        }

        protected override void OnException(ExceptionContext filterContext)
        {
        }
    }
}