using System;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Net;
using System.Web.Mvc;
using Horizon.Web.Models;

namespace Horizon.Web.Areas.Admin.Controllers
{
    public class PostTypesController : BaseController
    {
      
        // GET: /Admin/PostTypes/
        public async Task<ActionResult> Index()
        {
            return View(await db.PostTypes.ToListAsync());
        }

        // GET: /Admin/PostTypes/Details/5
        public async Task<ActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PostType posttype = await db.PostTypes.FindAsync(id);
            if (posttype == null)
            {
                return HttpNotFound();
            }
            return View(posttype);
        }

        // GET: /Admin/PostTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Admin/PostTypes/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include="Id,Name,Description,PstId")] PostType posttype)
        {
            if (ModelState.IsValid)
            {
                posttype.Id = Guid.NewGuid();
                db.PostTypes.Add(posttype);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(posttype);
        }

        // GET: /Admin/PostTypes/Edit/5
        public async Task<ActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PostType posttype = await db.PostTypes.FindAsync(id);
            if (posttype == null)
            {
                return HttpNotFound();
            }
            return View(posttype);
        }

        // POST: /Admin/PostTypes/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include="Id,Name,Description,PstId")] PostType posttype)
        {
            if (ModelState.IsValid)
            {
                db.Entry(posttype).State = System.Data.Entity.EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(posttype);
        }

        // GET: /Admin/PostTypes/Delete/5
        public async Task<ActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PostType posttype = await db.PostTypes.FindAsync(id);
            if (posttype == null)
            {
                return HttpNotFound();
            }
            return View(posttype);
        }

        // POST: /Admin/PostTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(Guid id)
        {
            PostType posttype = await db.PostTypes.FindAsync(id);
            db.PostTypes.Remove(posttype);
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
