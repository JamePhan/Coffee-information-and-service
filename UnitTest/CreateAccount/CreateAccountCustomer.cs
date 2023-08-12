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
    public class CreateAccountCustomer
    {
        private readonly Mock<CoffeehouseSystemContext> _mockContext;
        private readonly Mock<IConfiguration> _mockConfiguration;

        public CreateAccountCustomer()
        {
            _mockContext = new Mock<CoffeehouseSystemContext>();
            _mockConfiguration = new Mock<IConfiguration>();
        }

        [Fact]
        public void TC1_CreateAccountCustomer()
        {
            Register reg = new Register();
            reg.Username = "test";
            reg.Fullname = "Just Test";
            reg.Password = "test";
            Test_CreateAccountCustomer(reg);
        }

        [Fact]
        public void TC2_CreateAccountCustomer()
        {
            Register reg = new Register();
            reg.Username = "test";
            reg.Fullname = "Just Test";
            reg.Password = "";
            Test_CreateAccountCustomer(reg);
        }

        [Fact]
        public void TC3_CreateAccountCustomer()
        {
            Register reg = new Register();
            reg.Username = "";
            reg.Fullname = "Just Test";
            reg.Password = "test";
            Test_CreateAccountCustomer(reg);
        }

        [Fact]
        public void TC4_CreateAccountCustomer()
        {
            Register reg = new Register();
            reg.Username = "";
            reg.Fullname = "Just Test";
            reg.Password = "";
            Test_CreateAccountCustomer(reg);
        }

        [Fact]
        public void TC5_CreateAccountCustomer()
        {
            Register reg = new Register();
            reg.Username = "jame";
            reg.Fullname = "Just Test";
            reg.Password = "jame";
            Test_CreateAccountCustomer_Duplicate(reg);
        }

        [Fact]
        public void TC6_CreateAccountCustomer()
        {
            Register reg = new Register();
            reg.Username = "jame22";
            reg.Fullname = "Just Test";
            reg.Password = "jame";
            Test_CreateAccountCustomer_Duplicate(reg);
        }

        public void Test_CreateAccountCustomer(Register registerInfo)
        {
            var accounts = new List<Account>
            {
                new Account { Username = "jame", Password = "jame", AccountId = 1, IsBanned = false, },
                new Account { Username = "jame22", Password = "jame", AccountId = 2, IsBanned = false, }
            };
            var mockDBAccount = new Mock<DbSet<Account>>();
            mockDBAccount.As<IQueryable<Account>>().Setup(m => m.Provider).Returns(accounts.AsQueryable().Provider);
            mockDBAccount.As<IQueryable<Account>>().Setup(m => m.Expression).Returns(accounts.AsQueryable().Expression);
            mockDBAccount.As<IQueryable<Account>>().Setup(m => m.ElementType).Returns(accounts.AsQueryable().ElementType);
            mockDBAccount.As<IQueryable<Account>>().Setup(m => m.GetEnumerator()).Returns(accounts.AsQueryable().GetEnumerator());
            mockDBAccount.Setup(x => x.Add(It.IsAny<Account>())).Callback<Account>(accounts.Add);
            _mockContext.SetupGet(m => m.Accounts).Returns(mockDBAccount.Object);

            AccountController accController = new AccountController(_mockContext.Object, _mockConfiguration.Object);

            Assert.IsType<OkResult>(accController.Register(registerInfo));
            _mockContext.Verify(c => c.Accounts, Times.Exactly(3));
            _mockContext.Verify(c => c.SaveChanges(), Times.Once);
            Assert.Equal(3, _mockContext.Object.Accounts.Count());
        }

        public void Test_CreateAccountCustomer_Duplicate(Register registerInfo)
        {
            var accounts = new List<Account>
            {
                new Account { Username = "jame", Password = "jame", AccountId = 1, IsBanned = false, },
                new Account { Username = "jame22", Password = "jame", AccountId = 2, IsBanned = false, }
            };
            var mockDBAccount = new Mock<DbSet<Account>>();
            mockDBAccount.As<IQueryable<Account>>().Setup(m => m.Provider).Returns(accounts.AsQueryable().Provider);
            mockDBAccount.As<IQueryable<Account>>().Setup(m => m.Expression).Returns(accounts.AsQueryable().Expression);
            mockDBAccount.As<IQueryable<Account>>().Setup(m => m.ElementType).Returns(accounts.AsQueryable().ElementType);
            mockDBAccount.As<IQueryable<Account>>().Setup(m => m.GetEnumerator()).Returns(accounts.AsQueryable().GetEnumerator());
            mockDBAccount.Setup(x => x.Add(It.IsAny<Account>())).Callback<Account>(accounts.Add);
            _mockContext.SetupGet(m => m.Accounts).Returns(mockDBAccount.Object);

            AccountController accController = new AccountController(_mockContext.Object, _mockConfiguration.Object);

            Assert.IsType<BadRequestObjectResult>(accController.Register(registerInfo));
            _mockContext.Verify(c => c.Accounts, Times.Exactly(1));
            _mockContext.Verify(c => c.SaveChanges(), Times.Never);
            Assert.Equal(2, _mockContext.Object.Accounts.Count());
        }

    }
}
