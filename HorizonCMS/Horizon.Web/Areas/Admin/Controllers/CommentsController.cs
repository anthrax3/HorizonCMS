using System;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Net;
using System.Web.Mvc;
using Horizon.Web.Models;
using Horizon.Web.DAL;

namespace Horizon.Web.Areas.Admin.Controllers
{
    public class CommentsController : BaseController
    {
        
        public CommentsController() : base()
        {
        }

        // GET: /Admin/Comments/
        public async Task<ActionResult> Index()
        {
            var comment = db.Comment.Include(c => c.Post);
            return View(await comment.ToListAsync());
        }

        // GET: /Admin/Comments/Details/5
        public async Task<ActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comment comment = await db.Comment.FindAsync(id);
            if (comment == null)
            {
                return HttpNotFound();
            }
            return View(comment);
        }

        // GET: /Admin/Comments/Create
        public ActionResult Create()
        {
            ViewBag.PstId = new SelectList(db.Post, "Id", "Title");
            return View();
        }

        // POST: /Admin/Comments/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include="Id,Content,IsActive,CreateDate,PstId")] Comment comment)
        {
            if (ModelState.IsValid)
            {
                comment.Id = Guid.NewGuid();
                db.Comment.Add(comment);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.PstId = new SelectList(db.Post, "Id", "Title", comment.PstId);
            return View(comment);
        }

        // GET: /Admin/Comments/Edit/5
        public async Task<ActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comment comment = await db.Comment.FindAsync(id);
            if (comment == null)
            {
                return HttpNotFound();
            }
            ViewBag.PstId = new SelectList(db.Post, "Id", "Title", comment.PstId);
            return View(comment);
        }

        // POST: /Admin/Comments/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include="Id,Content,IsActive,CreateDate,PstId")] Comment comment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(comment).State = System.Data.Entity.EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.PstId = new SelectList(db.Post, "Id", "Title", comment.PstId);
            return View(comment);
        }

        // GET: /Admin/Comments/Delete/5
        public async Task<ActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comment comment = await db.Comment.FindAsync(id);
            if (comment == null)
            {
                return HttpNotFound();
            }
            return View(comment);
        }

        // POST: /Admin/Comments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(Guid id)
        {
            Comment comment = await db.Comment.FindAsync(id);
            db.Comment.Remove(comment);
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
