using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLibrary.Models
{
    public class Booking : IEntity
    {
        public int Id { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public virtual Desk Desk { get; set; }
        public int DeskNumber { get; set; }
        public string User_Id { get; set; }
        public virtual User User { get; set; }
    }
}
