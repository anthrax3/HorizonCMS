using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Horizon.Web.Models;
using Horizon.Web.DAL;

namespace Horizon.Web.Areas.Admin.Controllers
{
    [Authorize]
    public class PostsController : BaseController
    {

        public PostsController()
            : base()
        { }

        // GET: /Admin/Posts/
        public ActionResult Index()
        {
            var post = db.Post.Include(p => p.Blog).Include(p => p.Category).Include(p => p.PostType);
            return View(post.ToList());
        }

        // GET: /Admin/Posts/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db.Post.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        // GET: /Admin/Posts/Create
        public ActionResult Create()
        {
            ViewBag.BlgId = new SelectList(db.Blog, "Id", "Name");
            ViewBag.CatId = new SelectList(db.PostCategories, "Id", "Name");
            ViewBag.PstId = new SelectList(db.PostTypes, "Id", "Name");
            using (Identity.CustomUserStore store = new Identity.CustomUserStore())
            {
                ViewBag.UserId = store.GetUserIdByName(User.Identity.Name);
            }
            return View();
        }

        // POST: /Admin/Posts/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,Content,Excerpt,Viewed,Loved,Shared,Url,CreateDate,BlgId,AuthId,CatId,PstId")] Post post)
        {
            if (ModelState.IsValid)
            {
                post.Id = Guid.NewGuid();

                post.Blog = (from blog in db.Blog
                             where blog.Id.Equals(post.BlgId)
                             select blog).SingleOrDefault();

                post.Author = (from user in db.User
                               where user.Id.Equals(post.AuthId)
                               select user).SingleOrDefault();

                post.Category = (from postcategory in db.PostCategories
                                 where postcategory.Id.Equals(post.CatId)
                                 select postcategory).SingleOrDefault();

                post.PostType = (from posttype in db.PostTypes
                                 where posttype.Id.Equals(post.PstId)
                                 select posttype).SingleOrDefault();

                db.Post.Add(post);
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            ViewBag.BlgId = new SelectList(db.Blog, "Id", "Name", post.BlgId);
            ViewBag.CatId = new SelectList(db.PostCategories, "Id", "Name", post.CatId);
            ViewBag.PstId = new SelectList(db.PostTypes, "Id", "Name", post.PstId);
            return View(post);
        }

        // GET: /Admin/Posts/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db.Post.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            ViewBag.BlgId = new SelectList(db.Blog, "Id", "Name", post.BlgId);
            ViewBag.CatId = new SelectList(db.PostCategories, "Id", "Name", post.CatId);
            ViewBag.PstId = new SelectList(db.PostTypes, "Id", "Name", post.PstId);
            return View(post);
        }

        // POST: /Admin/Posts/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,Content,Excerpt,Viewed,Loved,Shared,Url,CreateDate,BlgId,AuthId,CatId,PstId")] Post post)
        {
            if (ModelState.IsValid)
            {
                db.Entry(post).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BlgId = new SelectList(db.Blog, "Id", "Name", post.BlgId);
            ViewBag.CatId = new SelectList(db.PostCategories, "Id", "Name", post.CatId);
            ViewBag.PstId = new SelectList(db.PostTypes, "Id", "Name", post.PstId);
            return View(post);
        }

        // GET: /Admin/Posts/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db.Post.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        // POST: /Admin/Posts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Post post = db.Post.Find(id);
            db.Post.Remove(post);
            db.SaveChanges();
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
