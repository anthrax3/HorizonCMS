using Horizon.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Objects.SqlClient;

namespace Horizon.Web.DAL
{
    public class BlogManager : IDisposable
    {

        #region Properties

        private HorizonContext db { get; set; }

        #endregion

        #region Ctor.

        public BlogManager()
        {
            db = new HorizonContext();
        }

        #endregion

        #region Methods

        public Blog GetDefault()
        {
            return (from blogs in db.Blog
                    select blogs).SingleOrDefault();
        }


        public ICollection<PostCategory> GetAllCategories()
        {
            return (from categories in db.PostCategories
                    orderby categories.Posts.Count() descending
                    select categories).ToList<PostCategory>();
        }

        public ICollection<Post> GetAllByCategory(string categoryName)
        {
            return (from post in db.Post
                    where post.Category.CategoryName.Equals(categoryName)
                    orderby post.CreateDate descending
                    select post).ToList<Post>();
        }

        public ICollection<Post> GetMostReadPosts(int count)
        {
            return (from post in db.Post
                    orderby post.Viewed descending
                    select post).ToList<Post>();
        }

        public Post GetPostByNameAndDate(DateTime date, string name)
        {
            return (from post in db.Post
                    where post.Url.Equals(name)
                    select post).SingleOrDefault();
        }

        public ICollection<Post> SearchByTitle(string searchterm)
        {
            return (from posts in db.Post
                    where posts.Title.Contains(searchterm)
                    select posts).ToList();
        }

        public ICollection<Post> SearchInContent(string searchterm)
        {
            return db.Post.Where(p => SqlFunctions.PatIndex(searchterm, p.Content) > 0).ToList<Post>();
        }

        public ICollection<Post> GetRelatedPosts(Post post)
        {
            return (from posts in db.Post
                    orderby posts.CreateDate descending
                    where posts.Category.Id.Equals(post.Category.Id) && posts.Id != post.Id
                    select posts).ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="post"></param>
        public void AddView(Post post)
        {
            post.Viewed += 1;
            db.Entry(post).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="post"></param>
        public void AddLove(Post post)
        {
            post.Loved += 1;
            db.Entry(post).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="post"></param>
        public void AddShared(Post post)
        {
            post.Shared += 1;
            db.Entry(post).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();

        }

        public bool checkIfExists(Blog blog)
        {
            return (from blogs in db.Blog
                    where blogs.Name.Equals(blog.Name)
                    select blogs).Count() > 0;
        }

        #endregion

        #region Dispose

        ~BlogManager()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
                db.Dispose();
        }

        #endregion

    }

}