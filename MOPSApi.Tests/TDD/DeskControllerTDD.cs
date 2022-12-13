using DataLibrary.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MOPSAPI.Controllers;
using MOPSAPI.Repository.Desk;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOPSApi.Tests.TDD
{
    [TestClass]
    public class DeskControllerTDD
    {
        [TestMethod]
        public void CreateDesK()
        {
            var desk = new DeskDTO()
            {
                DeskNumber = 1,
                NumberOfMonitors = 1
            };

            var deskRepository = new Mock<IDeskRepository>();

            var controller = new DeskController(deskRepository.Object);

            var result = controller.Create(desk) as OkObjectResult;

            Assert.AreEqual(result.StatusCode, 200);
        }

        [TestMethod]
        public void CreateWrongDesK()
        {
            DeskDTO desk = null;
            var deskRepository = new Mock<IDeskRepository>();

            var controller = new DeskController(deskRepository.Object);

            var result = controller.Create(desk) as StatusCodeResult;

            Assert.AreEqual(result.StatusCode, 400);
        }

        [TestMethod]
        public void GetAllTest()
        {

            var deskRepository = new Mock<IDeskRepository>();

            var controller = new DeskController(deskRepository.Object);

            var result = controller.GetAll() as OkObjectResult;

            Assert.AreEqual(result.StatusCode, 200);
        }
    }
}
