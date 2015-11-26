using System;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Net;
using System.Web.Mvc;
using Horizon.Web.Models;
using Horizon.Web.DAL;

namespace Horizon.Web.Areas.Admin.Controllers
{
    public class KeywordsController : BaseController
    {
        

        public KeywordsController() :base()
        {

        }

        // GET: /Admin/Keywords/
        public async Task<ActionResult> Index()
        {
            return View(await db.Keyword.ToListAsync());
        }

        // GET: /Admin/Keywords/Details/5
        public async Task<ActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Keyword keyword = await db.Keyword.FindAsync(id);
            if (keyword == null)
            {
                return HttpNotFound();
            }
            return View(keyword);
        }

        // GET: /Admin/Keywords/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Admin/Keywords/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include="Id,KeywordName")] Keyword keyword)
        {
            if (ModelState.IsValid)
            {
                keyword.Id = Guid.NewGuid();
                db.Keyword.Add(keyword);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(keyword);
        }

        // GET: /Admin/Keywords/Edit/5
        public async Task<ActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Keyword keyword = await db.Keyword.FindAsync(id);
            if (keyword == null)
            {
                return HttpNotFound();
            }
            return View(keyword);
        }

        // POST: /Admin/Keywords/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include="Id,KeywordName")] Keyword keyword)
        {
            if (ModelState.IsValid)
            {
                db.Entry(keyword).State = System.Data.Entity.EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(keyword);
        }

        // GET: /Admin/Keywords/Delete/5
        public async Task<ActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Keyword keyword = await db.Keyword.FindAsync(id);
            if (keyword == null)
            {
                return HttpNotFound();
            }
            return View(keyword);
        }

        // POST: /Admin/Keywords/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(Guid id)
        {
            Keyword keyword = await db.Keyword.FindAsync(id);
            db.Keyword.Remove(keyword);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
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
