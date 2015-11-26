using System;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Net;
using System.Web.Mvc;
using Horizon.Web.Models;
using Horizon.Web.DAL;

namespace Horizon.Web.Areas.Admin.Controllers
{
    public class LanguageCodesController : BaseController
    {
       


        public LanguageCodesController() : base()
        {
        }

        // GET: /Admin/LanguageCodes/
        public async Task<ActionResult> Index()
        {
            return View(await db.LanguageCode.ToListAsync());
        }

        // GET: /Admin/LanguageCodes/Details/5
        public async Task<ActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LanguageCode languagecode = await db.LanguageCode.FindAsync(id);
            if (languagecode == null)
            {
                return HttpNotFound();
            }
            return View(languagecode);
        }

        // GET: /Admin/LanguageCodes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Admin/LanguageCodes/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include="Id,Code,LngId")] LanguageCode languagecode)
        {
            if (ModelState.IsValid)
            {
                languagecode.Id = Guid.NewGuid();
                db.LanguageCode.Add(languagecode);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(languagecode);
        }

        // GET: /Admin/LanguageCodes/Edit/5
        public async Task<ActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LanguageCode languagecode = await db.LanguageCode.FindAsync(id);
            if (languagecode == null)
            {
                return HttpNotFound();
            }
            return View(languagecode);
        }

        // POST: /Admin/LanguageCodes/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include="Id,Code,LngId")] LanguageCode languagecode)
        {
            if (ModelState.IsValid)
            {
                db.Entry(languagecode).State = System.Data.Entity.EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(languagecode);
        }

        // GET: /Admin/LanguageCodes/Delete/5
        public async Task<ActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LanguageCode languagecode = await db.LanguageCode.FindAsync(id);
            if (languagecode == null)
            {
                return HttpNotFound();
            }
            return View(languagecode);
        }

        // POST: /Admin/LanguageCodes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(Guid id)
        {
            LanguageCode languagecode = await db.LanguageCode.FindAsync(id);
            db.LanguageCode.Remove(languagecode);
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
