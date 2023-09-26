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
            Test_ViewListUser_HaveData();
        }

        [Fact]
        public void TC2_ViewListUser_Test()
        {
            Test_ViewListUser_NoData();
        }

        public void Test_ViewListUser_HaveData()
        {
            var users = new List<User>
            {
                new User {  UserId = 1, AccountId = 1 , Account = new Account { IsBanned = false } },
                new User {  UserId = 2, AccountId = 2 , Account = new Account { IsBanned = true } },
                new User {  UserId = 3, AccountId = 3 , Account = new Account { IsBanned = false } },
                new User {  UserId = 4, AccountId = 4 , Account = new Account { IsBanned = false } },
            };
            var mockDBUser = new Mock<DbSet<User>>();
            mockDBUser.As<IQueryable<User>>().Setup(m => m.Provider).Returns(users.AsQueryable().Provider);
            mockDBUser.As<IQueryable<User>>().Setup(m => m.Expression).Returns(users.AsQueryable().Expression);
            mockDBUser.As<IQueryable<User>>().Setup(m => m.ElementType).Returns(users.AsQueryable().ElementType);
            mockDBUser.As<IQueryable<User>>().Setup(m => m.GetEnumerator()).Returns(users.AsQueryable().GetEnumerator());
            _mockContext.SetupGet(m => m.Users).Returns(mockDBUser.Object);

            var usersInfo = new List<UserInfo>
            {
                new UserInfo {  UserId = 1, },
                new UserInfo {  UserId = 3, },
                new UserInfo {  UserId = 4, },
            };
            _mockMapper.Setup(m => m.Map<List<User>, List<UserInfo>>(It.IsAny<List<User>>())).Returns(usersInfo);

            UserController userController = new UserController(_mockContext.Object, _mockMapper.Object);
            Assert.IsType<OkObjectResult>(userController.List());
            _mockContext.Verify(c => c.Users, Times.Exactly(1));
            _mockMapper.Verify(c => c.Map<List<User>, List<UserInfo>>(It.IsAny<List<User>>()), Times.Once);
        }

        public void Test_ViewListUser_NoData()
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
            Assert.IsType<NotFoundResult>(userController.List());
            _mockContext.Verify(c => c.Users, Times.Exactly(1));
            _mockMapper.Verify(c => c.Map<List<User>, List<UserInfo>>(It.IsAny<List<User>>()), Times.Once);
        }


    }
}
