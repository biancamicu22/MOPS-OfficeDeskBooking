using AutoFixture;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOPSApi.Tests.Booking
{
    public class BookingHelper
    {
        public static List<DataLibrary.Models.Booking> GenerateBookings(int count)
        {
            var desks = new List<DataLibrary.Models.Booking>();
            var fixture = GetFixtureForBookings();
            for (int i = 0; i < count; i++)
            {
                desks.Add(fixture.Create<DataLibrary.Models.Booking>());
            }
            return desks;
        }

        private static Fixture GetFixtureForBookings()
        {
            var fixture = new Fixture();
            fixture
                .Behaviors
                .OfType<ThrowingRecursionBehavior>()
                .ToList()
                .ForEach(b => fixture.Behaviors.Remove(b));

            fixture.Behaviors.Add(new OmitOnRecursionBehavior());
            return fixture;
        }
    }
}
