using System;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Net;
using System.Web.Mvc;
using Horizon.Web.Models;
using Horizon.Web.DAL;
using System.Web.Hosting;

namespace Horizon.Web.Areas.Admin.Controllers
{
    public class TemplatesController : Horizon.Web.Areas.Admin.Controllers.BaseController
    {
        private HorizonContext db = new HorizonContext();

        public TemplatesController() : base() {}

        // GET: /Admin/Templates/
        public async Task<ActionResult> Index()
        {
            return View(await db.Template.ToListAsync());
        }

        // GET: /Admin/Templates/Details/5
        public async Task<ActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Template template = await db.Template.FindAsync(id);

            if (template == null)
            {
                return HttpNotFound();
            }

            template.Content = System.IO.File.ReadAllText(HostingEnvironment.MapPath(string.Format("~/Views/Shared/_{0}.cshtml", template.Name)));
            return View(template);
        }

        // GET: /Admin/Templates/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Admin/Templates/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include="Id,Name,Content,CreateDate,UpdateDate,IsActive")] Template template)
        {
            if (ModelState.IsValid)
            {
                template.Id = Guid.NewGuid();
                db.Template.Add(template);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(template);
        }

        // GET: /Admin/Templates/Edit/5
        public async Task<ActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Template template = await db.Template.FindAsync(id);
            if (template == null)
            {
                return HttpNotFound();
            }

            template.Content = System.IO.File.ReadAllText(HostingEnvironment.MapPath(string.Format("~/Views/Shared/_{0}.cshtml", template.Name)));
            return View(template);
        }

        // POST: /Admin/Templates/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include="Id,Name,Content,CreateDate,UpdateDate,IsActive")] Template template)
        {
            if (ModelState.IsValid)
            {
                db.Entry(template).State = System.Data.Entity.EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(template);
        }

        // GET: /Admin/Templates/Delete/5
        public async Task<ActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Template template = await db.Template.FindAsync(id);
            if (template == null)
            {
                return HttpNotFound();
            }
            return View(template);
        }

        // POST: /Admin/Templates/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(Guid id)
        {
            Template template = await db.Template.FindAsync(id);
            db.Template.Remove(template);
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
