using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;

namespace Horizon.DAL.Manager.Entities
{
    public class SectionsManager : Interfaces.IManager<Tables.Entities.Section>, IDisposable
    {

        #region Properties

        private HorizonContext Context { get; set; }

        #endregion

        #region Ctor.

        public SectionsManager()
            : this(new HorizonContext())
        { }

        public SectionsManager(HorizonContext context)
        {
            Context = context;
        }

        #endregion


        #region Methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public Tables.Entities.Section Create(Tables.Entities.Section obj)
        {
            if (obj != null)
            {
                Context.Section.Add(obj);
                Context.SaveChanges();

                return obj;
            }
            else
                return null;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="page"></param>
        /// <param name="records"></param>
        /// <param name="sortField"></param>
        /// <param name="sortOrder"></param>
        /// <returns></returns>
        public List<Tables.Entities.Section> Retrieve(int page, int records, string sortField, string sortOrder)
        {
            return (from sections in Context.Section
                    select sections).OrderBy(string.Format("{0} {1}", sortField, sortOrder)).Skip((records * page) - records).Take(records).ToList();

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public Tables.Entities.Section Update(Tables.Entities.Section obj)
        {
            var to_update = Context.Section.Find(obj.Id);
            to_update = obj;
            Context.SaveChanges();

            return obj;

        }

        public bool Delete(Tables.Entities.Section obj)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Dispose

        public void Dispose()
        {
            Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
                Context.Dispose();
        }

        #endregion
    }
}
