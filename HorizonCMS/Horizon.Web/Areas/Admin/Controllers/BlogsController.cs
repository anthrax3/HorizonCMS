using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Horizon.Web.Models;
using Horizon.Web.DAL;
using Horizon.Web.DTOModels;
using System.Linq.Dynamic;

namespace Horizon.Web.Areas.Admin.Controllers
{
    public class BlogsController : BaseController
    {


        public BlogsController()
            : base()
        {

        }

        // GET: /Admin/Blogs/
        public ActionResult Index()
        {
            return View();
        }

        #region JSON Methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="page"></param>
        /// <param name="records"></param>
        /// <param name="orderBy"></param>
        /// <param name="orderDirection"></param>
        /// <returns></returns>
        public JsonResult getBlogs(int page, int records, string orderBy, string orderDirection)
        {
            JsonResult result = null;

            var query = (from b in db.Blog
                         select new DTOBlog
                         {
                             Id = b.Id,
                             Name = b.Name,
                             Description = b.Description,
                             IsActive = b.IsActive
                         }).OrderBy(string.Format("{0} {1}", orderBy, orderDirection)).Skip((records * page) - records).Take(records);

            result = new JsonResult()
            {
                Data = new
                {
                    Blogs = query.ToList(),
                    Count = (from b in db.Blog
                             select new DTOBlog
                             {
                                 Id = b.Id,
                                 Name = b.Name,
                                 Description = b.Description,
                                 IsActive = b.IsActive
                             }).Count()
                }
            };

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="page"></param>
        /// <param name="records"></param>
        /// <param name="orderBy"></param>
        /// <param name="orderDirection"></param>
        /// <returns></returns>
        public JsonResult getPosts(int page, int records, string orderBy, string orderDirection)
        {
            JsonResult result = null;

            var query = (from p in db.Post
                         select new Horizon.Web.Areas.Admin.Models.DTOBlogPosts
                         {
                             Id = p.Id,
                             Title = p.Title,
                             Loved = p.Loved,
                             Viewed = p.Viewed
                         }).OrderBy(string.Format("{0} {1}", orderBy, orderDirection)).Skip((records * page) - records).Take(records);

            result = new JsonResult()
            {
                Data = new
                {
                    Posts = query.ToList(),
                    Count = (from b in db.Post
                             select b).Count()
                }
            };

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="page"></param>
        /// <param name="records"></param>
        /// <param name="orderBy"></param>
        /// <param name="orderDirection"></param>
        /// <returns></returns>
        public JsonResult getPostsTypes(int page, int records, string orderBy, string orderDirection)
        {
            JsonResult result = null;

            var query = (from p in db.PostTypes
                         select new Horizon.Web.Areas.Admin.Models.DTOBlogPostsTypes
                         {
                             Id = p.Id,
                             Name = p.Name,
                             Description = p.Description
                         }).OrderBy(string.Format("{0} {1}", orderBy, orderDirection)).Skip((records * page) - records).Take(records);

            result = new JsonResult()
            {
                Data = new
                {
                    Posts = query.ToList(),
                    Count = (from p in db.PostTypes
                             select p).Count()
                }
            };

            return result;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="page"></param>
        /// <param name="records"></param>
        /// <param name="orderBy"></param>
        /// <param name="orderDirection"></param>
        /// <returns></returns>
        public JsonResult getPostsCategories(int page, int records, string orderBy, string orderDirection)
        {
            JsonResult result = null;

            var query = (from p in db.PostCategories
                         select new Horizon.Web.Areas.Admin.Models.DTOBlogPostsCategories
                         {
                             Id = p.Id,
                             Name = p.Name,
                             Description = p.Description
                         }).OrderBy(string.Format("{0} {1}", orderBy, orderDirection)).Skip((records * page) - records).Take(records);

            result = new JsonResult()
            {
                Data = new
                {
                    Posts = query.ToList(),
                    Count = (from p in db.PostCategories
                             select p).Count()
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
