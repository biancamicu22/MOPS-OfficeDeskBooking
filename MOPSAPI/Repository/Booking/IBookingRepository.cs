using DataLibrary.DTO;
using System.Collections.Generic;

namespace MOPSAPI.Repository.Booking
{
    public interface IBookingRepository: IBaseRepository<DataLibrary.Models.Booking>
    {
        DataLibrary.Models.Booking AddBookingRandomPlace(BookingDTO randomPlace);
        List<DataLibrary.Models.Booking> getAllActiveUserBookings(string userEmail);

        bool Delete(int id);
    }
}
