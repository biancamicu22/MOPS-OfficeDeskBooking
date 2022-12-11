using DataLibrary.Models;
using MOPSAPI.Repository;
using MOPSAPI.Repository.Booking;
using MOPSAPI.Repository.Desk;
using MOPSAPI.Validations.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MOPSAPI.Validations.Validators
{
    public class BookingValidator : AbstractEntityHandler<Booking>
    {
        public IServiceProvider ServiceProvider { get; }
        public BookingValidator(Booking newEntity, IBookingRepository bookingRepository, IServiceProvider serviceProvider) :
            base(newEntity, bookingRepository)
        {
            ServiceProvider = serviceProvider;
        }

        protected override void applyChanges()
        {
            dbEntity.EndDate = newEntity.EndDate;
            dbEntity.StartDate = newEntity.StartDate;
        }

        protected override List<ValidationKeyValue> validate()
        {
            var validationResult = new List<ValidationKeyValue>();

            validationResult.ValidateDate("endDate", dbEntity.EndDate, dbEntity.StartDate, dbEntity.StartDate.AddDays(7));
            validationResult.ValidateDate("startDate", dbEntity.StartDate, DateTime.Now, DateTime.Now.AddDays(30));

            return validationResult;
        }
    }
}
