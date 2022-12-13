using DataLibrary.DTO;
using FakeItEasy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MOPSAPI.Controllers;
using MOPSAPI.Repository.Booking;
using MOPSAPI.Validations;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Results;

namespace MOPSApi.Tests.Booking
{
    [TestClass]
    public class BookingControllerTDD
    {
        [TestMethod]
        public void AddTest()
        {
            var booking = new BookingDTO()
            {
                StartDate = DateTime.Now,
                EndDate = DateTime.Now,
                Id = 1,
                User_Id = "1",
                DeskNumber = 1
            };

            var bookingRepository = new Mock<IBookingRepository>();

             var controller = new BookingController(bookingRepository.Object);

            var result = controller.Create(booking) as OkObjectResult;

            Assert.AreEqual(result.StatusCode, 200);
        }


        [TestMethod]
        public void AddWrongTest()
        {
            BookingDTO booking = null;
            var bookingRepository = new Mock<IBookingRepository>();

            var controller = new BookingController(bookingRepository.Object);

            var result = controller.Create(null) as Microsoft.AspNetCore.Mvc.StatusCodeResult;

            Assert.AreEqual(result.StatusCode, 400);
        }

        [TestMethod]
        public void DeleteTest()
        {

            var bookingRepository = new Mock<IBookingRepository>();

            var controller = new BookingController(bookingRepository.Object);

            var result = controller.Delete("1") as OkObjectResult;

            Assert.AreEqual(result.StatusCode, 200);
        }


        [TestMethod]
        public void DeleteWrongTest()
        {
            var bookingRepository = new Mock<IBookingRepository>();

            var controller = new BookingController(bookingRepository.Object);

            var result = controller.Delete(" ") as Microsoft.AspNetCore.Mvc.StatusCodeResult;

            Assert.AreEqual(result.StatusCode, 400);
        }

        [TestMethod]
        public void DeleteWrongTestNull()
        {
            var bookingRepository = new Mock<IBookingRepository>();

            var controller = new BookingController(bookingRepository.Object);

            var result = controller.Delete(null) as Microsoft.AspNetCore.Mvc.StatusCodeResult;

            Assert.AreEqual(result.StatusCode, 400);
        }

        [TestMethod]
        public void DeleteWrongIdTest()
        {
            var bookingRepository = new Mock<IBookingRepository>();

            var controller = new BookingController(bookingRepository.Object);

            var result = controller.Delete("A12") as Microsoft.AspNetCore.Mvc.StatusCodeResult;

            Assert.AreEqual(result.StatusCode, 400);
        }


        [TestMethod]
        public void GetAllTest()
        {

            var bookingRepository = new Mock<IBookingRepository>();

            var controller = new BookingController(bookingRepository.Object);

            var result = controller.GetAll() as OkObjectResult;

            Assert.AreEqual(result.StatusCode, 200);
        }


        [TestMethod]
        public void GetUserActiveBookings()
        {
            var bookingRepository = new Mock<IBookingRepository>();

            var controller = new BookingController(bookingRepository.Object);

            var result = controller.GetUserActiveBookings("Test") as Microsoft.AspNetCore.Mvc.OkObjectResult;

            Assert.AreEqual(result.StatusCode, 200);
        }


        [TestMethod]
        public void GetUserActiveWrongBookings()
        {
            var bookingRepository = new Mock<IBookingRepository>();
            var _entityUpdateHandler = A.Fake<IEntityUpdateHandler>();

            var controller = new BookingController(bookingRepository.Object);

            var result = controller.GetUserActiveBookings("") as Microsoft.AspNetCore.Mvc.StatusCodeResult;

            Assert.AreEqual(result.StatusCode, 400);
        }
    }
}
