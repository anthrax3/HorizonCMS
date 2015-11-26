using System;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Net;
using System.Web.Mvc;
using Horizon.Web.Models;
using Horizon.Web.DAL;
using System.Linq;
using System.Linq.Dynamic;
using Horizon.Web.Areas.Admin.Models;

namespace Horizon.Web.Areas.Admin.Controllers
{
    public class UsersController : BaseController
    {

        public UsersController()
            : base()
        { }

        // GET: /Admin/Users/
        public async Task<ActionResult> Index()
        {
            return View(await db.User.ToListAsync());
        }

        // GET: /Admin/Users/Details/5
        public async Task<ActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = await db.User.FindAsync(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: /Admin/Users/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Admin/Users/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Name,Email,Password,IsActive,IsLocked,CreateDate")] User user)
        {
            if (ModelState.IsValid)
            {
                user.Id = Guid.NewGuid();
                db.User.Add(user);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(user);
        }

        // GET: /Admin/Users/Edit/5
        public async Task<ActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = await db.User.FindAsync(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: /Admin/Users/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Name,Email,Password,IsActive,IsLocked,CreateDate")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = System.Data.Entity.EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(user);
        }

        // GET: /Admin/Users/Delete/5
        public async Task<ActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = await db.User.FindAsync(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: /Admin/Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(Guid id)
        {
            User user = await db.User.FindAsync(id);
            db.User.Remove(user);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        #region JSON Methods

        public JsonResult getUsers(int page, int records, string orderBy, string orderDirection)
        {
            JsonResult result = null;
            var query = (from u in db.User
                         select new DTOUser
                         {
                             Id = u.Id,
                             Name = u.Name,
                             Email = u.Email,
                             IsLocked = u.IsLocked,
                             CreateDate = u.CreateDate,
                             ProfilePhoto = u.Profile.Image,
                             Profile = u.Profile.Id,                             
                             IsActive = u.IsActive
                         }).OrderBy(string.Format("{0} {1}", orderBy, orderDirection)).Skip((records * page) - records).Take(records);

            result = new JsonResult()
            {
                Data = new
                {
                    Users = query.ToList(),
                    Count = (from u in db.User
                             select u).Count()
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
