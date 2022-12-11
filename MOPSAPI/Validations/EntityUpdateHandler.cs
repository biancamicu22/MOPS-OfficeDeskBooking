using DataLibrary.Models;
using MOPSAPI.Repository;
using MOPSAPI.Repository.Booking;
using MOPSAPI.Repository.Desk;
using MOPSAPI.Validations.Models;
using MOPSAPI.Validations.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MOPSAPI.Validations
{
    public class EntityUpdateHandler : IEntityUpdateHandler
    {
        public IServiceProvider ServiceProvider { get; }
        public IBookingRepository BookingRepository { get; }
        public IDeskRepository DeskRepository { get; }

        public EntityUpdateHandler(IServiceProvider serviceProvider, IBookingRepository bookingRepository, IDeskRepository deskRepository)
        {
            ServiceProvider = serviceProvider;
            BookingRepository = bookingRepository;
            DeskRepository = deskRepository;
        }

        public EntityHandlerResult<TSuccess> Update<TSuccess>(IEntity entity) where TSuccess: class
        {
            var @switch = new Dictionary<Type, Func<EntityHandlerResult<TSuccess>>> {
                { typeof(Desk), () => new DeskValidator(entity as Desk, DeskRepository, ServiceProvider).update<TSuccess>()},
                { typeof(Booking), () => new BookingValidator(entity as Booking, BookingRepository, ServiceProvider).update<TSuccess>()},
            };

            return @switch[entity.GetType()]();
        }
    }
}
