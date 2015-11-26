using System;

namespace Horizon.DAL.Tables.Entities
{
    public class UserLogin
    {
        public Guid Id { get; set; }

        public DateTime? LastLogin { get; set; }
    }
}
