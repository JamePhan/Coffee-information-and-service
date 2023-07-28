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
using AutoMapper;
using Org.BouncyCastle.Asn1.Pkcs;
using Castle.Core.Resource;

namespace Capstone_UnitTest.Controller
{
    public class ViewDetailService_Test
    {
        private readonly Mock<CoffeehouseSystemContext> _mockContext;
        private readonly Mock<IMapper> _mockMapper;

        public ViewDetailService_Test()
        {
            _mockContext = new Mock<CoffeehouseSystemContext>();
            _mockMapper = new Mock<IMapper>();
        }

        [Fact]
        public void TC1_ViewDetailService_Test()
        {
            Test_ViewDetailService_Success(2);
        }

        [Fact]
        public void TC2_ViewDetailService_Test()
        {
            Test_ViewDetailService_Success(4);
        }

        [Fact]
        public void TC3_ViewDetailService_Test()
        {
            Test_ViewDetailService_Success(5);
        }

        [Fact]
        public void TC4_ViewDetailService_Test()
        {
            Test_ViewDetailService_Fail(9);
        }

        public void Test_ViewDetailService_Success(int id)
        {
            var services = new List<Service>
            {
                new Service { ServiceId = 1, Name = "Pay1" },
                new Service { ServiceId = 2, Name = "Pay2" },
                new Service { ServiceId = 3, Name = "Pay4" },
                new Service { ServiceId = 4, Name = "Pay3" },
                new Service { ServiceId = 5, Name = "Pay5" },
            };

            var mockDBSet = new Mock<DbSet<Service>>();
            mockDBSet.As<IQueryable<Service>>().Setup(m => m.Provider).Returns(services.AsQueryable().Provider);
            mockDBSet.As<IQueryable<Service>>().Setup(m => m.Expression).Returns(services.AsQueryable().Expression);
            mockDBSet.As<IQueryable<Service>>().Setup(m => m.ElementType).Returns(services.AsQueryable().ElementType);
            mockDBSet.As<IQueryable<Service>>().Setup(m => m.GetEnumerator()).Returns(services.AsQueryable().GetEnumerator());
            _mockContext.SetupGet(m => m.Services).Returns(mockDBSet.Object);

            var followInfos = new ServiceInfo();
            followInfos.ServiceId = 2;
            followInfos.Name = "Payment";
            followInfos.UserId = 3;

            _mockMapper.Setup(m => m.Map<Service, ServiceInfo>(It.IsAny<Service>())).Returns(followInfos);
            ServiceController serviceController = new ServiceController(_mockContext.Object, _mockMapper.Object);

            Assert.IsType<OkObjectResult>(serviceController.Detail(id));

            _mockContext.Verify(c => c.Services, Times.Exactly(1));
            _mockMapper.Verify(c => c.Map<Service, ServiceInfo>(It.IsAny<Service>()), Times.Once);
        }

        public void Test_ViewDetailService_Fail(int id)
        {
            var services = new List<Service>
            {
                new Service { ServiceId = 1, Name = "Pay1" },
                new Service { ServiceId = 2, Name = "Pay2" },
                new Service { ServiceId = 3, Name = "Pay4" },
                new Service { ServiceId = 4, Name = "Pay3" },
                new Service { ServiceId = 5, Name = "Pay5" },
            };

            var mockDBSet = new Mock<DbSet<Service>>();
            mockDBSet.As<IQueryable<Service>>().Setup(m => m.Provider).Returns(services.AsQueryable().Provider);
            mockDBSet.As<IQueryable<Service>>().Setup(m => m.Expression).Returns(services.AsQueryable().Expression);
            mockDBSet.As<IQueryable<Service>>().Setup(m => m.ElementType).Returns(services.AsQueryable().ElementType);
            mockDBSet.As<IQueryable<Service>>().Setup(m => m.GetEnumerator()).Returns(services.AsQueryable().GetEnumerator());
            _mockContext.SetupGet(m => m.Services).Returns(mockDBSet.Object);

            var followInfos = new ServiceInfo();
            followInfos.ServiceId = 2;
            followInfos.Name = "Payment";
            followInfos.UserId = 3;

            _mockMapper.Setup(m => m.Map<Service, ServiceInfo>(It.IsAny<Service>())).Returns(followInfos);
            ServiceController serviceController = new ServiceController(_mockContext.Object, _mockMapper.Object);

            Assert.IsType<NotFoundResult>(serviceController.Detail(id));

            _mockContext.Verify(c => c.Services, Times.Exactly(1));
            _mockMapper.Verify(c => c.Map<Service, ServiceInfo>(It.IsAny<Service>()), Times.Never);
        }
    }
}
