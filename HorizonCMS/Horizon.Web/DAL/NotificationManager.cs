using Horizon.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Horizon.Web.DAL
{
    public class NotificationManager : IDisposable
    {
            #region Properties

        private HorizonContext db { get; set; }

        #endregion

        #region Ctor.

        public NotificationManager()
        {
            db = new HorizonContext();
        }

        #endregion

        #region Methods

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ICollection<Notification> GetAll()
        {
            return (from notifications in db.Notification
                    orderby notifications.Priority ascending
                    select notifications).ToList();
        
        }

        public bool Notify(Notification obj)
        {
            db.Notification.Add(obj);
            db.SaveChanges();

            return true;
        }

        #endregion

        #region Dispose

        ~NotificationManager()
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