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
using Microsoft.EntityFrameworkCore.ChangeTracking;
using FakeItEasy;
using Microsoft.EntityFrameworkCore.Metadata;
using Castle.DynamicProxy;

namespace Capstone_UnitTest.Controller
{
    public class ForgetPassword_Test
    {
        private readonly Mock<CoffeehouseSystemContext> _mockContext;
        private readonly Mock<IConfiguration> _mockConfiguration;

        public ForgetPassword_Test()
        {
            _mockContext = new Mock<CoffeehouseSystemContext>();
            _mockConfiguration = new Mock<IConfiguration>();

        }

        [Fact]
        public void TC1_ForgetPassword_Test()
        {
            EmailEncapsulation mail = new EmailEncapsulation();
            mail.Email = "jamejame@gmail.com";
            Test_ForgetPassword(mail);
        }

      
        public void Test_ForgetPassword(EmailEncapsulation mail)
        {

            var accounts = new List<Account>
            {
                new Account { Username = "jame", Password = "jame", AccountId = 1, IsBanned = false }
            };
            var mockDBAccount = new Mock<DbSet<Account>>();
            mockDBAccount.As<IQueryable<Account>>().Setup(m => m.Provider).Returns(accounts.AsQueryable().Provider);
            mockDBAccount.As<IQueryable<Account>>().Setup(m => m.Expression).Returns(accounts.AsQueryable().Expression);
            mockDBAccount.As<IQueryable<Account>>().Setup(m => m.ElementType).Returns(accounts.AsQueryable().ElementType);
            mockDBAccount.As<IQueryable<Account>>().Setup(m => m.GetEnumerator()).Returns(accounts.AsQueryable().GetEnumerator());
            _mockContext.SetupGet(m => m.Accounts).Returns(mockDBAccount.Object);

            var users = new List<User>
            {
                new User { AccountId = 1, Email = "jamejame@gmail.com" }
            };
            var mockDBusers = new Mock<DbSet<User>>();
            mockDBusers.As<IQueryable<User>>().Setup(m => m.Provider).Returns(users.AsQueryable().Provider);
            mockDBusers.As<IQueryable<User>>().Setup(m => m.Expression).Returns(users.AsQueryable().Expression);
            mockDBusers.As<IQueryable<User>>().Setup(m => m.ElementType).Returns(users.AsQueryable().ElementType);
            mockDBusers.As<IQueryable<User>>().Setup(m => m.GetEnumerator()).Returns(users.AsQueryable().GetEnumerator());
            _mockContext.SetupGet(m => m.Users).Returns(mockDBusers.Object);

            var customers = new List<Customer>
            {
                new Customer { AccountId = 3, Email = "qwer12314@gmail.com" }
            };
            var mockDBcustomer = new Mock<DbSet<Customer>>();
            mockDBcustomer.As<IQueryable<Customer>>().Setup(m => m.Provider).Returns(customers.AsQueryable().Provider);
            mockDBcustomer.As<IQueryable<Customer>>().Setup(m => m.Expression).Returns(customers.AsQueryable().Expression);
            mockDBcustomer.As<IQueryable<Customer>>().Setup(m => m.ElementType).Returns(customers.AsQueryable().ElementType);
            mockDBcustomer.As<IQueryable<Customer>>().Setup(m => m.GetEnumerator()).Returns(customers.AsQueryable().GetEnumerator());
            _mockContext.SetupGet(m => m.Customers).Returns(mockDBcustomer.Object);

            AccountController accountController = new AccountController(_mockContext.Object, _mockConfiguration.Object);

            Assert.IsType<OkObjectResult>(accountController.Forget(mail));
        }

    }



}
