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
    public class AddService_Test
    {
        private readonly Mock<CoffeehouseSystemContext> _mockContext;
        private readonly Mock<IMapper> _mockMapper;

        public AddService_Test()
        {
            _mockContext = new Mock<CoffeehouseSystemContext>();
            _mockMapper = new Mock<IMapper>();
        }

        [Fact]
        public void TC1_CreateNews_Test()
        {
            ServiceInfo newInfo = new ServiceInfo();
            newInfo.ServiceId = 3;
            newInfo.Name = "Pay3";
            Test_AddService(newInfo);
        }

        [Fact]
        public void TC2_CreateNews_Test()
        {
            ServiceInfo newInfo = new ServiceInfo();
            newInfo.ServiceId = 4;
            newInfo.Name = "Pay4";
            Test_AddService(newInfo);
        }

        [Fact]
        public void TC3_CreateNews_Test()
        {
            ServiceInfo newInfo = new ServiceInfo();
            newInfo.ServiceId = 5;
            newInfo.Name = "Pay5";
            Test_AddService(newInfo);
        }

        [Fact]
        public void TC4_CreateNews_Test()
        {
            ServiceInfo newInfo = new ServiceInfo();
            newInfo = null;
            Test_AddService(newInfo);
        }

        public void Test_AddService(ServiceInfo newInfo)
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
            mockDBSet.Setup(x => x.Add(It.IsAny<Service>())).Callback<Service>(services.Add);
            _mockContext.SetupGet(m => m.Services).Returns(mockDBSet.Object);

            var servicesNew = new Service();
            if(newInfo == null)
            {
                servicesNew = null;
            }
            else
            {
                servicesNew.ServiceId = newInfo.ServiceId;
                servicesNew.Name = newInfo.Name;
            }
            
            _mockMapper.Setup(m => m.Map<ServiceInfo, Service>(It.IsAny<ServiceInfo>())).Returns(servicesNew);

            ServiceController serviceController = new ServiceController(_mockContext.Object, _mockMapper.Object);

            Assert.IsType<OkResult>(serviceController.Add(newInfo));
            _mockContext.Verify(c => c.Services, Times.Exactly(1));
            _mockMapper.Verify(c => c.Map<ServiceInfo, Service>(It.IsAny<ServiceInfo>()), Times.Once);
            _mockContext.Verify(c => c.SaveChanges(), Times.Once);
            Assert.Equal(3, _mockContext.Object.Services.Count());

        }
    }
}
