using System;

namespace Horizon.Web.Models
{
    public class UserLogin
    {
        public Guid Id { get; set; }

        public DateTime? LastLogin { get; set; }
    }
}
