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
using Newtonsoft.Json.Linq;
using MailKit;
using Org.BouncyCastle.Utilities.Collections;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Security.Cryptography;

namespace Capstone_UnitTest.Controller
{

    public class Login_Test
    {

        private readonly Mock<CoffeehouseSystemContext> _mockContext;
        private readonly Mock<IConfiguration> _mockConfiguration;

        public Login_Test()
        {
            _mockContext = new Mock<CoffeehouseSystemContext>();
            _mockConfiguration = new Mock<IConfiguration>();

        }

        [Fact]
        public void TC1_Login()
        {
            Login login = new Login();
            login.Username = "jame";
            login.Password = "jame";
            Test_Login_Success(login);
        }

        [Fact]
        public void TC2_Login()
        {
            Login login = new Login();
            login.Username = "jame";
            login.Password = "jam";
            Test_Login_Success(login);
        }

        [Fact]
        public void TC3_Login()
        {
            Login login = new Login();
            login.Username = "jam";
            login.Password = "jame";
            Test_Login_Success(login);
        }

        [Fact]
        public void TC4_Login()
        {
            Login login = new Login();
            login.Username = "jame";
            login.Password = null;
            Test_Login_Success(login);
        }

        [Fact]
        public void TC5_Login()
        {
            Login login = new Login();
            login.Username = null;
            login.Password = "jame";
            Test_Login_Success(login);
        }

        [Fact]
        public void TC6_Login()
        {
            Login login = new Login();
            login.Username = null;
            login.Password = null;
            Test_Login_Success(login);
        }

        [Fact]
        public void TC7_Login()
        {
            Login login = new Login();
            login.Password = "jame";
            Test_Login_Success(login);
        }

        public void Test_Login_Success(Login login)
        {
            var accounts = new List<Account>
            {
                new Account { Username = "jame", Password = "jame", AccountId = 1, IsBanned = false, }
            };
            var mockDBAccount = new Mock<DbSet<Account>>();
            mockDBAccount.As<IQueryable<Account>>().Setup(m => m.Provider).Returns(accounts.AsQueryable().Provider);
            mockDBAccount.As<IQueryable<Account>>().Setup(m => m.Expression).Returns(accounts.AsQueryable().Expression);
            mockDBAccount.As<IQueryable<Account>>().Setup(m => m.ElementType).Returns(accounts.AsQueryable().ElementType);
            mockDBAccount.As<IQueryable<Account>>().Setup(m => m.GetEnumerator()).Returns(accounts.AsQueryable().GetEnumerator());

            var admins = new List<Admin>
            {
                new Admin { AccountId = 1 }
            };
            var mockDBadmin = new Mock<DbSet<Admin>>();
            mockDBadmin.As<IQueryable<Admin>>().Setup(m => m.Provider).Returns(admins.AsQueryable().Provider);
            mockDBadmin.As<IQueryable<Admin>>().Setup(m => m.Expression).Returns(admins.AsQueryable().Expression);
            mockDBadmin.As<IQueryable<Admin>>().Setup(m => m.ElementType).Returns(admins.AsQueryable().ElementType);
            mockDBadmin.As<IQueryable<Admin>>().Setup(m => m.GetEnumerator()).Returns(admins.AsQueryable().GetEnumerator());

            _mockContext.SetupGet(m => m.Admins).Returns(mockDBadmin.Object);

            var users = new List<User>
            {
                new User { AccountId = 2 }
            };
            var mockDBusers = new Mock<DbSet<User>>();
            mockDBusers.As<IQueryable<User>>().Setup(m => m.Provider).Returns(users.AsQueryable().Provider);
            mockDBusers.As<IQueryable<User>>().Setup(m => m.Expression).Returns(users.AsQueryable().Expression);
            mockDBusers.As<IQueryable<User>>().Setup(m => m.ElementType).Returns(users.AsQueryable().ElementType);
            mockDBusers.As<IQueryable<User>>().Setup(m => m.GetEnumerator()).Returns(users.AsQueryable().GetEnumerator());
            _mockContext.SetupGet(m => m.Users).Returns(mockDBusers.Object);

            var customers = new List<Customer>
            {
                new Customer { AccountId = 3 }
            };
            var mockDBcustomer = new Mock<DbSet<Customer>>();
            mockDBcustomer.As<IQueryable<Customer>>().Setup(m => m.Provider).Returns(customers.AsQueryable().Provider);
            mockDBcustomer.As<IQueryable<Customer>>().Setup(m => m.Expression).Returns(customers.AsQueryable().Expression);
            mockDBcustomer.As<IQueryable<Customer>>().Setup(m => m.ElementType).Returns(customers.AsQueryable().ElementType);
            mockDBcustomer.As<IQueryable<Customer>>().Setup(m => m.GetEnumerator()).Returns(customers.AsQueryable().GetEnumerator());
            _mockContext.SetupGet(m => m.Customers).Returns(mockDBcustomer.Object);

            _mockContext.SetupGet(m => m.Accounts).Returns(mockDBAccount.Object);
            _mockConfiguration.Setup(x => x["Jwt:Audience"]).Returns(Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames.Aud);
            _mockConfiguration.Setup(x => x["Jwt:Issuer"]).Returns(Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames.Iss);

            byte[] key = new byte[16];
            using (var rngProvider = new RNGCryptoServiceProvider())
            {
                rngProvider.GetBytes(key);
            }
            var securityKey = new SymmetricSecurityKey(key);
            _mockConfiguration.Setup(x => x["Jwt:Key"]).Returns(securityKey.ToString);
    
            if (login.Username == accounts.First().Username && login.Password == accounts.First().Password)
            {
                AccountController accountController = new AccountController(_mockContext.Object, _mockConfiguration.Object);
                Assert.IsType<OkObjectResult>(accountController.Login(login));
            }
            else if (login.Username != accounts.First().Username && login.Password == accounts.First().Password)
            {
                AccountController accountController = new AccountController(_mockContext.Object, _mockConfiguration.Object);
                Assert.IsType<BadRequestObjectResult>(accountController.Login(login));
            }
            else if (login.Username == accounts.First().Username && login.Password != accounts.First().Password)
            {
                AccountController accountController = new AccountController(_mockContext.Object, _mockConfiguration.Object);
                Assert.IsType<BadRequestObjectResult>(accountController.Login(login));
            }
        }


    }

}
