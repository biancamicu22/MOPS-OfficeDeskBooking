using DataLibrary;
using DataLibrary.DTO;
using DataLibrary.Models;
using FakeItEasy;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MOPSAPI.Tests.Users;
using MOPSAPI;
using MOPSAPI.Controllers;
using MOPSAPI.Models;
using MOPSAPI.Repository;
using MOPSAPI.Validations;
using MOPSAPI.Validations.Models;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using MOPSAPI.Repository.Desk;
using MOPSApi.Tests.Desk;
using System.Web.Http.Results;

namespace MOPSAPI.Tests.Desks
{
    public class DeskControllerTests : IClassFixture<TestFixture<Startup>>
    {
        private DeskController Controller { get; }
        private IUserRepository _userRepository;
        private IDeskRepository _deskRepository;

        public IEntityUpdateHandler _entityUpdateHandler { get; }

        private List<Desk> Desks = new List<Desk>();
        private List<User> Users = new List<User>();

        private DeskDTO TestDesk = new DeskDTO
        {
            DeskNumber = 1,
            NumberOfMonitors = 1
        };

        public DeskControllerTests(TestFixture<Startup> fixture)
        {

            _userRepository = A.Fake<IUserRepository>();
            _entityUpdateHandler = A.Fake<IEntityUpdateHandler>();
            _deskRepository = A.Fake<IDeskRepository>();

            var deskRepository = new Mock<IDeskRepository>();


            Desks = DeskHelper.GenerateDesks(5);
            Users = UserHelpers.GenerateUsersManually(6);

            var modelDesk = TestDesk.ToModel();

            deskRepository.Setup(c => c.GetAll()).Returns(Desks);
            deskRepository.Setup(c => c.GetById(It.IsAny<string>())).Returns((int id) => Desks.FirstOrDefault(c => c.DeskNumber == id));

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

            Controller = new DeskController(deskRepository.Object);
        }


        [Fact]
        public void GetAllDesks()
        {
            //Act
            var result = (Controller.GetAll() as OkObjectResult).Value as List<Desk>; 
            // Assert
            Assert.Equal(result.Count(), Desks.Count);
        }


        [Fact]
        public void GetAllDesks_ReturnsNull()
        {
            var BadController = new DeskController(_deskRepository);
            var result = (BadController.GetAll() as OkObjectResult).Value as List<Desk>;

            Assert.Null(result);
        }

        [Fact]
        public async Task CreateDesk_ReturnsNull()
        {
            var userModel = Users[0];
            var user = new Mock<ClaimsPrincipal>();
            var claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Name, userModel.UserName));
            claims.Add(new Claim(ClaimTypes.Email, userModel.Email));
            claims.Add(new Claim("UserID", userModel.Id));
            var BadController = new DeskController(_deskRepository);

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
            var result = response as OkObjectResult;
            // Assert
            Assert.Null(result);
        }


        [Fact]
        public async Task CreateDesk()
        {
            var userModel = Users[0];
            var newDesk = new DeskDTO
            {
                DeskNumber = 1,
                NumberOfMonitors = 1
            };
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
            //Act
            var response = Controller.Create(newDesk);
            var result = response as OkObjectResult;
            // Assert
            Assert.NotNull(result);
        }
    }
}
