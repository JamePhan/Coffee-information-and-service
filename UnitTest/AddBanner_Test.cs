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
    public class AddBanner_Test
    {
        private readonly Mock<CoffeehouseSystemContext> _mockContext;
        private readonly Mock<IMapper> _mockMapper;

        public AddBanner_Test()
        {
            _mockContext = new Mock<CoffeehouseSystemContext>();
            _mockMapper = new Mock<IMapper>();
        }

        [Fact]
        public void TC5_AddBanner_Test()
        {
            BannerInfo banner = new BannerInfo();
            banner = null;
            Test_AddBanner(banner);
        }


        [Fact]
        public void TC1_AddBanner_Test()
        {
            BannerInfo banner = new BannerInfo();
            banner.BannerId = 1;
            banner.UserId = 1;
            banner.ImageUrl = "exampleimage.com";
            Test_AddBanner(banner);
        }

        [Fact]
        public void TC2_AddBanner_Test()
        {
            BannerInfo banner = new BannerInfo();
            banner.BannerId = 2;
            banner.UserId = 1;
            banner.ImageUrl = "exampleimage1.com";
            Test_AddBanner(banner);
        }

        [Fact]
        public void TC3_AddBanner_Test()
        {
            BannerInfo banner = new BannerInfo();
            banner.BannerId = 3;
            banner.UserId = 1;
            banner.ImageUrl = "exampleimage2.com";
            Test_AddBanner(banner);
        }

        [Fact]
        public void TC4_AddBanner_Test()
        {
            BannerInfo banner = new BannerInfo();
            banner = null;
            Test_AddBanner(banner);
        }

        
        public void Test_AddBanner(BannerInfo banner)
        {
            var banners = new List<Banner>();
            var mockDBBanner = new Mock<DbSet<Banner>>();
            mockDBBanner.As<IQueryable<Banner>>().Setup(m => m.Provider).Returns(banners.AsQueryable().Provider);
            mockDBBanner.As<IQueryable<Banner>>().Setup(m => m.Expression).Returns(banners.AsQueryable().Expression);
            mockDBBanner.As<IQueryable<Banner>>().Setup(m => m.ElementType).Returns(banners.AsQueryable().ElementType);
            mockDBBanner.As<IQueryable<Banner>>().Setup(m => m.GetEnumerator()).Returns(banners.AsQueryable().GetEnumerator());
            _mockContext.Setup(m => m.Banners).Returns(mockDBBanner.Object);
            mockDBBanner.Setup(x => x.Add(It.IsAny<Banner>())).Callback<Banner>(banners.Add);
            Banner bannerNew = new Banner();
            if (banner == null)
            {
                bannerNew = null;
                _mockMapper.Setup(m => m.Map<BannerInfo, Banner>(It.IsAny<BannerInfo>())).Returns(bannerNew);
            }
            else
            {
                bannerNew.BannerId = banner.BannerId;
                bannerNew.UserId = banner.UserId;
                bannerNew.ImageUrl = banner.ImageUrl;
                _mockMapper.Setup(m => m.Map<BannerInfo, Banner>(It.IsAny<BannerInfo>())).Returns(bannerNew);
            }
            BannerController bannerController = new BannerController(_mockContext.Object, _mockMapper.Object);

                Assert.IsType<OkResult>(bannerController.Add(banner));
                _mockContext.Verify(c => c.Banners.Add(bannerNew), Times.Once);
            Assert.Equal(1, _mockContext.Object.Banners.Count());

        }



    }
}
