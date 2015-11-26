using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace Horizon.Web.Controllers
{
    public class HomeController : Base.BaseController
    {
        private Horizon.Web.DAL.HorizonContext PageManager;

        public HomeController()
            : base()
        {
            PageManager = new DAL.HorizonContext();
        }

        public ActionResult Index(string page, string subpage)
        {
            if (page != null && subpage != null)
            {
                var default_page = PageManager.Page.Where(p => p.Name.Equals(subpage) && p.Parent.Name.Equals(page) && p.Language.LanguageCode.Code.Equals(Helpers.CultureHelper.GetCurrentCulture)).SingleOrDefault();

                if (default_page != null)
                {
                    StringBuilder keywords = new StringBuilder();
                    ViewBag.Title = default_page.Title;
                    ViewBag.Language = default_page.Language.LanguageCode.Code;
                    ViewBag.Menu = default_page.Template.Menu.Pages.Where(p => p.Template.Menu.Language.LanguageCode.Code.Equals(Helpers.CultureHelper.GetCurrentCulture)).ToList();

                    foreach (var keyword in default_page.Keywords)
                    {
                        keywords.Append(keyword.KeywordName).Append(",");
                    }

                    ViewBag.Keywords = keywords.ToString().Substring(0, keywords.ToString().LastIndexOf(','));
                    ViewBag.Description = default_page.Description;
                    return View(default_page);
                }
                else
                    return RedirectToAction("404", "Error");
            }
            else if (page != null && subpage == null)
            {
                var default_page = PageManager.Page.Where(p => p.Name.Equals(page) && p.Language.LanguageCode.Code.Equals(Helpers.CultureHelper.GetCurrentCulture)).SingleOrDefault();
                if (default_page != null)
                {
                    ViewBag.Title = default_page.Title;
                    ViewBag.Language = default_page.Language.LanguageCode.Code;
                    ViewBag.Menu = default_page.Template.Menu.Pages.Where(p => p.Template.Menu.Language.LanguageCode.Code.Equals(Helpers.CultureHelper.GetCurrentCulture)).ToList();
                    ViewBag.Description = default_page.Description;
                    return View(default_page);
                }
                else
                    return RedirectToAction("404", "Error");
            }
            else
            {
                var default_page = PageManager.Page.Where(p => p.IsDefault.Equals(true) && p.Language.LanguageCode.Code.Equals(Helpers.CultureHelper.GetCurrentCulture)).SingleOrDefault();
                if (default_page != null)
                {
                    ViewBag.Title = default_page.Title;
                    ViewBag.Language = default_page.Language.LanguageCode.Code;
                    ViewBag.Description = default_page.Description;
                    ViewBag.Menu = default_page.Template.Menu.Pages.Where(p => p.Template.Menu.Language.LanguageCode.Code.Equals(Helpers.CultureHelper.GetCurrentCulture)).ToList();
                    return View(default_page);
                }
                else
                    return RedirectToAction("404", "Error");
            }
        }
    }
}