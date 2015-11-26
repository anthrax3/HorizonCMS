using Horizon.Web.Helpers;
using Horizon.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web.Mvc;

namespace Horizon.Web.Areas.Account.Controllers.Base
{
    [HandleError]
    public partial class BaseController : Controller
    {
        #region Fields

        private static Dictionary<string, string> _translations;
        private static Uri _currentUrl;
        private static string _currentLanguage;

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

        #endregion

        #region Ctor.

        /// <summary>
        /// 
        /// </summary>
        public BaseController()
        {
            // Load available languages
            using (DAL.HorizonContext db = new DAL.HorizonContext())
            {
                List<string> result = new List<string>();

                foreach (var languages in (from language in db.Language.Include("LanguageCode")
                                           orderby language.SortOrder ascending
                                           select language))
                {
                    result.Add(languages.LanguageCode.Code.ToString());
                }
                CultureHelper.Cultures = result;
            }
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
    }
}