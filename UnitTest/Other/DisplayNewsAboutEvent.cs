using AutoMapper;
using Back.Controllers;
using Library.DTO;
using Library.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace Capstone_UnitTest.Controller
{
    public class DisplayNewsAboutEvent
    {
        private readonly Mock<CoffeehouseSystemContext> _mockContext;
        private readonly Mock<IMapper> _mockMapper;

        public DisplayNewsAboutEvent()
        {
            _mockContext = new Mock<CoffeehouseSystemContext>();
            _mockMapper = new Mock<IMapper>();
        }

        [Fact]
        public void TC1_DisplayNewsAboutEvent()
        {
            Test_DisplayNewsAboutEvent_HaveData(5);
        }

        [Fact]
        public void TC2_DisplayNewsAboutEvent()
        {
            Test_DisplayNewsAboutEvent_HaveData(3);
        }

        [Fact]
        public void TC3_DisplayNewsAboutEvent()
        {
            Test_DisplayNewsAboutEvent_HaveData(8);
        }

        [Fact]
        public void TC4_DisplayNewsAboutEvent()
        {
            Test_DisplayNewsAboutEvent_NoData(8);
        }

        [Fact]
        public void TC5_DisplayNewsAboutEvent()
        {
            Test_DisplayNewsAboutEvent_NoData(0);
        }

        public void Test_DisplayNewsAboutEvent_HaveData(int id)
        {
            var events = new List<Event>
            {
                new Event { EventId = 1 , Date = new DateTime(2023, 03, 25) },
                new Event { EventId = 2 , Date = new DateTime(2023, 03, 23) },
                new Event { EventId = 3 , Date = new DateTime(2023, 03, 24) },
                new Event { EventId = 4 , Date = new DateTime(2023, 04, 25) },
                new Event { EventId = 5 , Date = new DateTime(2023, 04, 05) }   
            };
            var mockDBEvent = new Mock<DbSet<Event>>();
            mockDBEvent.As<IQueryable<Event>>().Setup(m => m.Provider).Returns(events.AsQueryable().Provider);
            mockDBEvent.As<IQueryable<Event>>().Setup(m => m.Expression).Returns(events.AsQueryable().Expression);
            mockDBEvent.As<IQueryable<Event>>().Setup(m => m.ElementType).Returns(events.AsQueryable().ElementType);
            mockDBEvent.As<IQueryable<Event>>().Setup(m => m.GetEnumerator()).Returns(events.AsQueryable().GetEnumerator());
            _mockContext.SetupGet(m => m.Events).Returns(mockDBEvent.Object);

            var eventInfos = new List<EventInfo>
            {
                new EventInfo { EventId = 1, Name = "Melody1", Date = new DateTime(2023, 03, 25) },
                new EventInfo { EventId = 2, Name = "Melody2", Date = new DateTime(2023, 03, 23) },
                new EventInfo { EventId = 3, Name = "Melody3", Date = new DateTime(2023, 03, 24) },
                new EventInfo { EventId = 4, Name = "Melody4", Date = new DateTime(2023, 04, 25) },
                new EventInfo { EventId = 5, Name = "Melody5", Date = new DateTime(2023, 04, 05) }
            };
            _mockMapper.Setup(m => m.Map<List<Event>, List<EventInfo>>(It.IsAny<List<Event>>())).Returns(eventInfos.OrderByDescending(e => e.Date).Take(id).ToList());

            EventController eventController = new EventController(_mockContext.Object, _mockMapper.Object);

            Assert.IsType<OkObjectResult>(eventController.Lastest(id));
            _mockMapper.Verify(c => c.Map<List<Event>, List<EventInfo>>(It.IsAny<List<Event>>()), Times.Once);
        }

        public void Test_DisplayNewsAboutEvent_NoData(int id)
        {
            var events = new List<Event>
            {
                
            };
            var mockDBEvent = new Mock<DbSet<Event>>();
            mockDBEvent.As<IQueryable<Event>>().Setup(m => m.Provider).Returns(events.AsQueryable().Provider);
            mockDBEvent.As<IQueryable<Event>>().Setup(m => m.Expression).Returns(events.AsQueryable().Expression);
            mockDBEvent.As<IQueryable<Event>>().Setup(m => m.ElementType).Returns(events.AsQueryable().ElementType);
            mockDBEvent.As<IQueryable<Event>>().Setup(m => m.GetEnumerator()).Returns(events.AsQueryable().GetEnumerator());
            _mockContext.SetupGet(m => m.Events).Returns(mockDBEvent.Object);

            var eventInfos = new List<EventInfo>
            {
                
            };
            _mockMapper.Setup(m => m.Map<List<Event>, List<EventInfo>>(It.IsAny<List<Event>>())).Returns(eventInfos.OrderByDescending(e => e.Date).Take(id).ToList());

            EventController eventController = new EventController(_mockContext.Object, _mockMapper.Object);

            Assert.IsType<NotFoundResult>(eventController.Lastest(id));
            _mockMapper.Verify(c => c.Map<List<Event>, List<EventInfo>>(It.IsAny<List<Event>>()), Times.Never);
        }
    }
}
