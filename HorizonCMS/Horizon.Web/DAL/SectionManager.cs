using Horizon.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Horizon.Web.DAL
{
    public class SectionManager : IDisposable
    {
         #region Properties

        private HorizonContext db { get; set; }

        #endregion

        #region Ctor.

        public SectionManager()
        {
            db = new HorizonContext();
        }

        #endregion

        #region Methods

     
        public ICollection<Section> GetAllSections()
        {
            return (from sections in db.Section
                    orderby sections.SortOrder ascending
                    select sections).ToList();
        
        }

        public ICollection<Section> getSectionsByUser(string userName)
        {

            return db.Section.OrderBy(s => s.SortOrder).Where(s => s.Roles.Any(r => (db.User.Where(u => u.Name.Equals(userName)).FirstOrDefault().Roles.Select(ur => ur.Name).ToList()).Contains(r.Name.ToString()))).ToList();
           
            //
        }

        #endregion

        #region Dispose

        ~SectionManager()
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