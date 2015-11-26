using System;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Net;
using System.Web.Mvc;
using Horizon.Web.Models;

namespace Horizon.Web.Areas.Admin.Controllers
{
    public class PostCategoriesController : BaseController
    {

        public PostCategoriesController() : base()
        { }

        // GET: /Admin/PostCategories/
        public async Task<ActionResult> Index()
        {
            return View(await db.PostCategories.ToListAsync());
        }

        // GET: /Admin/PostCategories/Details/5
        public async Task<ActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PostCategory postcategory = await db.PostCategories.FindAsync(id);
            if (postcategory == null)
            {
                return HttpNotFound();
            }
            return View(postcategory);
        }

        // GET: /Admin/PostCategories/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Admin/PostCategories/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include="Id,Name,CategoryName,Description")] PostCategory postcategory)
        {
            if (ModelState.IsValid)
            {
                postcategory.Id = Guid.NewGuid();
                db.PostCategories.Add(postcategory);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(postcategory);
        }

        // GET: /Admin/PostCategories/Edit/5
        public async Task<ActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PostCategory postcategory = await db.PostCategories.FindAsync(id);
            if (postcategory == null)
            {
                return HttpNotFound();
            }
            return View(postcategory);
        }

        // POST: /Admin/PostCategories/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include="Id,Name,CategoryName,Description")] PostCategory postcategory)
        {
            if (ModelState.IsValid)
            {
                db.Entry(postcategory).State = System.Data.Entity.EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(postcategory);
        }

        // GET: /Admin/PostCategories/Delete/5
        public async Task<ActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PostCategory postcategory = await db.PostCategories.FindAsync(id);
            if (postcategory == null)
            {
                return HttpNotFound();
            }
            return View(postcategory);
        }

        // POST: /Admin/PostCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(Guid id)
        {
            PostCategory postcategory = await db.PostCategories.FindAsync(id);
            db.PostCategories.Remove(postcategory);
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
