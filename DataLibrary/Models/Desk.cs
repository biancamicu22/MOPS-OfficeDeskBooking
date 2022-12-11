using System.Collections.Generic;

namespace DataLibrary.Models
{
    public class Desk : IEntity
    {
        public int DeskNumber { get; set; }
        public int NumberOfMonitors { get; set; }
        public virtual ICollection<Booking> Bookings { get; set; }

        public Desk()
        {
            Bookings = new HashSet<Booking>();
        }
    }
}
