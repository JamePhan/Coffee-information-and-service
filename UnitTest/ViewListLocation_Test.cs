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
using AngleSharp.Dom;

namespace Capstone_UnitTest.Controller
{
    public class ViewListLocation_Test
    {
        private readonly Mock<CoffeehouseSystemContext> _mockContext;
        private readonly Mock<IMapper> _mockMapper;

        public ViewListLocation_Test()
        {
            _mockContext = new Mock<CoffeehouseSystemContext>();
            _mockMapper = new Mock<IMapper>();
        }

        [Fact]
        public void TC1_ViewListLocation_Test()
        {
            Test_ViewListLocation_Success(2);
        }

        [Fact]
        public void TC2_ViewListLocation_Test()
        {
            Test_ViewListLocation_Success(5);
        }

        [Fact]
        public void TC3_ViewListLocation_Test()
        {
            Test_ViewListLocation_Success(3);
        }

        [Fact]
        public void TC4_ViewListLocation_Test()
        {
            Test_ViewListLocation_Fail(0);
        }

        [Fact]
        public void TC5_ViewListLocation_Test()
        {
            Test_ViewListLocation_Fail(-5);
        }

        [Fact]
        public void TC6_ViewListLocation_Test()
        {
            Test_ViewListLocation_Fail(1);
        }


        public void Test_ViewListLocation_Success(int count)
        {
            var locations = new List<Location>
            {
                new Location { },
                new Location { },
            };
            var mockDBLocation = new Mock<DbSet<Location>>();
            mockDBLocation.As<IQueryable<Location>>().Setup(m => m.Provider).Returns(locations.AsQueryable().Provider);
            mockDBLocation.As<IQueryable<Location>>().Setup(m => m.Expression).Returns(locations.AsQueryable().Expression);
            mockDBLocation.As<IQueryable<Location>>().Setup(m => m.ElementType).Returns(locations.AsQueryable().ElementType);
            mockDBLocation.As<IQueryable<Location>>().Setup(m => m.GetEnumerator()).Returns(locations.AsQueryable().GetEnumerator());
            _mockContext.Setup(m => m.Locations).Returns(mockDBLocation.Object);

            var locationInfos = new List<LocationInfo>
            {
                new LocationInfo { },
                new LocationInfo {  },
            };

            _mockMapper.Setup(m => m.Map<List<Location>, List<LocationInfo>>(It.IsAny<List<Location>>())).Returns(locationInfos);

            LocationController locationController = new LocationController(_mockContext.Object, _mockMapper.Object);

            Assert.IsType<OkObjectResult>(locationController.List());
            _mockContext.Verify(c => c.Locations, Times.Exactly(1));
            _mockMapper.Verify(c => c.Map <List<Location>,List<LocationInfo>>(It.IsAny<List<Location>>()), Times.Once);
        }

        public void Test_ViewListLocation_Fail(int count)
        {
            var locations = new List<Location>
            {
                
            };
            var mockDBLocation = new Mock<DbSet<Location>>();
            mockDBLocation.As<IQueryable<Location>>().Setup(m => m.Provider).Returns(locations.AsQueryable().Provider);
            mockDBLocation.As<IQueryable<Location>>().Setup(m => m.Expression).Returns(locations.AsQueryable().Expression);
            mockDBLocation.As<IQueryable<Location>>().Setup(m => m.ElementType).Returns(locations.AsQueryable().ElementType);
            mockDBLocation.As<IQueryable<Location>>().Setup(m => m.GetEnumerator()).Returns(locations.AsQueryable().GetEnumerator());
            _mockContext.Setup(m => m.Locations).Returns(mockDBLocation.Object);

            var locationInfos = new List<LocationInfo>
            {
                
            };
            _mockMapper.Setup(m => m.Map<List<Location>, List<LocationInfo>>(It.IsAny<List<Location>>())).Returns(locationInfos);

            LocationController locationController = new LocationController(_mockContext.Object, _mockMapper.Object);

            Assert.IsType<NotFoundResult>(locationController.List());
            _mockContext.Verify(c => c.Locations, Times.Exactly(1));
            _mockMapper.Verify(c => c.Map<List<Location>, List<LocationInfo>>(It.IsAny<List<Location>>()), Times.Once);
        }


    }
}
