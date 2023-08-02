using AutoMapper;
using Back.Controllers;
using Library.DTO;
using Library.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace Capstone_UnitTest.Controller
{
    public class ViewListUser_Test
    {
        private readonly Mock<CoffeehouseSystemContext> _mockContext;
        private readonly Mock<IMapper> _mockMapper;

        public ViewListUser_Test()
        {
            _mockContext = new Mock<CoffeehouseSystemContext>();
            _mockMapper = new Mock<IMapper>();
        }

        [Fact]
        public void TC1_ViewListUser_Test()
        {
            Test_ViewListUser_HaveData(5);
        }

        [Fact]
        public void TC2_ViewListUser_Test()
        {
            Test_ViewListUser_HaveData(3);
        }

        [Fact]
        public void TC3_ViewListUser_Test()
        {
            Test_ViewListUser_HaveData(9);
        }

        [Fact]
        public void TC4_ViewListUser_Test()
        {
            Test_ViewListUser_NoData(5);
        }

        [Fact]
        public void TC5_ViewListUser_Test()
        {
            Test_ViewListUser_NoData(8);
        }

        [Fact]
        public void TC6_ViewListUser_Test()
        {
            Test_ViewListUser_NoData(2);
        }
        public void Test_ViewListUser_HaveData(int count)
        {
            var users = new List<User>
            {
                new User {  UserId = 1, AccountId = 1 },
                new User {  UserId = 2, AccountId = 1 },
                new User {  UserId = 3, AccountId = 1 },
                new User {  UserId = 4, AccountId = 1 },
            };
            var mockDBUser = new Mock<DbSet<User>>();
            mockDBUser.As<IQueryable<User>>().Setup(m => m.Provider).Returns(users.AsQueryable().Provider);
            mockDBUser.As<IQueryable<User>>().Setup(m => m.Expression).Returns(users.AsQueryable().Expression);
            mockDBUser.As<IQueryable<User>>().Setup(m => m.ElementType).Returns(users.AsQueryable().ElementType);
            mockDBUser.As<IQueryable<User>>().Setup(m => m.GetEnumerator()).Returns(users.AsQueryable().GetEnumerator());
            _mockContext.SetupGet(m => m.Users).Returns(mockDBUser.Object);

            var usersInfo = new List<UserInfo>
            {
                new UserInfo {  UserId = 1, AccountId = 1 },
                new UserInfo {  UserId = 2, AccountId = 1 },
                new UserInfo {  UserId = 3, AccountId = 1 },
                new UserInfo {  UserId = 4, AccountId = 1 },
            };
            _mockMapper.Setup(m => m.Map<List<User>, List<UserInfo>>(It.IsAny<List<User>>())).Returns(usersInfo);



            UserController userController = new UserController(_mockContext.Object, _mockMapper.Object);
            Assert.IsType<OkObjectResult>(userController.List(count));
            _mockContext.Verify(c => c.Users, Times.Exactly(1));
            _mockMapper.Verify(c => c.Map<List<User>, List<UserInfo>>(It.IsAny<List<User>>()), Times.Once);
        }

        public void Test_ViewListUser_NoData(int count)
        {
            var users = new List<User>
            {
                
            };
            var mockDBUser = new Mock<DbSet<User>>();
            mockDBUser.As<IQueryable<User>>().Setup(m => m.Provider).Returns(users.AsQueryable().Provider);
            mockDBUser.As<IQueryable<User>>().Setup(m => m.Expression).Returns(users.AsQueryable().Expression);
            mockDBUser.As<IQueryable<User>>().Setup(m => m.ElementType).Returns(users.AsQueryable().ElementType);
            mockDBUser.As<IQueryable<User>>().Setup(m => m.GetEnumerator()).Returns(users.AsQueryable().GetEnumerator());
            _mockContext.SetupGet(m => m.Users).Returns(mockDBUser.Object);

            var usersInfo = new List<UserInfo>
            {
                
            };
            _mockMapper.Setup(m => m.Map<List<User>, List<UserInfo>>(It.IsAny<List<User>>())).Returns(usersInfo);

            UserController userController = new UserController(_mockContext.Object, _mockMapper.Object);
            Assert.IsType<NotFoundResult>(userController.List(count));
            _mockContext.Verify(c => c.Users, Times.Exactly(1));
            _mockMapper.Verify(c => c.Map<List<User>, List<UserInfo>>(It.IsAny<List<User>>()), Times.Once);
        }


    }
}
