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
    public class ViewListEvent_Test
    {
        private readonly Mock<CoffeehouseSystemContext> _mockContext;
        private readonly Mock<IMapper> _mockMapper;

        public ViewListEvent_Test()
        {
            _mockContext = new Mock<CoffeehouseSystemContext>();
            _mockMapper = new Mock<IMapper>();
        }

        [Fact]
        public void TC1_ViewListEvent_Test()
        {
            Test_ViewListEvent_HaveData(5);
        }

        [Fact]
        public void TC2_ViewListEvent_Test()
        {
            Test_ViewListEvent_HaveData(3);
        }

        [Fact]
        public void TC3_ViewListEvent_Test()
        {
            Test_ViewListEvent_NoData(4);
        }

        [Fact]
        public void TC4_ViewListEvent_Test()
        {
            Test_ViewListEvent_NoData(2);
        }

        public void Test_ViewListEvent_HaveData(int count)
        {
            var events = new List<Event>
            {
                new Event { EventId = 1, Name = "Melody1" },
                new Event { EventId = 2, Name = "Melody2" },
                new Event { EventId = 3, Name = "Melody3" },
                new Event { EventId = 4, Name = "Melody4" },
                new Event { EventId = 5, Name = "Melody5" }
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
            _mockMapper.Setup(m => m.Map<List<Event>, List<EventInfo>>(It.IsAny<List<Event>>())).Returns(eventInfos);

            EventController eventController = new EventController(_mockContext.Object, _mockMapper.Object);
            Assert.IsType<OkObjectResult>(eventController.List(count));
        }

        public void Test_ViewListEvent_NoData(int count)
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
            _mockMapper.Setup(m => m.Map<List<Event>, List<EventInfo>>(It.IsAny<List<Event>>())).Returns(eventInfos);

            EventController eventController = new EventController(_mockContext.Object, _mockMapper.Object);
            Assert.IsType<NotFoundResult>(eventController.List(count));
        }


    }
}
