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
    public class CreateAccountUser
    {
        private readonly Mock<CoffeehouseSystemContext> _mockContext;
        private readonly Mock<IMapper> _mockMapper;

        public CreateAccountUser()
        {
            _mockContext = new Mock<CoffeehouseSystemContext>();
            _mockMapper = new Mock<IMapper>();
        }

        [Fact]
        public void TC1_CreateAccountUser()
        {
            Test_CreateAccountUser_Success(1);
        }

        [Fact]
        public void TC2_CreateAccountUser()
        {
            Test_CreateAccountUser_Success(2);
        }

        [Fact]
        public void TC3_CreateAccountUser()
        {
            Test_CreateAccountUser_Fail(3);
        }

        [Fact]
        public void TC4_CreateAccountUser()
        {
            Test_CreateAccountUser_Fail(9);
        }

        public void Test_CreateAccountUser_Success(int id)
        {
            
            var waits = new List<Waiting>
            {

            };
            var mockDBSet = new Mock<DbSet<Waiting>>();
            mockDBSet.As<IQueryable<Waiting>>().Setup(m => m.Provider).Returns(waits.AsQueryable().Provider);
            mockDBSet.As<IQueryable<Waiting>>().Setup(m => m.Expression).Returns(waits.AsQueryable().Expression);
            mockDBSet.As<IQueryable<Waiting>>().Setup(m => m.ElementType).Returns(waits.AsQueryable().ElementType);
            mockDBSet.As<IQueryable<Waiting>>().Setup(m => m.GetEnumerator()).Returns(waits.AsQueryable().GetEnumerator());
            mockDBSet.Setup(x => x.Add(It.IsAny<Waiting>())).Callback<Waiting>(waits.Add);
            _mockContext.SetupGet(m => m.Waitings).Returns(mockDBSet.Object);

            var css = new List<Customer>
            {
                new Customer{ CustomerId = 1, Name = "Cuss1" },
                new Customer{ CustomerId = 2, Name = "Cuss2" },
            };
            var mockDBSet2 = new Mock<DbSet<Customer>>();
            mockDBSet2.As<IQueryable<Customer>>().Setup(m => m.Provider).Returns(css.AsQueryable().Provider);
            mockDBSet2.As<IQueryable<Customer>>().Setup(m => m.Expression).Returns(css.AsQueryable().Expression);
            mockDBSet2.As<IQueryable<Customer>>().Setup(m => m.ElementType).Returns(css.AsQueryable().ElementType);
            mockDBSet2.As<IQueryable<Customer>>().Setup(m => m.GetEnumerator()).Returns(css.AsQueryable().GetEnumerator());
            _mockContext.SetupGet(m => m.Customers).Returns(mockDBSet2.Object);

            var usersInfo = new CustomerInfo();
            if(id == 1)
            {
                usersInfo.CustomerId = 1;
                usersInfo.Name = "Cuss1";
                _mockMapper.Setup(m => m.Map<Customer, CustomerInfo>(It.IsAny<Customer>())).Returns(usersInfo);

                var waitInfo = new WaitingInfo();
                waitInfo.WaitingId = 1;
                waitInfo.AccountId = 1;
                _mockMapper.Setup(m => m.Map<CustomerInfo, WaitingInfo>(It.IsAny<CustomerInfo>())).Returns(waitInfo);
                var waitting = new Waiting();
                waitting.WaitingId = 1;
                waitting.AccountId = 1;
                _mockMapper.Setup(m => m.Map<WaitingInfo, Waiting>(It.IsAny<WaitingInfo>())).Returns(waitting);

            }
            else if(id == 2)
            {
                usersInfo.CustomerId = 2;
                usersInfo.Name = "Cuss2";
                _mockMapper.Setup(m => m.Map<Customer, CustomerInfo>(It.IsAny<Customer>())).Returns(usersInfo);

                var waitInfo = new WaitingInfo();
                waitInfo.WaitingId = 2;
                waitInfo.AccountId = 2;
                _mockMapper.Setup(m => m.Map<CustomerInfo, WaitingInfo>(It.IsAny<CustomerInfo>())).Returns(waitInfo);
                var waitting = new Waiting();
                waitting.WaitingId = 2;
                waitting.AccountId = 2;
                _mockMapper.Setup(m => m.Map<WaitingInfo, Waiting>(It.IsAny<WaitingInfo>())).Returns(waitting);

            }
            WaitingController wController = new WaitingController(_mockContext.Object, _mockMapper.Object);

            Assert.Equal(0, _mockContext.Object.Waitings.Count());
            Assert.IsType<OkResult>(wController.Request(id));
            _mockContext.Verify(c => c.SaveChanges(), Times.Once);
            Assert.Equal(1, _mockContext.Object.Waitings.Count());
        }

        public void Test_CreateAccountUser_Fail(int id)
        {

            var waits = new List<Waiting>
            {

            };
            var mockDBSet = new Mock<DbSet<Waiting>>();
            mockDBSet.As<IQueryable<Waiting>>().Setup(m => m.Provider).Returns(waits.AsQueryable().Provider);
            mockDBSet.As<IQueryable<Waiting>>().Setup(m => m.Expression).Returns(waits.AsQueryable().Expression);
            mockDBSet.As<IQueryable<Waiting>>().Setup(m => m.ElementType).Returns(waits.AsQueryable().ElementType);
            mockDBSet.As<IQueryable<Waiting>>().Setup(m => m.GetEnumerator()).Returns(waits.AsQueryable().GetEnumerator());
            mockDBSet.Setup(x => x.Add(It.IsAny<Waiting>())).Callback<Waiting>(waits.Add);
            _mockContext.SetupGet(m => m.Waitings).Returns(mockDBSet.Object);

            var css = new List<Customer>
            {
                new Customer{ CustomerId = 1, Name = "Cuss1" },
                new Customer{ CustomerId = 2, Name = "Cuss2" },
            };
            var mockDBSet2 = new Mock<DbSet<Customer>>();
            mockDBSet2.As<IQueryable<Customer>>().Setup(m => m.Provider).Returns(css.AsQueryable().Provider);
            mockDBSet2.As<IQueryable<Customer>>().Setup(m => m.Expression).Returns(css.AsQueryable().Expression);
            mockDBSet2.As<IQueryable<Customer>>().Setup(m => m.ElementType).Returns(css.AsQueryable().ElementType);
            mockDBSet2.As<IQueryable<Customer>>().Setup(m => m.GetEnumerator()).Returns(css.AsQueryable().GetEnumerator());
            _mockContext.SetupGet(m => m.Customers).Returns(mockDBSet2.Object);

            var usersInfo = new CustomerInfo();
            if (id == 1)
            {
                usersInfo.CustomerId = 1;
                usersInfo.Name = "Cuss1";
                _mockMapper.Setup(m => m.Map<Customer, CustomerInfo>(It.IsAny<Customer>())).Returns(usersInfo);

                var waitInfo = new WaitingInfo();
                waitInfo.WaitingId = 1;
                waitInfo.AccountId = 1;
                _mockMapper.Setup(m => m.Map<CustomerInfo, WaitingInfo>(It.IsAny<CustomerInfo>())).Returns(waitInfo);
                var waitting = new Waiting();
                waitting.WaitingId = 1;
                waitting.AccountId = 1;
                _mockMapper.Setup(m => m.Map<WaitingInfo, Waiting>(It.IsAny<WaitingInfo>())).Returns(waitting);

            }
            else if (id == 2)
            {
                usersInfo.CustomerId = 2;
                usersInfo.Name = "Cuss2";
                _mockMapper.Setup(m => m.Map<Customer, CustomerInfo>(It.IsAny<Customer>())).Returns(usersInfo);

                var waitInfo = new WaitingInfo();
                waitInfo.WaitingId = 2;
                waitInfo.AccountId = 2;
                _mockMapper.Setup(m => m.Map<CustomerInfo, WaitingInfo>(It.IsAny<CustomerInfo>())).Returns(waitInfo);
                var waitting = new Waiting();
                waitting.WaitingId = 2;
                waitting.AccountId = 2;
                _mockMapper.Setup(m => m.Map<WaitingInfo, Waiting>(It.IsAny<WaitingInfo>())).Returns(waitting);

            }
            WaitingController wController = new WaitingController(_mockContext.Object, _mockMapper.Object);

            Assert.Equal(0, _mockContext.Object.Waitings.Count());
            Assert.IsType<NotFoundObjectResult>(wController.Request(id));
            _mockContext.Verify(c => c.SaveChanges(), Times.Never);
            Assert.Equal(0, _mockContext.Object.Waitings.Count());
        }
    }
}
