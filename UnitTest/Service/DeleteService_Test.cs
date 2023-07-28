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
using AngleSharp.Dom;

namespace Capstone_UnitTest.Controller
{
    public class DeleteService_Test
    {
        private readonly Mock<CoffeehouseSystemContext> _mockContext;
        private readonly Mock<IMapper> _mockMapper;

        public DeleteService_Test()
        {
            _mockContext = new Mock<CoffeehouseSystemContext>();
            _mockMapper = new Mock<IMapper>();
        }

        [Fact]
        public void TC1_DeleteService_Test()
        {
            Test_DeleteService_Success(2);
        }

        [Fact]
        public void TC2_DeleteService_Test()
        {
            Test_DeleteService_Success(1);
        }

        [Fact]
        public void TC3_DeleteService_Test()
        {
            Test_DeleteService_Success(3);
        }


        [Fact]
        public void TC4_DeleteService_Test()
        {
            Test_DeleteService_Fail(5);
        }

        public void Test_DeleteService_Success(int id)
        {
            var services = new List<Service>
            {
                new Service { ServiceId = 1, Name = "Pay1" },
                new Service { ServiceId = 2, Name = "Pay2" },
                new Service { ServiceId = 3, Name = "Pay2" },
            };

            var mockDBSet = new Mock<DbSet<Service>>();
            mockDBSet.As<IQueryable<Service>>().Setup(m => m.Provider).Returns(services.AsQueryable().Provider);
            mockDBSet.As<IQueryable<Service>>().Setup(m => m.Expression).Returns(services.AsQueryable().Expression);
            mockDBSet.As<IQueryable<Service>>().Setup(m => m.ElementType).Returns(services.AsQueryable().ElementType);
            mockDBSet.As<IQueryable<Service>>().Setup(m => m.GetEnumerator()).Returns(services.AsQueryable().GetEnumerator());
            mockDBSet.Setup(x => x.Remove(It.IsAny<Service>())).Callback<Service>((entity) => services.Remove(entity));
            _mockContext.SetupGet(m => m.Services).Returns(mockDBSet.Object);

            ServiceController serviceController = new ServiceController(_mockContext.Object, _mockMapper.Object);

            Assert.IsType<OkResult>(serviceController.Delete(id));
            _mockContext.Verify(c => c.Services, Times.Exactly(2));
            _mockContext.Verify(c => c.SaveChanges(), Times.Once);
            Assert.Equal(2, _mockContext.Object.Services.Count());
        }

        public void Test_DeleteService_Fail(int id)
        {
            var services = new List<Service>
            {
                new Service { ServiceId = 1, Name = "Pay1" },
                new Service { ServiceId = 2, Name = "Pay2" },
            };

            var mockDBSet = new Mock<DbSet<Service>>();
            mockDBSet.As<IQueryable<Service>>().Setup(m => m.Provider).Returns(services.AsQueryable().Provider);
            mockDBSet.As<IQueryable<Service>>().Setup(m => m.Expression).Returns(services.AsQueryable().Expression);
            mockDBSet.As<IQueryable<Service>>().Setup(m => m.ElementType).Returns(services.AsQueryable().ElementType);
            mockDBSet.As<IQueryable<Service>>().Setup(m => m.GetEnumerator()).Returns(services.AsQueryable().GetEnumerator());
            mockDBSet.Setup(x => x.Remove(It.IsAny<Service>())).Callback<Service>((entity) => services.Remove(entity));
            _mockContext.SetupGet(m => m.Services).Returns(mockDBSet.Object);

            ServiceController serviceController = new ServiceController(_mockContext.Object, _mockMapper.Object);

            Assert.IsType<BadRequestResult>(serviceController.Delete(id));
            _mockContext.Verify(c => c.Services, Times.Exactly(1));
            _mockContext.Verify(c => c.SaveChanges(), Times.Never);
            Assert.Equal(2, _mockContext.Object.Services.Count());
        }

    }
}
