using DataLibrary.DTO;

namespace MOPSAPI.Repository.Booking
{
    public interface IBookingRepository: IBaseRepository<DataLibrary.Models.Booking>
    {
        DataLibrary.Models.Booking AddBookingRandomPlace(BookingDTO randomPlace);
    }
}
