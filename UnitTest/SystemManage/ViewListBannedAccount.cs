using AutoMapper;
using Back.Controllers;
using Library.DTO;
using Library.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace Capstone_UnitTest.Controller
{
    public class ViewListBannedAccount
    {
        private readonly Mock<CoffeehouseSystemContext> _mockContext;
        private readonly Mock<IMapper> _mockMapper;

        public ViewListBannedAccount()
        {
            _mockContext = new Mock<CoffeehouseSystemContext>();
            _mockMapper = new Mock<IMapper>();
        }

        [Fact]
        public void TC1_ViewListBannedAccount()
        {
            Test_ViewListBannedAccount_HaveData(2);
        }

        [Fact]
        public void TC2_ViewListBannedAccount()
        {
            Test_ViewListBannedAccount_HaveData(3);
        }

        [Fact]
        public void TC3_ViewListBannedAccount()
        {
            Test_ViewListBannedAccount_HaveData(0);
        }

        [Fact]
        public void TC4_ViewListBannedAccount()
        {
            Test_ViewListBannedAccount_HaveData(9);
        }

        [Fact]
        public void TC5_ViewListBannedAccount()
        {
            Test_ViewListBannedAccount_NoData(3);
        }

        public void Test_ViewListBannedAccount_HaveData(int count)
        {
            var users = new List<User>
            {
                new User {  UserId = 1, Account = new Account { AccountId = 1, IsBanned = true } , CoffeeShopName = "House1" },
                new User {  UserId = 2, Account = new Account { AccountId = 2, IsBanned = false } , CoffeeShopName = "House2" },
                new User {  UserId = 3, Account = new Account { AccountId = 3, IsBanned = false } , CoffeeShopName = "House3" },
                new User {  UserId = 4, Account = new Account { AccountId = 4, IsBanned = true } , CoffeeShopName = "House4" },
                new User {  UserId = 5, Account = new Account { AccountId = 5, IsBanned = true } , CoffeeShopName = "House5" },
                new User {  UserId = 6, Account = new Account { AccountId = 6, IsBanned = true } , CoffeeShopName = "House6" },
            };
            var mockDBUser = new Mock<DbSet<User>>();
            mockDBUser.As<IQueryable<User>>().Setup(m => m.Provider).Returns(users.AsQueryable().Provider);
            mockDBUser.As<IQueryable<User>>().Setup(m => m.Expression).Returns(users.AsQueryable().Expression);
            mockDBUser.As<IQueryable<User>>().Setup(m => m.ElementType).Returns(users.AsQueryable().ElementType);
            mockDBUser.As<IQueryable<User>>().Setup(m => m.GetEnumerator()).Returns(users.AsQueryable().GetEnumerator());
            _mockContext.SetupGet(m => m.Users).Returns(mockDBUser.Object);

            var usersInfo = new List<UserInfo>
            {
                new UserInfo {  UserId = 1,  CoffeeShopName = "House1" },
                new UserInfo {  UserId = 4,  CoffeeShopName = "House4" },
                new UserInfo {  UserId = 5,  CoffeeShopName = "House5" },
                new UserInfo {  UserId = 6,  CoffeeShopName = "House6" },
            };
            _mockMapper.Setup(m => m.Map<List<User>, List<UserInfo>>(It.IsAny<List<User>>())).Returns(usersInfo);

            UserController userController = new UserController(_mockContext.Object, _mockMapper.Object);
            Assert.IsType<OkObjectResult>(userController.Banned());

            _mockContext.Verify(c => c.Users, Times.Exactly(1));
            _mockMapper.Verify(c => c.Map<List<User>, List<UserInfo>>(It.IsAny<List<User>>()), Times.Once);

        }


        public void Test_ViewListBannedAccount_NoData(int count)
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
            Assert.IsType<NotFoundResult>(userController.Banned());

            _mockContext.Verify(c => c.Users, Times.Exactly(1));
            _mockMapper.Verify(c => c.Map<List<User>, List<UserInfo>>(It.IsAny<List<User>>()), Times.Once);
        }
    }
}
