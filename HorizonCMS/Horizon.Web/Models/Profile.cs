using System;

namespace Horizon.Web.Models
{
    public class Profile
    {
        public Guid Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Country { get; set; }

        public string State { get; set; }

        public string Town { get; set; }

        public string Address { get; set; }

        public string PostalCode { get; set; }

        public string Image { get; set; }
    }
}
