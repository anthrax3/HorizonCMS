using System;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Net;
using System.Web.Mvc;
using Horizon.Web.Models;
using Horizon.Web.DAL;
using System.Linq;

namespace Horizon.Web.Areas.Admin.Controllers
{
    public class TranslationsController : BaseController
    {
        
        public TranslationsController()
            : base()
        { }

        // GET: /Admin/Translations/
        public async Task<ActionResult> Index()
        {
            var translation = db.Translation.Include(t => t.Language);
            return View(await translation.ToListAsync());
        }

        // GET: /Admin/Translations/Details/5
        public async Task<ActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Translation translation = await db.Translation.FindAsync(id);
            if (translation == null)
            {
                return HttpNotFound();
            }
            return View(translation);
        }

        // GET: /Admin/Translations/Create
        public ActionResult Create()
        {
            ViewBag.LngId = new SelectList(db.Language, "Id", "Name");
            return View();
        }

        // POST: /Admin/Translations/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Name,Content,LngId")] Translation translation)
        {
            if (ModelState.IsValid)
            {
                if ((from translations in db.Translation
                     where translations.Name.Equals(translation.Name) && translations.LngId.Equals(translation.LngId)
                     select translations).SingleOrDefault() == null)
                {


                    translation.Id = Guid.NewGuid();
                    translation.Language = (from languages in db.Language where languages.Id.Equals(translation.LngId) select languages).SingleOrDefault();
                    db.Translation.Add(translation);
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
            }

            ViewBag.LngId = new SelectList(db.Language, "Id", "Name", translation.LngId);
            return View(translation);
        }

        // GET: /Admin/Translations/Edit/5
        public async Task<ActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Translation translation = await db.Translation.FindAsync(id);
            if (translation == null)
            {
                return HttpNotFound();
            }
            ViewBag.LngId = new SelectList(db.Language, "Id", "Name", translation.LngId);
            return View(translation);
        }

        // POST: /Admin/Translations/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Name,Content,LngId")] Translation translation)
        {
            if (ModelState.IsValid)
            {
                db.Entry(translation).State = System.Data.Entity.EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.LngId = new SelectList(db.Language, "Id", "Name", translation.LngId);
            return View(translation);
        }

        // GET: /Admin/Translations/Delete/5
        public async Task<ActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Translation translation = await db.Translation.FindAsync(id);
            if (translation == null)
            {
                return HttpNotFound();
            }
            return View(translation);
        }

        // POST: /Admin/Translations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(Guid id)
        {
            Translation translation = await db.Translation.FindAsync(id);
            db.Translation.Remove(translation);
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
