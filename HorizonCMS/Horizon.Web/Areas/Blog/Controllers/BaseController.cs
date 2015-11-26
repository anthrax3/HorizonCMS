using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Horizon.Web.Areas.Blog.Controllers
{
    public class BaseController : Controller
    {
        #region Properties

        protected Horizon.Web.DAL.HorizonContext db { get; set; }

        #endregion

        public BaseController()
        {
            db = new DAL.HorizonContext();
            
        }
	}
}