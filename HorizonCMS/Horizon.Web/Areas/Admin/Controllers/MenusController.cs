using System;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Net;
using System.Web.Mvc;
using Horizon.Web.Models;
using Horizon.Web.DAL;
using System.Linq;
using System.Collections.Generic;

namespace Horizon.Web.Areas.Admin.Controllers
{
    public class MenusController : BaseController
    {
        // GET: /Admin/Menus/
        public async Task<ActionResult> Index()
        {
            var menu = db.Menu.Include(m => m.Template);
            return View(await menu.ToListAsync());
        }

        // GET: /Admin/Menus/Details/5
        public async Task<ActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Menu menu = await db.Menu.FindAsync(id);
            if (menu == null)
            {
                return HttpNotFound();
            }
            return View(menu);
        }

        // GET: /Admin/Menus/Create
        public ActionResult Create()
        {
            ViewBag.LngId = new SelectList(db.Language, "Id", "Name");
            ViewBag.TplId = new SelectList(db.Template, "Id", "Name");
            ViewBag.Pages = db.Page.ToList();

            return View();
        }

        // POST: /Admin/Menus/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Name,TplId,LngId")] Menu menu, List<string> Pages)
        {
            if (ModelState.IsValid)
            {
                if (db.Menu.Where(m => m.TplId == menu.TplId).SingleOrDefault() == null)
                {
                    menu.Id = Guid.NewGuid();

                    menu.Template = db.Template.Find(menu.TplId);
                    menu.Language = db.Language.Find(menu.LngId);

                    foreach (string s in Pages)
                    {
                        menu.Pages.Add(db.Page.Find(Guid.Parse(s)));
                    }

                    db.Menu.Add(menu);

                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                else
                {

                    ViewBag.TplId = new SelectList(db.Template, "Id", "Name");
                    ViewBag.LngId = new SelectList(db.Language, "Id", "Name");
                    ViewBag.Pages = db.Page.ToList();

                    return View(menu);
                }
            }

            ViewBag.TplId = new SelectList(db.Template, "Id", "Name");
            ViewBag.LngId = new SelectList(db.Language, "Id", "Name");
            ViewBag.Pages = db.Page.ToList();
            return View(menu);
        }

        // GET: /Admin/Menus/Edit/5
        public async Task<ActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Menu menu = await db.Menu.FindAsync(id);
            if (menu == null)
            {
                return HttpNotFound();
            }
            ViewBag.Id = new SelectList(db.Template, "Id", "Name", menu.Id);
            return View(menu);
        }

        // POST: /Admin/Menus/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Name,TplId,LngId")] Menu menu)
        {
            if (ModelState.IsValid)
            {
                db.Entry(menu).State = System.Data.Entity.EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.TplId = new SelectList(db.Template, "Id", "Name");
            ViewBag.LngId = new SelectList(db.Language, "Id", "Name");
            ViewBag.Pages = db.Page.ToList();
            return View(menu);
        }

        // GET: /Admin/Menus/Delete/5
        public async Task<ActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Menu menu = await db.Menu.FindAsync(id);
            if (menu == null)
            {
                return HttpNotFound();
            }
            return View(menu);
        }

        // POST: /Admin/Menus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(Guid id)
        {
            Menu menu = await db.Menu.FindAsync(id);
            db.Menu.Remove(menu);
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
