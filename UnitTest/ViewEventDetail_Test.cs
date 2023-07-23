using Library.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Library.DTO;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using MailKit.Net.Smtp;
using Library.Models;
using Back.Utilities;
using Moq;
using Back.Controllers;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestPlatform.Common;
using Newtonsoft.Json;
using System.Net.Http;
using System.Configuration;
using Org.BouncyCastle.Asn1;
using System.Net.Sockets;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using AutoMapper.Internal;
using AutoMapper;
using Org.BouncyCastle.Asn1.Pkcs;

namespace Capstone_UnitTest.Controller
{
    public class ViewEventDetail_Test
    {
        private readonly Mock<CoffeehouseSystemContext> _mockContext;
        private readonly Mock<IMapper> _mockMapper;

        public ViewEventDetail_Test()
        {
            _mockContext = new Mock<CoffeehouseSystemContext>();
            _mockMapper = new Mock<IMapper>();
        }

        [Fact]
        public void TC1_ViewEventDetail_Test()
        {
            Test_ViewListEvent_HaveData(5);
        }

        [Fact]
        public void TC2_ViewEventDetail_Test()
        {
            Test_ViewListEvent_HaveData(3);
        }

        [Fact]
        public void TC3_ViewEventDetail_Test()
        {
            Test_ViewListEvent_HaveData(1);
        }

        [Fact]
        public void TC4_ViewEventDetail_Test()
        {
            Test_ViewListEvent_HaveNoData(9);
        }

        [Fact]
        public void TC5_ViewEventDetail_Test()
        {
            Test_ViewListEvent_HaveNoData(11);
        }

        [Fact]
        public void TC6_ViewEventDetail_Test()
        {
            Test_ViewListEvent_HaveNoData(6);
        }
        public void Test_ViewListEvent_HaveData(int id)
        {
            var events = new List<Event>
            {
                new Event { EventId = 1 },
                new Event { EventId = 2 },
                new Event { EventId = 3 },
                new Event { EventId = 4 },
                new Event { EventId = 5 }
            };
            var mockDBEvent = new Mock<DbSet<Event>>();
            mockDBEvent.As<IQueryable<Event>>().Setup(m => m.Provider).Returns(events.AsQueryable().Provider);
            mockDBEvent.As<IQueryable<Event>>().Setup(m => m.Expression).Returns(events.AsQueryable().Expression);
            mockDBEvent.As<IQueryable<Event>>().Setup(m => m.ElementType).Returns(events.AsQueryable().ElementType);
            mockDBEvent.As<IQueryable<Event>>().Setup(m => m.GetEnumerator()).Returns(events.AsQueryable().GetEnumerator());
            _mockContext.SetupGet(m => m.Events).Returns(mockDBEvent.Object);

            var eventInfos = new List<EventInfo>
            {
                new EventInfo { EventId = 1, Name = "Melody1" },
                new EventInfo { EventId = 2, Name = "Melody2" },
                new EventInfo { EventId = 3, Name = "Melody3" },
                new EventInfo { EventId = 4, Name = "Melody4" },
                new EventInfo { EventId = 5, Name = "Melody5" }
            };
            _mockMapper.Setup(m => m.Map<Event, EventInfo>(It.IsAny<Event>())).Returns(eventInfos.FirstOrDefault(ev => ev.EventId.Equals(id)));

            EventController eventController = new EventController(_mockContext.Object, _mockMapper.Object);

            Assert.IsType<OkObjectResult>(eventController.Detail(id));
            _mockMapper.Verify(c => c.Map<Event, EventInfo>(It.IsAny<Event>()), Times.Once);
        }

        public void Test_ViewListEvent_HaveNoData(int id)
        {
            var events = new List<Event>
            {
                new Event { EventId = 1 },
                new Event { EventId = 2 },
                new Event { EventId = 3 },
                new Event { EventId = 4 },
                new Event { EventId = 5 }
            };
            var mockDBEvent = new Mock<DbSet<Event>>();
            mockDBEvent.As<IQueryable<Event>>().Setup(m => m.Provider).Returns(events.AsQueryable().Provider);
            mockDBEvent.As<IQueryable<Event>>().Setup(m => m.Expression).Returns(events.AsQueryable().Expression);
            mockDBEvent.As<IQueryable<Event>>().Setup(m => m.ElementType).Returns(events.AsQueryable().ElementType);
            mockDBEvent.As<IQueryable<Event>>().Setup(m => m.GetEnumerator()).Returns(events.AsQueryable().GetEnumerator());
            _mockContext.SetupGet(m => m.Events).Returns(mockDBEvent.Object);

            var eventInfos = new List<EventInfo>
            {
                new EventInfo { EventId = 1, Name = "Melody1" },
                new EventInfo { EventId = 2, Name = "Melody2" },
                new EventInfo { EventId = 3, Name = "Melody3" },
                new EventInfo { EventId = 4, Name = "Melody4" },
                new EventInfo { EventId = 5, Name = "Melody5" }
            };
            _mockMapper.Setup(m => m.Map<Event, EventInfo>(It.IsAny<Event>())).Returns(eventInfos.FirstOrDefault(ev => ev.EventId.Equals(id)));

            EventController eventController = new EventController(_mockContext.Object, _mockMapper.Object);

            Assert.IsType<NotFoundResult>(eventController.Detail(id));
            _mockMapper.Verify(c => c.Map<Event, EventInfo>(It.IsAny<Event>()), Times.Never);
        }
    }
}
