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
    public class DeleteBanner_Test
    {
        private readonly Mock<CoffeehouseSystemContext> _mockContext;
        private readonly Mock<IMapper> _mockMapper;

        public DeleteBanner_Test()
        {
            _mockContext = new Mock<CoffeehouseSystemContext>();
            _mockMapper = new Mock<IMapper>();
        }

        [Fact]
        public void TC1_DeleteBanner_Test()
        {
            Test_DeleteBanner_Success(4);
        }

        [Fact]
        public void TC2_DeleteBanner_Test()
        {
            Test_DeleteBanner_Success(3);
        }

        [Fact]
        public void TC3_DeleteBanner_Test()
        {
            Test_DeleteBanner_Fail(7);
        }

        [Fact]
        public void TC4_DeleteBanner_Test()
        {
            Test_DeleteBanner_Fail(9);
        }

        public void Test_DeleteBanner_Success(int bannerID)
        {
            var banners = new List<Banner>
            {
                new Banner { BannerId = 1, UserId = 1 },
                new Banner { BannerId = 2, UserId = 1 },
                new Banner { BannerId = 3, UserId = 1 },
                new Banner { BannerId = 4, UserId = 1 },
                new Banner { BannerId = 5, UserId = 1 }
            };
            var mockDBBanner = new Mock<DbSet<Banner>>();
            mockDBBanner.As<IQueryable<Banner>>().Setup(m => m.Provider).Returns(banners.AsQueryable().Provider);
            mockDBBanner.As<IQueryable<Banner>>().Setup(m => m.Expression).Returns(banners.AsQueryable().Expression);
            mockDBBanner.As<IQueryable<Banner>>().Setup(m => m.ElementType).Returns(banners.AsQueryable().ElementType);
            mockDBBanner.As<IQueryable<Banner>>().Setup(m => m.GetEnumerator()).Returns(banners.AsQueryable().GetEnumerator());
            _mockContext.Setup(c => c.Banners).Returns(mockDBBanner.Object);
            BannerController bannerController = new BannerController(_mockContext.Object, _mockMapper.Object);

            Assert.IsType<OkResult>(bannerController.Delete(bannerID));
            _mockContext.Verify(c => c.Banners.Remove(It.IsAny<Banner>()), Times.Once());
        }

        public void Test_DeleteBanner_Fail(int bannerID)
        {
            var banners = new List<Banner>
            {
                new Banner { BannerId = 1, UserId = 1 },
                new Banner { BannerId = 2, UserId = 1 },
                new Banner { BannerId = 3, UserId = 1 },
                new Banner { BannerId = 4, UserId = 1 },
                new Banner { BannerId = 5, UserId = 1 }
            };
            var mockDBBanner = new Mock<DbSet<Banner>>();
            mockDBBanner.As<IQueryable<Banner>>().Setup(m => m.Provider).Returns(banners.AsQueryable().Provider);
            mockDBBanner.As<IQueryable<Banner>>().Setup(m => m.Expression).Returns(banners.AsQueryable().Expression);
            mockDBBanner.As<IQueryable<Banner>>().Setup(m => m.ElementType).Returns(banners.AsQueryable().ElementType);
            mockDBBanner.As<IQueryable<Banner>>().Setup(m => m.GetEnumerator()).Returns(banners.AsQueryable().GetEnumerator());
            _mockContext.Setup(c => c.Banners).Returns(mockDBBanner.Object);
            BannerController bannerController = new BannerController(_mockContext.Object, _mockMapper.Object);

            Assert.IsType<BadRequestResult>(bannerController.Delete(bannerID));
            _mockContext.Verify(c => c.Banners.Remove(It.IsAny<Banner>()), Times.Never());
        }
    }
}
