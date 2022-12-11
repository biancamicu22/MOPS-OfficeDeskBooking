using System;

namespace MOPSAPI.Models
{
    public class AuthResponse
    {
        public string jwt { get; set; }
        public string email { get; set; }
        public string username { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public DateTime? birthDate { get; set; }
        public string phoneNumber { get; set; }
        public string department { get; set; }
    }
}
