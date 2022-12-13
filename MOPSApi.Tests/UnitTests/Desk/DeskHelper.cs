using AutoFixture;
using AutoFixture.Kernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOPSApi.Tests.Desk
{
    public static class DeskHelper
    {
        public static List<DataLibrary.Models.Desk> GenerateDesks(int count)
        {
            var desks = new List<DataLibrary.Models.Desk>();
            var fixture = GetFixtureForDesks();
            for (int i = 0; i < count; i++)
            {
                desks.Add(fixture.Create<DataLibrary.Models.Desk>());
            }
            return desks;
        }

        private static Fixture GetFixtureForDesks()
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
