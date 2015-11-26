using System;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Net;
using System.Web.Mvc;
using Horizon.Web.Models;
using System.Linq;
using System.Linq.Dynamic;
using Horizon.Web.Areas.Admin.Models;

namespace Horizon.Web.Areas.Admin.Controllers
{
    public class RolesController : BaseController
    {
       

        #region Ctor.

        public RolesController()
            : base()
        { }

        #endregion

        // GET: /Admin/Roles/
        public async Task<ActionResult> Index()
        {
            return View(await db.Role.ToListAsync());
        }

        // GET: /Admin/Roles/Details/5
        public async Task<ActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Role role = await db.Role.FindAsync(id);
            if (role == null)
            {
                return HttpNotFound();
            }
            return View(role);
        }

        // GET: /Admin/Roles/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Admin/Roles/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Name,Description,UserId")] Role role)
        {
            if (ModelState.IsValid)
            {
                role.Id = Guid.NewGuid();
                db.Role.Add(role);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(role);
        }

        // GET: /Admin/Roles/Edit/5
        public async Task<ActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Role role = await db.Role.FindAsync(id);
            if (role == null)
            {
                return HttpNotFound();
            }
            return View(role);
        }

        // POST: /Admin/Roles/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Name,Description,UserId")] Role role)
        {
            if (ModelState.IsValid)
            {
                db.Entry(role).State = System.Data.Entity.EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(role);
        }

        // GET: /Admin/Roles/Delete/5
        public async Task<ActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Role role = await db.Role.FindAsync(id);
            if (role == null)
            {
                return HttpNotFound();
            }
            return View(role);
        }

        // POST: /Admin/Roles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(Guid id)
        {
            Role role = await db.Role.FindAsync(id);
            db.Role.Remove(role);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        #region JSON Methods

        public JsonResult getRoles(int page, int records, string orderBy, string orderDirection)
        {
            JsonResult result = null;

            var query = (from r in db.Role
                         select new DTORole
                         {
                             Id = r.Id,
                             Name = r.Name,
                             Description = r.Description
                         }).OrderBy(string.Format("{0} {1}", orderBy, orderDirection)).Skip((records * page) - records).Take(records);

            result = new JsonResult()
            {
                Data = new
                {
                    Roles = query.ToList(),
                    Count = (from r in db.Role
                             select r).Count()
                }
            };

            return result;
        }

        #endregion

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
