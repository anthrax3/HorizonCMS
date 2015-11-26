using System;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Net;
using System.Web.Mvc;
using Horizon.Web.Models;
using System.Text;
using System.Linq;
using System.Web;
using System.Collections.Generic;
using Horizon.Web.DTOModels;
using System.Linq.Dynamic;

namespace Horizon.Web.Areas.Admin.Controllers
{
    public class PagesController : BaseController
    {
        #region Ctor.

        public PagesController() : base() { }

        #endregion

        #region Methods

        // GET: /Admin/Pages/
        public async Task<ActionResult> Index()
        {
            var page = db.Page.Include(p => p.Language).Include(p => p.Template);
            SetUpPageData();
            return View(await page.ToListAsync());
        }

        // GET: /Admin/Pages/Details/5
        public async Task<ActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Page page = await db.Page.FindAsync(id);
            if (page == null)
            {
                return HttpNotFound();
            }
            return View(page);
        }

        // GET: /Admin/Pages/Create
        public ActionResult Create()
        {
            SetUpPageData();
            return View();
        }

        // POST: /Admin/Pages/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Name,Title,Content,TplId,LngId,PrntId,IsDefault,Description")] Page page, List<string> Keywords)
        {
            if (ModelState.IsValid)
            {
                page.Id = Guid.NewGuid();
                page.CreateDate = DateTime.Now;

                // Set page language
                page.Language = (from languages in db.Language
                                 where languages.Id.Equals(page.LngId)
                                 select languages).SingleOrDefault();

                // Set page template
                page.Template = (from templates in db.Template
                                 where templates.Id.Equals(page.TplId)
                                 select templates).SingleOrDefault();
                if (page.PrntId != null)
                {
                    page.Parent = (from pages in db.Page
                                   where pages.Id == page.PrntId
                                   select pages).SingleOrDefault();
                }

                // Set page's name
                page.Name = HttpUtility.UrlDecode(Helpers.UrlHelper.SanitizeUrl(page.Title), Encoding.GetEncoding("iso-8859-1"));

                foreach (string keyword in Keywords)
                {
                    page.Keywords.Add(db.Keyword.Find(Guid.Parse(keyword)));
                }

                db.Page.Add(page);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            SetUpPageData();
            return View(page);
        }

        // GET: /Admin/Pages/Edit/5
        public async Task<ActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Page page = await db.Page.FindAsync(id);
            if (page == null)
            {
                return HttpNotFound();
            }
            ViewBag.Keywords = db.Keyword.ToList();
            ViewBag.LngId = new SelectList(db.Language, "Id", "Name", page.LngId);
            ViewBag.TplId = new SelectList(db.Template, "Id", "Name", page.TplId);
            ViewBag.PrntId = new SelectList(db.Page.Where(p => p.Id != id).ToList(), "Id", "Name", page.PrntId);
            return View(page);
        }

        // POST: /Admin/Pages/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id, Name,Title,Content,TplId,LngId,PrntId,IsDefault,Description")] Page editPage, List<string> Keywords)
        {
            if (ModelState.IsValid)
            {
                var page = db.Page.Find(editPage.Id);

                db.Entry(page).State = System.Data.Entity.EntityState.Modified;

                page.Content = editPage.Content;
                page.Title = editPage.Title;


                page.UpdateDate = DateTime.Now;

                page.IsDefault = editPage.IsDefault;

                page.Language = (from languages in db.Language
                                 where languages.Id.Equals(editPage.LngId)
                                 select languages).SingleOrDefault();

                page.Template = (from templates in db.Template
                                 where templates.Id.Equals(editPage.TplId)
                                 select templates).SingleOrDefault();

                page.Name = HttpUtility.UrlDecode(Helpers.UrlHelper.SanitizeUrl(editPage.Title), Encoding.GetEncoding("iso-8859-1"));

                if (page.PrntId != null)
                {
                    page.Parent = (from pages in db.Page
                                   where pages.Id == editPage.PrntId
                                   select pages).SingleOrDefault();
                }
                List<Keyword> currentKeywords = page.Keywords.ToList();

                foreach (var keyword in currentKeywords)
                {
                    page.Keywords.Remove(db.Keyword.Find(keyword.Id));
                }

                foreach (string keyword in Keywords)
                {

                    if (!page.Keywords.Contains(db.Keyword.Find(Guid.Parse(keyword))))

                        page.Keywords.Add(db.Keyword.Find(Guid.Parse(keyword)));
                }



                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.Keywords = db.Keyword.ToList();
            ViewBag.LngId = new SelectList(db.Language, "Id", "Name", editPage.LngId);
            ViewBag.TplId = new SelectList(db.Template, "Id", "Name", editPage.TplId);
            ViewBag.PrntId = new SelectList(db.Page.Where(p => p.Id != editPage.Id).ToList(), "Id", "Name", editPage.PrntId);

            return View(editPage);
        }

        // GET: /Admin/Pages/Delete/5
        public async Task<ActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Page page = await db.Page.FindAsync(id);
            if (page == null)
            {
                return HttpNotFound();
            }
            return View(page);
        }

        // POST: /Admin/Pages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(Guid id)
        {
            Page page = await db.Page.FindAsync(id);
            db.Page.Remove(page);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// 
        /// </summary>
        private void SetUpPageData()
        {
            ViewBag.LngId = new SelectList(db.Language, "Id", "Name");
            ViewBag.TplId = new SelectList(db.Template, "Id", "Name");
            ViewBag.PrntId = new SelectList(db.Page, "Id", "Name");
            ViewBag.Keywords = db.Keyword.ToList();
        }

        #endregion

        public JsonResult getPages(int page, int records, string orderBy, string orderDirection)
        {
            JsonResult result = null;

            var query = (from p in db.Page
                         select new DTOPage
                         {
                             Id = p.Id,
                             Name = p.Name,
                             Description = p.Description,
                             CreateDate = p.CreateDate,
                             IsDefault = p.IsDefault,
                             Language = p.Language.Name,
                             Title = p.Title
                         }).OrderBy(string.Format("{0} {1}", orderBy, orderDirection)).Skip((records * page) - records).Take(records);

          
            result = new JsonResult()
            {
                Data = new
                {
                    Notifications = query.ToList(),
                    Count = (from p in db.Page
                             select p).Count()

                }
            };

            return result;
        }

        #region Dispose

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        #endregion

    }
}
