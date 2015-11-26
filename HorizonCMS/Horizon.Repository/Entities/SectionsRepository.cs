using Horizon.DAL.Tables.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Horizon.Repository.Entities
{
    public class SectionsRepository : Interfaces.IRepository<DTO.DTOSections, Section>, IDisposable
    {

        #region Properties

        private Horizon.DAL.Manager.Entities.SectionsManager Context { get; set; }

        #endregion

        #region Ctor.

        public SectionsRepository()
            : this(new Horizon.DAL.Manager.Entities.SectionsManager())
        { }

        public SectionsRepository(Horizon.DAL.Manager.Entities.SectionsManager context)
        {
            Context = context;
        }

        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="page"></param>
        /// <param name="records"></param>
        /// <param name="sortField"></param>
        /// <param name="sortOrder"></param>
        /// <returns></returns>
        public List<DTO.DTOSections> GetPaged(int page, int records, string sortField, string sortOrder)
        {
            // Context.Retrieve(page, records, sortField, sortOrder).ToList<DTO.DTOSections>();7
            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public DTO.DTOSections Remove(Section obj)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public DTO.DTOSections Create(Section obj)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public DTO.DTOSections Update(Section obj)
        {
            throw new NotImplementedException();
        }

        //private static List<DTO.DTOSections> ToDTO(this List<DAL.Tables.Entities.Section> obj)
        //{
        //    List<DTO.DTOSections> result = null;

        //    if (obj == null) throw new Exception("obj");
        //    else
        //    {
        //        result = new List<DTO.DTOSections>();
        //        foreach (var row in obj)
        //        {
        //            result.Add(new DTO.DTOSections()
        //            {

        //            });
        //        }
        //    }

        //    return result;
        //}

        #region Dispose

        ~SectionsRepository()
        {
            Dispose(false);
            
        }

        public void Dispose()
        {
            Dispose(false);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
                Context.Dispose();
        }

        #endregion

    }
}
