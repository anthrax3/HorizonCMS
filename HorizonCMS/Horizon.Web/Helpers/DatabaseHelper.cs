using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Horizon.Web.Helpers
{
    public static class DatabaseHelper
    {
        public static bool IsDown(System.Data.Entity.DbContext context)
        {
            return !context.Database.Exists();
        }
    }
}