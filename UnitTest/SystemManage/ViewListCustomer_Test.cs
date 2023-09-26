using AutoMapper;
using Back.Controllers;
using Library.DTO;
using Library.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace Capstone_UnitTest.Controller
{
    public class ViewListCustomer_Test
    {
        private readonly Mock<CoffeehouseSystemContext> _mockContext;
        private readonly Mock<IMapper> _mockMapper;

        public ViewListCustomer_Test()
        {
            _mockContext = new Mock<CoffeehouseSystemContext>();
            _mockMapper = new Mock<IMapper>();
        }

        [Fact]
        public void TC1_ViewListCustomer_Test()
        {
            Test_ViewListCustomer_HaveData(5);
        }

        [Fact]
        public void TC2_ViewListCustomer_Test()
        {
            Test_ViewListCustomer_NoData(5);
        }

        public void Test_ViewListCustomer_HaveData(int count)
        {
            var customers = new List<Customer>
            {
                new Customer { CustomerId = 1, AccountId = 1 , Account = new Account { IsBanned = false } },
                new Customer { CustomerId = 2, AccountId = 2 , Account = new Account { IsBanned = true } },
                new Customer { CustomerId = 3, AccountId = 3 , Account = new Account { IsBanned = false } },
                new Customer { CustomerId = 4, AccountId = 4 , Account = new Account { IsBanned = false } },
            };
            var mockDBCustomer = new Mock<DbSet<Customer>>();
            mockDBCustomer.As<IQueryable<Customer>>().Setup(m => m.Provider).Returns(customers.AsQueryable().Provider);
            mockDBCustomer.As<IQueryable<Customer>>().Setup(m => m.Expression).Returns(customers.AsQueryable().Expression);
            mockDBCustomer.As<IQueryable<Customer>>().Setup(m => m.ElementType).Returns(customers.AsQueryable().ElementType);
            mockDBCustomer.As<IQueryable<Customer>>().Setup(m => m.GetEnumerator()).Returns(customers.AsQueryable().GetEnumerator());
            _mockContext.SetupGet(m => m.Customers).Returns(mockDBCustomer.Object);

            var usersInfo = new List<CustomerInfo>
            {
                new CustomerInfo { CustomerId = 1, },
                new CustomerInfo { CustomerId = 3, },
                new CustomerInfo { CustomerId = 4, },
            };
            _mockMapper.Setup(m => m.Map<List<Customer>, List<CustomerInfo>>(It.IsAny<List<Customer>>())).Returns(usersInfo);

            CustomerController userController = new CustomerController(_mockContext.Object, _mockMapper.Object);
            Assert.IsType<OkObjectResult>(userController.List());
            _mockContext.Verify(c => c.Customers, Times.Exactly(1));
            _mockMapper.Verify(c => c.Map<List<Customer>, List<CustomerInfo>>(It.IsAny<List<Customer>>()), Times.Once);
        }

        public void Test_ViewListCustomer_NoData(int count)
        {
            var customers = new List<Customer>
            {
                
            };
            var mockDBCustomer = new Mock<DbSet<Customer>>();
            mockDBCustomer.As<IQueryable<Customer>>().Setup(m => m.Provider).Returns(customers.AsQueryable().Provider);
            mockDBCustomer.As<IQueryable<Customer>>().Setup(m => m.Expression).Returns(customers.AsQueryable().Expression);
            mockDBCustomer.As<IQueryable<Customer>>().Setup(m => m.ElementType).Returns(customers.AsQueryable().ElementType);
            mockDBCustomer.As<IQueryable<Customer>>().Setup(m => m.GetEnumerator()).Returns(customers.AsQueryable().GetEnumerator());
            _mockContext.SetupGet(m => m.Customers).Returns(mockDBCustomer.Object);

            var usersInfo = new List<CustomerInfo>
            {
                
            };
            _mockMapper.Setup(m => m.Map<List<Customer>, List<CustomerInfo>>(It.IsAny<List<Customer>>())).Returns(usersInfo);

            CustomerController userController = new CustomerController(_mockContext.Object, _mockMapper.Object);
            Assert.IsType<NotFoundResult>(userController.List());
            _mockContext.Verify(c => c.Customers, Times.Exactly(1));
            _mockMapper.Verify(c => c.Map<List<Customer>, List<CustomerInfo>>(It.IsAny<List<Customer>>()), Times.Once);
        }

    }
}
