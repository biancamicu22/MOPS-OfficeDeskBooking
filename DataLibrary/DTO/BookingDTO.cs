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
    public class BookingDTO
    {
        [DataMember]
        [JsonProperty("id")]
        public int Id { get; set; }

        [DataMember]
        [JsonProperty("startDate")]
        public DateTime StartDate { get; set; }

        [DataMember]
        [JsonProperty("endDate")]
        public DateTime EndDate { get; set; }
        [DataMember]
        [JsonProperty("deskNumber")]
        public int DeskNumber { get; set; }

        [DataMember]
        [JsonProperty("user_id")]
        public string User_Id { get; set; }

        public static BookingDTO FromModel(Booking model)
        {
            if (model == null) return null;
            return new BookingDTO()
            {
               Id = model.Id,
               StartDate = model.StartDate,
               EndDate = model.EndDate,
               User_Id = model.User_Id,
               DeskNumber = model.DeskNumber
            };
        }

        public Booking ToModel()
        {
            return new Booking()
            {
                Id =  Id,
                StartDate = StartDate,
                EndDate = EndDate,
                User_Id = User_Id,
                DeskNumber = DeskNumber
            };
        }
    }
}
