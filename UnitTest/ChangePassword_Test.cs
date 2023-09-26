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

namespace Capstone_UnitTest.Controller
{
    public class ChangePassword_Test
    {
        private readonly Mock<CoffeehouseSystemContext> _mockContext;
        private readonly Mock<IConfiguration> _mockConfiguration;

        public ChangePassword_Test()
        {
            _mockContext = new Mock<CoffeehouseSystemContext>();
            _mockConfiguration = new Mock<IConfiguration>();

        }

        [Fact]
        public void TC1_ChangePassword_Test()
        {
            ResetPassword reset = new ResetPassword();
            reset.AccountId = 1;
            reset.Password = "jamejame";
            Test_ChangePassword(reset);
        }

        [Fact]
        public void TC2_ChangePassword_Test()
        {
            ResetPassword reset = new ResetPassword();
            reset.AccountId = 2;
            reset.Password = "cus1";
            Test_ChangePassword(reset);
        }

        [Fact]
        public void TC3_ChangePassword_Test()
        {
            ResetPassword reset = new ResetPassword();
            reset.AccountId = 1;
            Test_ChangePassword(reset);
        }

        public void Test_ChangePassword(ResetPassword reset)
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
            AccountController accountController = new AccountController(_mockContext.Object, _mockConfiguration.Object);
            Assert.IsType<OkResult>(accountController.ResetPassword(reset));



        }
    }
}
