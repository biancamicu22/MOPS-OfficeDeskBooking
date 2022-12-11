using System;
using System.Linq;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using DataLibrary.Models;
using System.Collections.Generic;
using System.Security.Claims;

namespace DataLibrary.DTO
{
    [DataContract]
    public class UserDTO
    {
        [DataMember]
        [JsonProperty("id")]
        public string Id { get; set; }

        [DataMember]
        [JsonProperty("active")]
        public bool Active { get; set; }

        [DataMember]
        [JsonProperty("birthdate")]
        public DateTime? Birthdate { get; set; }

        [DataMember]
        [JsonProperty("firstName")]
        public string FirstName { get; set; }

        [DataMember]
        [JsonProperty("lastName")]
        public string LastName { get; set; }

        [DataMember]
        [JsonProperty("roles")]
        public string Roles { get; set; }

        [DataMember]
        [JsonProperty("description")]
        public string Description { get; set; }

        [DataMember]
        [JsonProperty("department")]
        public string Department { get; set; }

        [DataMember]
        [JsonProperty("password")]
        public string Password { get; set; }

        [DataMember]
        [JsonProperty("email")]
        public string Email { get; set; }

        [DataMember]
        [JsonProperty("username")]
        public string UserName { get; set; }

        [DataMember]
        [JsonProperty("bookings")]
        public ICollection<BookingDTO> Bookings { get; set; }

        public UserDTO()
        {
            Bookings = new HashSet<BookingDTO>();
        }

        public static UserDTO FromModel(User model)
        {
            if (model == null) return null;
            return new UserDTO()
            {
                Id = model.Id,
                UserName = model.UserName,
                Email = model.Email,
                Active = model.Active, 
                Birthdate = model.Birthdate, 
                FirstName = model.FirstName, 
                LastName = model.LastName, 
                Roles = model.Roles, 
                Description = model.Description, 
                Bookings = model.Bookings.Select(c => { c.User = model; return BookingDTO.FromModel(c); }).ToList(), 
            }; 
        }

        public User ToModel()
        {
            return new User()
            {
                Id = Id,
                UserName = UserName,
                Email = Email,
                Active = Active, 
                Birthdate = Birthdate, 
                FirstName = FirstName, 
                LastName = LastName, 
                Roles = Roles, 
                Description = Description, 
                Bookings = Bookings.Select(c => c.ToModel()).ToList(), 
            }; 
        }

        public static implicit operator ClaimsPrincipal(UserDTO v)
        {
            throw new NotImplementedException();
        }
    }
}