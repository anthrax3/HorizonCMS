using System.Web.Mvc;
using System.Linq;
using Horizon.Web.Areas.Error.Models;

namespace Horizon.Web.Areas.Error.Controllers
{
    public class ErrorController : Horizon.Web.Controllers.Base.BaseController
    {
        //
        // GET: /Error/Error/
        public ActionResult Index(string type)
        {
            ErrorViewModel result = null;

            switch (type)
            {
                case "404":
                    result = new ErrorViewModel()
                    {
                        Title = "404",
                        subTitle = "Because no one is perfect!",
                        Message = "Oh gosh! It looks like that the content you are looking for is not available anymore on our servers.<br/>Why don't you take a look at these sections?"

                    };
                    break;

                case "500":
                    result = new ErrorViewModel()
                    {
                        Title = "500",
                        subTitle = "Ooops! Today is not our best day",
                        Message = "Our servers are under maintenance. Please come back in a few minutes."

                    };
                    break;

                default:
                     result = new ErrorViewModel()
                    {
                        Title = type,
                        subTitle = "Ooops! Today is not our best day",
                        Message = "Our servers are under maintenance. Please come back in a few minutes."

                    };
                    break;
            }

            using (Horizon.Web.DAL.HorizonContext db = new DAL.HorizonContext())
            {
                ViewBag.Menu = db.Page.Where(p => p.Template.Menu.Language.LanguageCode.Code.Equals(Helpers.CultureHelper.GetCurrentCulture)).ToList();
            }
            return View(result);
        }
    }
}