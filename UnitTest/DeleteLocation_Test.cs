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
    public class DeleteLocation_Test
    {
        private readonly Mock<CoffeehouseSystemContext> _mockContext;
        private readonly Mock<IMapper> _mockMapper;

        public DeleteLocation_Test()
        {
            _mockContext = new Mock<CoffeehouseSystemContext>();
            _mockMapper = new Mock<IMapper>();
        }

        [Fact]
        public void TC1_DeleteLocation_Test()
        {
            Test_DeleteLocation_Success(1);
        }

        [Fact]
        public void TC2_DeleteLocation_Test()
        {
            Test_DeleteLocation_Success(2);
        }

        [Fact]
        public void TC3_DeleteLocation_Test()
        {
            Test_DeleteLocation_Success(3);
        }

        [Fact]
        public void TC4_DeleteLocation_Test()
        {

            Test_DeleteLocation_Fail(6);
        }

        [Fact]
        public void TC5_DeleteLocation_Test()
        {
            Test_DeleteLocation_Fail(5);
        }

        [Fact]
        public void TC6_DeleteLocation_Test()
        {
            Test_DeleteLocation_Fail(4);
        }

        public void Test_DeleteLocation_Success(int id)
        {
            var locations = new List<Location>
            {
                new Location { LocationId = 1, },
                new Location { LocationId = 2, },
                new Location { LocationId = 3, },
            };

            var mockDBLocation= new Mock<DbSet<Location>>();
            mockDBLocation.As<IQueryable<Location>>().Setup(m => m.Provider).Returns(locations.AsQueryable().Provider);
            mockDBLocation.As<IQueryable<Location>>().Setup(m => m.Expression).Returns(locations.AsQueryable().Expression);
            mockDBLocation.As<IQueryable<Location>>().Setup(m => m.ElementType).Returns(locations.AsQueryable().ElementType);
            mockDBLocation.As<IQueryable<Location>>().Setup(m => m.GetEnumerator()).Returns(locations.AsQueryable().GetEnumerator());
            _mockContext.Setup(m => m.Locations).Returns(mockDBLocation.Object);

            LocationController locationController = new LocationController(_mockContext.Object, _mockMapper.Object);

            Assert.IsType<OkResult>(locationController.Delete(id));
            _mockContext.Verify(c => c.Locations, Times.Exactly(2));
            _mockContext.Verify(c => c.SaveChanges(), Times.Once);
        }

        public void Test_DeleteLocation_Fail(int id)
        {
            var locations = new List<Location>
            {
                new Location { LocationId = 1, },
                new Location { LocationId = 2, },
            };

            var mockDBLocation = new Mock<DbSet<Location>>();
            mockDBLocation.As<IQueryable<Location>>().Setup(m => m.Provider).Returns(locations.AsQueryable().Provider);
            mockDBLocation.As<IQueryable<Location>>().Setup(m => m.Expression).Returns(locations.AsQueryable().Expression);
            mockDBLocation.As<IQueryable<Location>>().Setup(m => m.ElementType).Returns(locations.AsQueryable().ElementType);
            mockDBLocation.As<IQueryable<Location>>().Setup(m => m.GetEnumerator()).Returns(locations.AsQueryable().GetEnumerator());
            _mockContext.Setup(m => m.Locations).Returns(mockDBLocation.Object);

            LocationController locationController = new LocationController(_mockContext.Object, _mockMapper.Object);

            Assert.IsType<BadRequestResult>(locationController.Delete(id));
            _mockContext.Verify(c => c.Locations, Times.Once);
            _mockContext.Verify(c => c.SaveChanges(), Times.Never);
        }
    }
}
