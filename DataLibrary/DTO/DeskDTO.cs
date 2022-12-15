using DataLibrary.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DataLibrary.DTO
{
    public class DeskDTO
    {

        [DataMember]
        [JsonProperty("deskNumber")]
        public int DeskNumber { get; set; }

        [DataMember]
        [JsonProperty("numberOfMonitors")]
        public int NumberOfMonitors { get; set; }
        [DataMember]
        [JsonProperty("bookingDesk")]
        public virtual ICollection<BookingDTO> Bookings { get; set; }

        public DeskDTO()
        {
            Bookings = new HashSet<BookingDTO>();
        }

        public static DeskDTO FromModel(Desk model)
        {
            if (model == null) return null;
            return new DeskDTO()
            {
                DeskNumber = model.DeskNumber,
                NumberOfMonitors = model.NumberOfMonitors,
                Bookings = model.Bookings.Select(st => BookingDTO.FromModel(st)).ToList()
            };
        }

        public Desk ToModel()
        {
            return new Desk()
            {
                DeskNumber =  DeskNumber,
                NumberOfMonitors = NumberOfMonitors,
                Bookings = Bookings.Select(st=> st.ToModel()).ToList()
            };
        }
    }
}
