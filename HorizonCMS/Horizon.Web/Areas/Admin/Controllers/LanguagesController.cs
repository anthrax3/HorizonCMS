using System;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Net;
using System.Web.Mvc;
using Horizon.Web.Models;
using Horizon.Web.DTOModels;
using System.Linq;
using System.Linq.Dynamic;
using Horizon.Web.Filters;

namespace Horizon.Web.Areas.Admin.Controllers
{
    [ValidateAntiForgeryTokenOnAllPosts]
    public class LanguagesController : BaseController
    {

        public LanguagesController() : base() { }

        // GET: /Admin/Languages/
        public async Task<ActionResult> Index()
        {
            var language = db.Language.Include(l => l.LanguageCode);
            ViewBag.LngCdId = new SelectList(db.LanguageCode, "Id", "Code");
            return View(await language.ToListAsync());
        }

        public JsonResult getLanguage(Guid id)
        {
            JsonResult result = null;
            var language = db.Language.Find(id);

            result = new JsonResult()
            {
                Data = new
                {
                    Languages = new DTOLanguage()
                    {
                        Id = language.Id,
                        Name = language.Name,
                        Description = language.Description,
                        IsActive = language.IsActive,
                        LngCdId = language.LanguageCode.Id.ToString()
                    }
                }
            };

            return result;
        }

        public JsonResult getLanguages(int page, int records, string orderBy, string orderDirection)
        {
            JsonResult result = null;

            var query = (from l in db.Language
                         select new DTOLanguage
                         {
                             Id = l.Id,
                             Name = l.Name,
                             IsActive = l.IsActive,
                             LngCdId = l.LanguageCode.Code
                         }).OrderBy(string.Format("{0} {1}", orderBy, orderDirection)).Skip((records * page) - records).Take(records);


            result = new JsonResult()
            {
                Data = new
                {
                    Languages = query.ToList(),
                    Count = (from l in db.Language
                             select l).Count()

                }
            };

            return result;
        }

        public JsonResult createLanguages([Bind(Include = "Id,Name,Description,IsActive,LngCdId")] Language language)
        {
            JsonResult result = null;

            language.Id = Guid.NewGuid();
            language.LanguageCode = db.LanguageCode.Find(language.LngCdId);

            if (ModelState.IsValid)
            {

                if (db.Language.Where(l => l.Name.Equals(language.Name) && l.LngCdId.Equals(language.LngCdId)).Count() == 0)
                {
                    db.Language.Add(language);

                    db.SaveChanges();

                    result = new JsonResult()
                    {
                        Data = new
                        {
                            Result = true

                        }
                    };
                }
                else
                {
                    result = new JsonResult()
                    {
                        Data = new
                        {
                            Result = false,
                            Message = "Duplicate entry"

                        }
                    };
                }
            }
            return result;
        }

        public JsonResult updateLanguages([Bind(Include = "Id,Name,Description,IsActive,LngCdId")] Language language)
        {
            JsonResult result = null;

            if (ModelState.IsValid)
            {
                var to_update = db.Language.Find(language.Id);

                to_update.IsActive = language.IsActive;
                to_update.LngCdId = language.LngCdId;
                to_update.LanguageCode = db.LanguageCode.Find(language.LngCdId);
                to_update.Name = language.Name;

                db.SaveChanges();

                result = new JsonResult()
                {
                    Data = new
                    {
                        Result = true

                    }
                };
            }
            return result;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
