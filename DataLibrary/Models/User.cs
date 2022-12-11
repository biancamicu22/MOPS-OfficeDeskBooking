using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace DataLibrary.Models
{

    public partial class User : IdentityUser, IEntity
    {
        public bool Active { get; set; }
        public DateTime? Birthdate { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Roles { get; set; }
        public string Description { get; set; }

        public string Department { get; set; }  

        public virtual ICollection<Booking> Bookings { get; set; }

        public User()
        {
            Bookings = new HashSet<Booking>();
        }
    }
}
