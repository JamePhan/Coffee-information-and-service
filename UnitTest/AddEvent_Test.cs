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

namespace Capstone_UnitTest.Controller
{
    public class AddEvent_Test
    {
        private readonly Mock<CoffeehouseSystemContext> _mockContext;
        private readonly Mock<IMapper> _mockMapper;

        public AddEvent_Test()
        {
            _mockContext = new Mock<CoffeehouseSystemContext>();
            _mockMapper = new Mock<IMapper>();
        }

        [Fact]
        public void TC1_AddEvent_Test()
        {
            EventInfo eventInfo = new EventInfo();
            eventInfo.EventId = 1;
            eventInfo.Name = "Melody Event";
            eventInfo.ImageUrl = "hhja.img";
            eventInfo.Address = "hhja.img";
            Test_AddEvent(eventInfo);
        }

        [Fact]
        public void TC2_AddEvent_Test()
        {
            EventInfo eventInfo = new EventInfo();
            eventInfo.EventId = 2;
            eventInfo.Name = "Melody Event 2";
            Test_AddEvent(eventInfo);
        }

        [Fact]
        public void TC3_AddEvent_Test()
        {
            EventInfo eventInfo = new EventInfo();
            eventInfo.EventId = 3;
            eventInfo.Name = "Melody Event 3";
            Test_AddEvent(eventInfo);
        }

        [Fact]
        public void TC4_AddEvent_Test()
        {
            EventInfo eventInfo = new EventInfo();
            eventInfo = null;
            Test_AddEvent(eventInfo);
        }

        public void Test_AddEvent(EventInfo eventInfo)
        {
            var events = new List<Event>();

            var mockDBEvent = new Mock<DbSet<Event>>();
            mockDBEvent.As<IQueryable<Event>>().Setup(m => m.Provider).Returns(events.AsQueryable().Provider);
            mockDBEvent.As<IQueryable<Event>>().Setup(m => m.Expression).Returns(events.AsQueryable().Expression);
            mockDBEvent.As<IQueryable<Event>>().Setup(m => m.ElementType).Returns(events.AsQueryable().ElementType);
            mockDBEvent.As<IQueryable<Event>>().Setup(m => m.GetEnumerator()).Returns(events.AsQueryable().GetEnumerator());
            _mockContext.Setup(m => m.Events).Returns(mockDBEvent.Object);

            Event eventNew = new Event();
            if (eventInfo == null)
            {
                eventNew = null;
                _mockMapper.Setup(m => m.Map<EventInfo, Event>(It.IsAny<EventInfo>())).Returns(eventNew);
            }
            else
            {
                eventNew.EventId = eventInfo.EventId;
                eventNew.Name = eventInfo.Name;
                _mockMapper.Setup(m => m.Map<EventInfo, Event>(It.IsAny<EventInfo>())).Returns(eventNew);
            }
            EventController eventController = new EventController(_mockContext.Object, _mockMapper.Object);
            Assert.IsType<BadRequestObjectResult>(eventController.Add(eventInfo));
        }
    }
}
