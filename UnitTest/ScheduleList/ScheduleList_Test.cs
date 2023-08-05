using AutoMapper;
using Back.Controllers;
using Library.DTO;
using Library.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace Capstone_UnitTest.Controller
{
    public class ScheduleList_Test
    {
        private readonly Mock<CoffeehouseSystemContext> _mockContext;
        private readonly Mock<IMapper> _mockMapper;

        public ScheduleList_Test()
        {
            _mockContext = new Mock<CoffeehouseSystemContext>();
            _mockMapper = new Mock<IMapper>();
        }

        [Fact]
        public void TC1_ScheduleList_Test()
        {
            Test_ScheduleList_User_HaveData(1);
        }

        [Fact]
        public void TC2_ScheduleList_Test()
        {
            Test_ScheduleList_User_HaveData(2);
        }

        [Fact]
        public void TC3_ScheduleList_Test()
        {
            Test_ScheduleList_User_HaveData(3);
        }

        [Fact]
        public void TC4_ScheduleList_Test()
        {
            Test_ScheduleList_User_NoData(1);
        }

        [Fact]
        public void TC5_ScheduleList_Test()
        {
            Test_ScheduleList_Customer_HaveData(3);
        }

        [Fact]
        public void TC6_ScheduleList_Test()
        {
            Test_ScheduleList_Customer_HaveData(4);
        }

        [Fact]
        public void TC7_ScheduleList_Test()
        {
            Test_ScheduleList_Customer_NoData(3);
        }

        public void Test_ScheduleList_User_HaveData(int id)
        {
            var users = new List<Schedule>
            {
                new Schedule { ScheduleId = 1, CustomerId = 1, Event = new Event { EventId = 1, User = new User{ UserId = 1 }}},
                new Schedule { ScheduleId = 2, CustomerId = 2, Event = new Event { EventId = 2, User = new User{ UserId = 1 }}},
                new Schedule { ScheduleId = 3, CustomerId = 3, Event = new Event { EventId = 3, User = new User{ UserId = 2 }}},
                new Schedule { ScheduleId = 4, CustomerId = 4, Event = new Event { EventId = 4, User = new User{ UserId = 3 }}},
                new Schedule { ScheduleId = 5, CustomerId = 5, Event = new Event { EventId = 5, User = new User{ UserId = 3 }}},
            };
            var mockDBUser = new Mock<DbSet<Schedule>>();
            mockDBUser.As<IQueryable<Schedule>>().Setup(m => m.Provider).Returns(users.AsQueryable().Provider);
            mockDBUser.As<IQueryable<Schedule>>().Setup(m => m.Expression).Returns(users.AsQueryable().Expression);
            mockDBUser.As<IQueryable<Schedule>>().Setup(m => m.ElementType).Returns(users.AsQueryable().ElementType);
            mockDBUser.As<IQueryable<Schedule>>().Setup(m => m.GetEnumerator()).Returns(users.AsQueryable().GetEnumerator());
            _mockContext.SetupGet(m => m.Schedules).Returns(mockDBUser.Object);

            var usersInfo = new List<ScheduleInfo>
            {
                new ScheduleInfo { ScheduleId = 1, CustomerId = 1, EventId = 1  },
                new ScheduleInfo { ScheduleId = 2, CustomerId = 2, EventId = 2, },
                new ScheduleInfo { ScheduleId = 3, CustomerId = 3, EventId = 3, },
                new ScheduleInfo { ScheduleId = 4, CustomerId = 4, EventId = 4, },
                new ScheduleInfo { ScheduleId = 5, CustomerId = 5, EventId = 5, },
            };
            _mockMapper.Setup(m => m.Map<List<Schedule>, List<ScheduleInfo>>(It.IsAny<List<Schedule>>())).Returns(usersInfo);

            ScheduleController userController = new ScheduleController(_mockContext.Object, _mockMapper.Object);
            Assert.IsType<OkObjectResult>(userController.User(id));
            _mockContext.Verify(c => c.Schedules, Times.Exactly(1));
            _mockMapper.Verify(c => c.Map<List<Schedule>, List<ScheduleInfo>>(It.IsAny<List<Schedule>>()), Times.Once);
        }

        public void Test_ScheduleList_User_NoData(int id)
        {
            var users = new List<Schedule>
            {
                
            };
            var mockDBUser = new Mock<DbSet<Schedule>>();
            mockDBUser.As<IQueryable<Schedule>>().Setup(m => m.Provider).Returns(users.AsQueryable().Provider);
            mockDBUser.As<IQueryable<Schedule>>().Setup(m => m.Expression).Returns(users.AsQueryable().Expression);
            mockDBUser.As<IQueryable<Schedule>>().Setup(m => m.ElementType).Returns(users.AsQueryable().ElementType);
            mockDBUser.As<IQueryable<Schedule>>().Setup(m => m.GetEnumerator()).Returns(users.AsQueryable().GetEnumerator());
            _mockContext.SetupGet(m => m.Schedules).Returns(mockDBUser.Object);

            var usersInfo = new List<ScheduleInfo>
            {
                
            };
            _mockMapper.Setup(m => m.Map<List<Schedule>, List<ScheduleInfo>>(It.IsAny<List<Schedule>>())).Returns(usersInfo);

            ScheduleController userController = new ScheduleController(_mockContext.Object, _mockMapper.Object);
            Assert.IsType<NotFoundResult>(userController.User(id));
            _mockContext.Verify(c => c.Schedules, Times.Exactly(1));
            _mockMapper.Verify(c => c.Map<List<Schedule>, List<ScheduleInfo>>(It.IsAny<List<Schedule>>()), Times.Once);
        }

        public void Test_ScheduleList_Customer_HaveData(int id)
        {
            var users = new List<Schedule>
            {
                new Schedule { ScheduleId = 1, CustomerId = 1, Event = new Event { EventId = 1, User = new User{ UserId = 1 }}},
                new Schedule { ScheduleId = 2, CustomerId = 1, Event = new Event { EventId = 2, User = new User{ UserId = 1 }}},
                new Schedule { ScheduleId = 3, CustomerId = 3, Event = new Event { EventId = 3, User = new User{ UserId = 2 }}},
                new Schedule { ScheduleId = 4, CustomerId = 4, Event = new Event { EventId = 4, User = new User{ UserId = 3 }}},
                new Schedule { ScheduleId = 5, CustomerId = 3, Event = new Event { EventId = 5, User = new User{ UserId = 3 }}},
            };
            var mockDBUser = new Mock<DbSet<Schedule>>();
            mockDBUser.As<IQueryable<Schedule>>().Setup(m => m.Provider).Returns(users.AsQueryable().Provider);
            mockDBUser.As<IQueryable<Schedule>>().Setup(m => m.Expression).Returns(users.AsQueryable().Expression);
            mockDBUser.As<IQueryable<Schedule>>().Setup(m => m.ElementType).Returns(users.AsQueryable().ElementType);
            mockDBUser.As<IQueryable<Schedule>>().Setup(m => m.GetEnumerator()).Returns(users.AsQueryable().GetEnumerator());
            _mockContext.SetupGet(m => m.Schedules).Returns(mockDBUser.Object);

            var usersInfo = new List<ScheduleInfo>
            {
                new ScheduleInfo { ScheduleId = 1, CustomerId = 1, EventId = 1  },
                new ScheduleInfo { ScheduleId = 2, CustomerId = 2, EventId = 2, },
                new ScheduleInfo { ScheduleId = 3, CustomerId = 3, EventId = 3, },
                new ScheduleInfo { ScheduleId = 4, CustomerId = 4, EventId = 4, },
                new ScheduleInfo { ScheduleId = 5, CustomerId = 5, EventId = 5, },
            };
            _mockMapper.Setup(m => m.Map<List<Schedule>, List<ScheduleInfo>>(It.IsAny<List<Schedule>>())).Returns(usersInfo);

            ScheduleController userController = new ScheduleController(_mockContext.Object, _mockMapper.Object);
            Assert.IsType<OkObjectResult>(userController.Customer(id));
            _mockContext.Verify(c => c.Schedules, Times.Exactly(1));
            _mockMapper.Verify(c => c.Map<List<Schedule>, List<ScheduleInfo>>(It.IsAny<List<Schedule>>()), Times.Once);
        }

        public void Test_ScheduleList_Customer_NoData(int id)
        {
            var users = new List<Schedule>
            {
                
            };
            var mockDBUser = new Mock<DbSet<Schedule>>();
            mockDBUser.As<IQueryable<Schedule>>().Setup(m => m.Provider).Returns(users.AsQueryable().Provider);
            mockDBUser.As<IQueryable<Schedule>>().Setup(m => m.Expression).Returns(users.AsQueryable().Expression);
            mockDBUser.As<IQueryable<Schedule>>().Setup(m => m.ElementType).Returns(users.AsQueryable().ElementType);
            mockDBUser.As<IQueryable<Schedule>>().Setup(m => m.GetEnumerator()).Returns(users.AsQueryable().GetEnumerator());
            _mockContext.SetupGet(m => m.Schedules).Returns(mockDBUser.Object);

            var usersInfo = new List<ScheduleInfo>
            {

            };
            _mockMapper.Setup(m => m.Map<List<Schedule>, List<ScheduleInfo>>(It.IsAny<List<Schedule>>())).Returns(usersInfo);

            ScheduleController userController = new ScheduleController(_mockContext.Object, _mockMapper.Object);
            Assert.IsType<NotFoundResult>(userController.Customer(id));
            _mockContext.Verify(c => c.Schedules, Times.Exactly(1));
            _mockMapper.Verify(c => c.Map<List<Schedule>, List<ScheduleInfo>>(It.IsAny<List<Schedule>>()), Times.Once);
        }
    }
}
