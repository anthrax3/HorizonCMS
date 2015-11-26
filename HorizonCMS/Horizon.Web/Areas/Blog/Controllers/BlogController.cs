using Horizon.Web.DAL;
using System;
using System.Web.Mvc;

namespace Horizon.Web.Areas.Blog.Controllers
{
    public class BlogController : BaseController
    {

        #region Properties

        private BlogManager Manager { get; set; }

        #endregion

        #region Ctor.

        public BlogController()
            : this(new BlogManager())
        { }

        public BlogController(BlogManager dal)
        {
            Manager = dal;

            var blog = Manager.GetDefault();

            ViewBag.Title = blog.Title;
            ViewBag.Name = blog.Description;
        }

        #endregion

        #region Index

        //
        // GET: /Blog/
        [HttpGet]
        public ActionResult Index()
        {
            ViewBag.Categories = Manager.GetAllCategories();
            ViewBag.MostReadPosts = Manager.GetMostReadPosts(5);
            return View(Manager.GetDefault());
        }

        #endregion

        #region Categories

        /// <summary>
        /// Shows all available categories.
        /// </summary>
        /// <returns>ActionResult</returns>
        [HttpGet]

        public ActionResult Categories()
        {
            ViewBag.BreadCrumb = GenerateBreadcrumb(new string[] { "Home", "Categories" });
            ViewBag.Categories = Manager.GetAllCategories();
            return View(Manager.GetAllCategories());
        }

        [HttpGet]
        public ActionResult Category(string categoryname)
        {
            ViewBag.BreadCrumb = GenerateBreadcrumb(new string[] { "Home", "Categories", categoryname });
            ViewBag.Categories = Manager.GetAllCategories();
            ViewBag.MostReadPosts = Manager.GetMostReadPosts(5);

            if (!string.IsNullOrEmpty(categoryname))
            {
                ViewBag.Posts = Manager.GetAllByCategory(categoryname);
                return View(Manager.GetDefault());
            }
            else
                return HttpNotFound();

        }

        #endregion

        #region Posts

        /// <summary>
        /// 
        /// </summary>
        /// <param name="year">Year</param>
        /// <param name="month">Month</param>
        /// <param name="day">Day</param>
        /// <param name="postname">Post's name</param>
        /// <returns></returns>
        [HttpGet]

        public ActionResult Post(string year, string month, string day, string postname)
        {
            if (!string.IsNullOrEmpty(year) && !string.IsNullOrEmpty(month) && !string.IsNullOrEmpty(day) && !string.IsNullOrEmpty(postname))
            {
                ViewBag.Categories = Manager.GetAllCategories();
                ViewBag.MostReadPosts = Manager.GetMostReadPosts(5);
   
                var post = Manager.GetPostByNameAndDate(DateTime.Parse(string.Format("{2}-{1}-{0}", year, month, day)), postname.Replace(' ', '-'));

                if (post != null)
                {
                    Manager.AddView(post);

                    ViewBag.Breadcrumb = GenerateBreadcrumb(new string[] { "Home", post.Category.Name, post.Title });
                    ViewBag.RelatedPosts = Manager.GetRelatedPosts(post);
                    return View(post);
                }
                else
                    return HttpNotFound();
            }
            else
            {
                return HttpNotFound();
            }
        }       

        #endregion

        [HttpPost]
        //[Route("{culture}/blog/search/{searchterm}", Name = "blog-post-search")]
        public ActionResult Search(string searchterm)
        {
            
            ViewBag.Categories = Manager.GetAllCategories();
            ViewBag.MostReadPosts = Manager.GetMostReadPosts(5);
            ViewBag.BreadCrumb = GenerateBreadcrumb(new string[] { "Home", "Search results", searchterm });

            if (!string.IsNullOrEmpty(searchterm))
            {
                return View(Manager.SearchByTitle(searchterm));

            }

            return View();
        }

        #region Private methods

        private static Models.BreadCrumb GenerateBreadcrumb(string[] path)
        {
            if (path != null && path.Length > 0)
            {
                Models.BreadCrumb result = new Models.BreadCrumb();
                result.parameters = new System.Collections.Generic.List<string>();

                foreach (string s in path)
                {
                    result.parameters.Add(s);
                }

                return result;
            }
            else
                return null;
        }

        #endregion

        #region Dispose

        /// <summary>
        /// Disposes Blog controller and it's managed resources
        /// </summary>
        /// <param name="disposing">Dipose or not managed resources</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
                Manager.Dispose();
            base.Dispose(disposing);
        }

        #endregion
    }
}