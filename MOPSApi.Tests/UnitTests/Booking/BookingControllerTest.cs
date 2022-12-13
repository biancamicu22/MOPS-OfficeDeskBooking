using FakeItEasy;
using MOPSAPI.Tests.Users;
using MOPSAPI;
using MOPSAPI.Controllers;
using MOPSAPI.Repository;
using MOPSAPI.Validations;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Xunit;
using MOPSAPI.Repository.Booking;
using MOPSAPI.Tests;
using Microsoft.AspNetCore.Mvc;
using DataLibrary.Models;
using DataLibrary;
using DataLibrary.DTO;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using System;

namespace MOPSApi.Tests.Booking
{
    public class BookingControllerTest : IClassFixture<TestFixture<Startup>>
    {
        private BookingController Controller { get; }
        private IUserRepository _userRepository;
        private IBookingRepository _bookingRepository;

        public IEntityUpdateHandler _entityUpdateHandler { get; }

        private List<DataLibrary.Models.Booking> Bookings = new List<DataLibrary.Models.Booking>();
        private List<User> Users = new List<User>();

        private BookingDTO TestBooking = new BookingDTO
        {
            DeskNumber = 1,
            Id= 1,
            StartDate = System.DateTime.UtcNow,
            EndDate = System.DateTime.UtcNow,
            User_Id = "1"
        };

        public BookingControllerTest(TestFixture<Startup> fixture)
        {

            _userRepository = A.Fake<IUserRepository>();
            _entityUpdateHandler = A.Fake<IEntityUpdateHandler>();

            var bookingRepository = new Mock<IBookingRepository>();

            Bookings = BookingHelper.GenerateBookings(5);
            Users = UserHelpers.GenerateUsersManually(6);

            var modelBooking = TestBooking.ToModel();

            bookingRepository.Setup(c => c.GetAll()).Returns(Bookings);
            bookingRepository.Setup(c => c.getAllActiveUserBookings("Test")).Returns(Bookings);
            bookingRepository.Setup(c => c.GetById(It.IsAny<string>())).Returns((int id) => Bookings.FirstOrDefault(c => c.Id == id));

            var userRepository = new Mock<IUserRepository>();
            userRepository.Setup(x => x.GetAll()).Returns(Users);
            userRepository.Setup(x => x.GetById(It.IsAny<string>())).Returns((string id) => Users.FirstOrDefault(u => u.Id == id));
            userRepository.Setup(x => x.Edit(It.IsAny<User>())).Returns((User user) =>
            {
                var usrId = Users.FindIndex(u => u.Id == u.Id);
                Users[usrId] = user;
                return user;
            }
            );

            var dbContext = new Mock<MOPSContext>();
            dbContext.Setup(x => x.SaveChangesAsync(CancellationToken.None)).ReturnsAsync(1);

            Controller = new BookingController(bookingRepository.Object);
        }

        [Fact]
        public void GetAllBookings()
        {
            //Act
            var result = (Controller.GetAll() as OkObjectResult).Value as List<DataLibrary.Models.Booking>;
            // Assert
            Assert.Equal(result.Count(), Bookings.Count);
        }

        [Fact]
        public void DeleteWithIdNull()
        {
            //Act
            var result = Controller.Delete(null) as StatusCodeResult;
            // Assert
            Assert.Equal(result.StatusCode, 400);
        }

        [Fact]
        public void Delete()
        {

            var booking = Bookings.First();
            var result = Controller.Delete(booking.Id.ToString()) as OkObjectResult;
            // Assert
            Assert.Equal(result.StatusCode, 200);
        }

        [Fact]
        public async Task CreateBooking_ReturnsNull()
        {
            var userModel = Users[0];
            var user = new Mock<ClaimsPrincipal>();
            var claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Name, userModel.UserName));
            claims.Add(new Claim(ClaimTypes.Email, userModel.Email));
            claims.Add(new Claim("UserID", userModel.Id));
            var BadController = new BookingController(_bookingRepository);

            var identity = new ClaimsPrincipal(new ClaimsIdentity(claims));
            Controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext
                {
                    User = identity,
                }
            };
            //Act
            var response = BadController.Create(null);

            var result = response as StatusCodeResult;
            // Assert
            Assert.Equal(result.StatusCode, 400);
        }


        [Fact]
        public async Task CreateBooking()
        {
            var userModel = Users[0];
            var user = new Mock<ClaimsPrincipal>();
            var claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Name, userModel.UserName));
            claims.Add(new Claim(ClaimTypes.Email, userModel.Email));
            claims.Add(new Claim("UserID", userModel.Id));

            var identity = new ClaimsPrincipal(new ClaimsIdentity(claims));
            Controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext
                {
                    User = identity,
                }
            };
            var booking = new BookingDTO();
            //Act
            var response = Controller.Create(booking);

            var result = response as OkObjectResult;
            // Assert
            Assert.Equal(result.StatusCode, 200);
        }


        [Fact]
        public void GetAllActiveBookings()
        {
            //Act
            var result = (Controller.GetUserActiveBookings("Test") as OkObjectResult).Value as List<DataLibrary.Models.Booking>;
            // Assert
            Assert.Equal(result.Count(), Bookings.Count);
        }

        [Fact]
        public void GetAllActiveBooking_ReturnsNull()
        {
            var BadController = new BookingController(_bookingRepository);
            var result = BadController.GetUserActiveBookings("");

            var response = result as StatusCodeResult;
            // Assert
            Assert.Equal(response.StatusCode, 400);
        }
    }
}
