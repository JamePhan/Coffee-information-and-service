using AutoMapper;
using Back.Controllers;
using Library.DTO;
using Library.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace Capstone_UnitTest.Controller
{
    public class SearchInformation_Test
    {
        private readonly Mock<CoffeehouseSystemContext> _mockContext;
        private readonly Mock<IMapper> _mockMapper;

        public SearchInformation_Test()
        {
            _mockContext = new Mock<CoffeehouseSystemContext>();
            _mockMapper = new Mock<IMapper>();
        }

        [Fact]
        public void TC1_SearchInformation_Test()
        {
            Test_SearchInformation_HaveData("House3", "Linh2");
        }

        [Fact]
        public void TC2_SearchInformation_Test()
        {
            Test_SearchInformation_HaveData("House2", "Linh2");
        }

        [Fact]
        public void TC3_SearchInformation_Test()
        {
            Test_SearchInformation_HaveData("House1", "Linh4");
        }

        [Fact]
        public void TC4_SearchInformation_Test()
        {
            Test_SearchInformation_HaveData("House9", "Linh9");
        }

        [Fact]
        public void TC5_SearchInformation_Test()
        {
            Test_SearchInformation_NoData("House9", "Linh9");
        }

        [Fact]
        public void TC6_SearchInformation_Test()
        {
            Test_SearchInformation_NoData("","");
        }

        public void Test_SearchInformation_HaveData(string nameUser, string nameCus)
        {
            var users = new List<User>
            {
                new User {  UserId = 1, AccountId = 1, CoffeeShopName = "House1", Account = new Account { IsBanned = false }  },
                new User {  UserId = 2, AccountId = 2, CoffeeShopName = "House2", Account = new Account { IsBanned = true }  },
                new User {  UserId = 3, AccountId = 3, CoffeeShopName = "House3", Account = new Account { IsBanned = false }  },
                new User {  UserId = 4, AccountId = 4, CoffeeShopName = "House4", Account = new Account { IsBanned = false }  },
            };
            var mockDBUser = new Mock<DbSet<User>>();
            mockDBUser.As<IQueryable<User>>().Setup(m => m.Provider).Returns(users.AsQueryable().Provider);
            mockDBUser.As<IQueryable<User>>().Setup(m => m.Expression).Returns(users.AsQueryable().Expression);
            mockDBUser.As<IQueryable<User>>().Setup(m => m.ElementType).Returns(users.AsQueryable().ElementType);
            mockDBUser.As<IQueryable<User>>().Setup(m => m.GetEnumerator()).Returns(users.AsQueryable().GetEnumerator());
            _mockContext.SetupGet(m => m.Users).Returns(mockDBUser.Object);

            var usersInfo = new List<UserInfo>
            {
                new UserInfo {  UserId = 1,  CoffeeShopName = "House1"},
                new UserInfo {  UserId = 3,  CoffeeShopName = "House3"},
                new UserInfo {  UserId = 4, CoffeeShopName = "House4"},
            };
            _mockMapper.Setup(m => m.Map<List<User>, List<UserInfo>>(It.IsAny<List<User>>())).Returns(usersInfo);



            var cuss = new List<Customer>
            {
                new Customer { CustomerId = 1, AccountId = 1, Name = "Linh1", Account = new Account { IsBanned = false }  },
                new Customer { CustomerId = 2, AccountId = 2, Name = "Linh2", Account = new Account { IsBanned = true }  },
                new Customer { CustomerId = 3, AccountId = 3, Name = "Linh3", Account = new Account { IsBanned = false }  },
                new Customer { CustomerId = 4, AccountId = 4, Name = "Linh4", Account = new Account { IsBanned = false }  },
            };
            var mockDBCus = new Mock<DbSet<Customer>>();
            mockDBCus.As<IQueryable<Customer>>().Setup(m => m.Provider).Returns(cuss.AsQueryable().Provider);
            mockDBCus.As<IQueryable<Customer>>().Setup(m => m.Expression).Returns(cuss.AsQueryable().Expression);
            mockDBCus.As<IQueryable<Customer>>().Setup(m => m.ElementType).Returns(cuss.AsQueryable().ElementType);
            mockDBCus.As<IQueryable<Customer>>().Setup(m => m.GetEnumerator()).Returns(cuss.AsQueryable().GetEnumerator());
            _mockContext.SetupGet(m => m.Customers).Returns(mockDBCus.Object);

            var cussInfo = new List<CustomerInfo>
            {
                new CustomerInfo { CustomerId = 1,  Name = "Linh1"  },
                new CustomerInfo { CustomerId = 3, Name = "Linh3"  },
                new CustomerInfo { CustomerId = 4,  Name = "Linh4"  },
            };
            _mockMapper.Setup(m => m.Map<List<Customer>, List<CustomerInfo>>(It.IsAny<List<Customer>>())).Returns(cussInfo);

            UserController userController = new UserController(_mockContext.Object, _mockMapper.Object);
            Assert.IsType<OkObjectResult>(userController.Search(nameUser));
            _mockContext.Verify(c => c.Users, Times.Exactly(1));
            _mockMapper.Verify(c => c.Map<List<User>, List<UserInfo>>(It.IsAny<List<User>>()), Times.Once);


            CustomerController cssController = new CustomerController(_mockContext.Object, _mockMapper.Object);
            Assert.IsType<OkObjectResult>(cssController.Search(nameCus));
            _mockContext.Verify(c => c.Customers, Times.Exactly(1));
            _mockMapper.Verify(c => c.Map<List<Customer>, List<CustomerInfo>>(It.IsAny<List<Customer>>()), Times.Once);
        }

        public void Test_SearchInformation_NoData(string nameUser, string nameCus)
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



            var cuss = new List<Customer>
            {
                
            };
            var mockDBCus = new Mock<DbSet<Customer>>();
            mockDBCus.As<IQueryable<Customer>>().Setup(m => m.Provider).Returns(cuss.AsQueryable().Provider);
            mockDBCus.As<IQueryable<Customer>>().Setup(m => m.Expression).Returns(cuss.AsQueryable().Expression);
            mockDBCus.As<IQueryable<Customer>>().Setup(m => m.ElementType).Returns(cuss.AsQueryable().ElementType);
            mockDBCus.As<IQueryable<Customer>>().Setup(m => m.GetEnumerator()).Returns(cuss.AsQueryable().GetEnumerator());
            _mockContext.SetupGet(m => m.Customers).Returns(mockDBCus.Object);

            var cussInfo = new List<CustomerInfo>
            {
                
            };
            _mockMapper.Setup(m => m.Map<List<Customer>, List<CustomerInfo>>(It.IsAny<List<Customer>>())).Returns(cussInfo);

            UserController userController = new UserController(_mockContext.Object, _mockMapper.Object);
            Assert.IsType<NotFoundResult>(userController.Search(nameUser));
            _mockContext.Verify(c => c.Users, Times.Exactly(1));
            _mockMapper.Verify(c => c.Map<List<User>, List<UserInfo>>(It.IsAny<List<User>>()), Times.Once);


            CustomerController cssController = new CustomerController(_mockContext.Object, _mockMapper.Object);
            Assert.IsType<NotFoundResult>(cssController.Search(nameCus));
            _mockContext.Verify(c => c.Customers, Times.Exactly(1));
            _mockMapper.Verify(c => c.Map<List<Customer>, List<CustomerInfo>>(It.IsAny<List<Customer>>()), Times.Once);
        }
    }
}
