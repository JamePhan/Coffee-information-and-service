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
    public class ViewListService_Test
    {
        private readonly Mock<CoffeehouseSystemContext> _mockContext;
        private readonly Mock<IMapper> _mockMapper;

        public ViewListService_Test()
        {
            _mockContext = new Mock<CoffeehouseSystemContext>();
            _mockMapper = new Mock<IMapper>();
        }

        [Fact]
        public void TC1_ViewListService_Test()
        {
            Test_ViewListService_Success(5);
        }

        [Fact]
        public void TC2_ViewListService_Test()
        {
            Test_ViewListService_Success(1);
        }

        [Fact]
        public void TC3_ViewListService_Test()
        {
            Test_ViewListService_Success(8);
        }

        [Fact]
        public void TC4_ViewListService_Test()
        {
            Test_ViewListService_Fail(5);
        }

        public void Test_ViewListService_Success(int count)
        {
            var services = new List<Service>
            {
                new Service { ServiceId = 1, Name = "Pay1", User = new User { Account = new Account { IsBanned = false }} },
                new Service { ServiceId = 2, Name = "Pay2", User = new User { Account = new Account { IsBanned = false }} },
                new Service { ServiceId = 3, Name = "Pay4", User = new User { Account = new Account { IsBanned = false }} },
                new Service { ServiceId = 4, Name = "Pay3", User = new User { Account = new Account { IsBanned = false }} },
                new Service { ServiceId = 5, Name = "Pay5", User = new User { Account = new Account { IsBanned = false }} },
            };

            var mockDBSet = new Mock<DbSet<Service>>();
            mockDBSet.As<IQueryable<Service>>().Setup(m => m.Provider).Returns(services.AsQueryable().Provider);
            mockDBSet.As<IQueryable<Service>>().Setup(m => m.Expression).Returns(services.AsQueryable().Expression);
            mockDBSet.As<IQueryable<Service>>().Setup(m => m.ElementType).Returns(services.AsQueryable().ElementType);
            mockDBSet.As<IQueryable<Service>>().Setup(m => m.GetEnumerator()).Returns(services.AsQueryable().GetEnumerator());
            _mockContext.SetupGet(m => m.Services).Returns(mockDBSet.Object);

            var followInfos = new List<ServiceInfo>
            {
                new ServiceInfo { ServiceId = 1, Name = "Pay1" },
                new ServiceInfo { ServiceId = 2, Name = "Pay2" },
                new ServiceInfo { ServiceId = 3, Name = "Pay4" },
                new ServiceInfo { ServiceId = 4, Name = "Pay3" },
                new ServiceInfo { ServiceId = 5, Name = "Pay5" },
            };

            _mockMapper.Setup(m => m.Map<List<Service>, List<ServiceInfo>> (It.IsAny<List<Service>>())).Returns(followInfos);

            ServiceController serviceController = new ServiceController(_mockContext.Object, _mockMapper.Object);

            Assert.IsType<OkObjectResult>(serviceController.List());

            _mockContext.Verify(c => c.Services, Times.Exactly(1));
            _mockMapper.Verify(c => c.Map<List<Service>, List<ServiceInfo>>(It.IsAny<List<Service>>()), Times.Once);
        }

        public void Test_ViewListService_Fail(int count)
        {
            var services = new List<Service>
            {
  
            };

            var mockDBSet = new Mock<DbSet<Service>>();
            mockDBSet.As<IQueryable<Service>>().Setup(m => m.Provider).Returns(services.AsQueryable().Provider);
            mockDBSet.As<IQueryable<Service>>().Setup(m => m.Expression).Returns(services.AsQueryable().Expression);
            mockDBSet.As<IQueryable<Service>>().Setup(m => m.ElementType).Returns(services.AsQueryable().ElementType);
            mockDBSet.As<IQueryable<Service>>().Setup(m => m.GetEnumerator()).Returns(services.AsQueryable().GetEnumerator());
            _mockContext.SetupGet(m => m.Services).Returns(mockDBSet.Object);

            var followInfos = new List<ServiceInfo>
            {
              
            };

            _mockMapper.Setup(m => m.Map<List<Service>, List<ServiceInfo>>(It.IsAny<List<Service>>())).Returns(followInfos);

            ServiceController serviceController = new ServiceController(_mockContext.Object, _mockMapper.Object);

            Assert.IsType<NotFoundResult>(serviceController.List());

            _mockContext.Verify(c => c.Services, Times.Exactly(1));
            _mockMapper.Verify(c => c.Map<List<Service>, List<ServiceInfo>>(It.IsAny<List<Service>>()), Times.Once);
        }

    }
}
