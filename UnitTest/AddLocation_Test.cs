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
    public class AddLocation_Test
    {
        private readonly Mock<CoffeehouseSystemContext> _mockContext;
        private readonly Mock<IMapper> _mockMapper;

        public AddLocation_Test()
        {
            _mockContext = new Mock<CoffeehouseSystemContext>();
            _mockMapper = new Mock<IMapper>();
        }

        [Fact]
        public void TC1_AddLocation_Test()
        {
            LocationInfo locationInfo = new LocationInfo();
            locationInfo.LocationId = 1;
            locationInfo.PlusCode = "pluscode1";
            locationInfo.UserId = 1;
            Test_AddLocation_Success(locationInfo);
        }

        [Fact]
        public void TC2_AddLocation_Test()
        {
            LocationInfo locationInfo = new LocationInfo();
            locationInfo.LocationId = 2;
            locationInfo.PlusCode = "pluscode1";
            locationInfo.UserId = 1;
            Test_AddLocation_Success(locationInfo);
        }

        [Fact]
        public void TC3_AddLocation_Test()
        {
            LocationInfo locationInfo = new LocationInfo();
            locationInfo.LocationId = 4;
            locationInfo.PlusCode = "pluscode2";
            locationInfo.UserId = 3;
            Test_AddLocation_Success(locationInfo);
        }

        [Fact]
        public void TC4_AddLocation_Test()
        {
            LocationInfo locationInfo = new LocationInfo();
            locationInfo.LocationId = 1;
            locationInfo.PlusCode = "pluscode3";
            locationInfo.UserId = 1;
            Test_AddLocation_Fail(locationInfo);
        }

        [Fact]
        public void TC5_AddLocation_Test()
        {
            LocationInfo locationInfo = new LocationInfo();
            locationInfo.LocationId = 2;
            locationInfo.PlusCode = "pluscode4";
            locationInfo.UserId = 1;
            Test_AddLocation_Fail(locationInfo);
        }

        [Fact]
        public void TC6_AddLocation_Test()
        {
            LocationInfo locationInfo = new LocationInfo();
            locationInfo.LocationId = 4;
            locationInfo.PlusCode = "pluscode3";
            locationInfo.UserId = 3;
            Test_AddLocation_Fail(locationInfo);
        }


        public void Test_AddLocation_Success(LocationInfo locationInfo)
        {
            var locations = new List<Location>
            {
                new Location { PlusCode = "pluscode1", },
                new Location { PlusCode = "pluscode2", },
            };

            var mockDBLocation= new Mock<DbSet<Location>>();
            mockDBLocation.As<IQueryable<Location>>().Setup(m => m.Provider).Returns(locations.AsQueryable().Provider);
            mockDBLocation.As<IQueryable<Location>>().Setup(m => m.Expression).Returns(locations.AsQueryable().Expression);
            mockDBLocation.As<IQueryable<Location>>().Setup(m => m.ElementType).Returns(locations.AsQueryable().ElementType);
            mockDBLocation.As<IQueryable<Location>>().Setup(m => m.GetEnumerator()).Returns(locations.AsQueryable().GetEnumerator());
            _mockContext.Setup(m => m.Locations).Returns(mockDBLocation.Object);

            Location locationNew = new Location();
            if (locationNew == null)
            {
                locationNew = null;
                _mockMapper.Setup(m => m.Map<LocationInfo, Location>(It.IsAny<LocationInfo>())).Returns(locationNew);
            }
            else
            {
                locationNew.LocationId = locationInfo.LocationId;
                locationNew.PlusCode = locationInfo.PlusCode;
                locationNew.UserId = locationInfo.UserId;
                _mockMapper.Setup(m => m.Map<LocationInfo, Location>(It.IsAny<LocationInfo>())).Returns(locationNew);
            }
            LocationController locationController = new LocationController(_mockContext.Object, _mockMapper.Object);

            Assert.IsType<OkResult>(locationController.Add(locationInfo));
            _mockContext.Verify(c => c.Locations.Add(locationNew), Times.Once);
            _mockContext.Verify(c => c.SaveChanges(), Times.Once);
        }

        public void Test_AddLocation_Fail(LocationInfo locationInfo)
        {
            var locations = new List<Location>
            {
                new Location { PlusCode = "pluscode1", },
                new Location { PlusCode = "pluscode2", },
            };

            var mockDBLocation = new Mock<DbSet<Location>>();
            mockDBLocation.As<IQueryable<Location>>().Setup(m => m.Provider).Returns(locations.AsQueryable().Provider);
            mockDBLocation.As<IQueryable<Location>>().Setup(m => m.Expression).Returns(locations.AsQueryable().Expression);
            mockDBLocation.As<IQueryable<Location>>().Setup(m => m.ElementType).Returns(locations.AsQueryable().ElementType);
            mockDBLocation.As<IQueryable<Location>>().Setup(m => m.GetEnumerator()).Returns(locations.AsQueryable().GetEnumerator());
            _mockContext.Setup(m => m.Locations).Returns(mockDBLocation.Object);

            Location locationNew = new Location();
            if (locationNew == null)
            {
                locationNew = null;
                _mockMapper.Setup(m => m.Map<LocationInfo, Location>(It.IsAny<LocationInfo>())).Returns(locationNew);
            }
            else
            {
                locationNew.LocationId = locationInfo.LocationId;
                locationNew.PlusCode = locationInfo.PlusCode;
                locationNew.UserId = locationInfo.UserId;
                _mockMapper.Setup(m => m.Map<LocationInfo, Location>(It.IsAny<LocationInfo>())).Returns(locationNew);
            }
            LocationController locationController = new LocationController(_mockContext.Object, _mockMapper.Object);

            Assert.IsType<BadRequestResult>(locationController.Add(locationInfo));
            _mockContext.Verify(c => c.Locations.Add(locationNew), Times.Never);
            _mockContext.Verify(c => c.SaveChanges(), Times.Never);
        }
    }
}
