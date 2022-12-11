using DataLibrary;
using DataLibrary.DTO;
using DataLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOPSAPI.Repository.Booking
{
    public class BookingRepository : BaseRepository<DataLibrary.Models.Booking>, IBookingRepository
    {
        public BookingRepository(MOPSContext context) : base(context)
        {
        }

        public DataLibrary.Models.Booking AddBookingRandomPlace(BookingDTO randomPlace)
        {
            var users = _context.Set<User>().AsEnumerable();
            var userId = users.FirstOrDefault(x => x.Email == randomPlace.User_Id);

            var desks = _context.Set<DataLibrary.Models.Desk>().Where(x => x.NumberOfMonitors == randomPlace.DeskNumber).AsEnumerable();
            var bookings = _context.Set<DataLibrary.Models.Booking>().AsEnumerable();
            var bookingsCurrent = bookings.Where(x => (x.StartDate.Day == randomPlace.StartDate.Day && x.StartDate.Month == randomPlace.StartDate.Month && x.StartDate.Year == randomPlace.StartDate.Year) || 
                (x.EndDate.Day >= randomPlace.EndDate.Day && x.EndDate.Month >= randomPlace.EndDate.Month && x.EndDate.Year >= randomPlace.EndDate.Year));
            var excludedDesks = new HashSet<int>(bookingsCurrent.Select(x => x.DeskNumber));
            var availableDesks = desks.Where(x => !excludedDesks.Contains(x.DeskNumber)).ToList();

            if (availableDesks.Count() == 0) {
                return null;
            } else
            {
                var random = new Random();
                var randomIndex = random.Next(availableDesks.Count());

                randomPlace.DeskNumber = availableDesks[randomIndex].DeskNumber;
                randomPlace.User_Id = userId.Id;

                _context.Set<DataLibrary.Models.Booking>().Add(randomPlace.ToModel());
                _context.SaveChanges();

                return randomPlace.ToModel();
            }
        }
    }
}
