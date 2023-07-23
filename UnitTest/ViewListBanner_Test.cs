using AutoMapper;
using Back.Controllers;
using Library.DTO;
using Library.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace Capstone_UnitTest.Controller
{
    public class ViewListBanner_Test
    {
        private readonly Mock<CoffeehouseSystemContext> _mockContext;
        private readonly Mock<IMapper> _mockMapper;

        public ViewListBanner_Test()
        {
            _mockContext = new Mock<CoffeehouseSystemContext>();
            _mockMapper = new Mock<IMapper>();
        }

        [Fact]
        public void TC1_ViewListBanner_Test()
        {
            Test_ViewListBanner_HaveData(5);
        }

        [Fact]
        public void TC2_ViewListBanner_Test()
        {
            Test_ViewListBanner_NoData(5);
        }

        public void Test_ViewListBanner_HaveData(int count)
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
            _mockContext.SetupGet(m => m.Banners).Returns(mockDBBanner.Object);

            var bannerInfos = new List<BannerInfo>
            {
                new BannerInfo { BannerId = 1, UserId = 1 },
                new BannerInfo { BannerId = 2, UserId = 1 },
                new BannerInfo { BannerId = 3, UserId = 1 },
                new BannerInfo { BannerId = 4, UserId = 1 },
                new BannerInfo { BannerId = 5, UserId = 1 }
            };
            _mockMapper.Setup(m => m.Map<List<Banner>, List<BannerInfo>>(It.IsAny<List<Banner>>())).Returns(bannerInfos);

            BannerController bannerController = new BannerController(_mockContext.Object, _mockMapper.Object);
            Assert.IsType<OkObjectResult>(bannerController.List(count));
        }

        public void Test_ViewListBanner_NoData(int count)
        {
            var banners = new List<Banner>
            {

            };
            var mockDBBanner = new Mock<DbSet<Banner>>();
            mockDBBanner.As<IQueryable<Banner>>().Setup(m => m.Provider).Returns(banners.AsQueryable().Provider);
            mockDBBanner.As<IQueryable<Banner>>().Setup(m => m.Expression).Returns(banners.AsQueryable().Expression);
            mockDBBanner.As<IQueryable<Banner>>().Setup(m => m.ElementType).Returns(banners.AsQueryable().ElementType);
            mockDBBanner.As<IQueryable<Banner>>().Setup(m => m.GetEnumerator()).Returns(banners.AsQueryable().GetEnumerator());
            _mockContext.SetupGet(m => m.Banners).Returns(mockDBBanner.Object);

            var bannerInfos = new List<BannerInfo>
            {

            };
            _mockMapper.Setup(m => m.Map<List<Banner>, List<BannerInfo>>(It.IsAny<List<Banner>>())).Returns(bannerInfos);

            BannerController bannerController = new BannerController(_mockContext.Object, _mockMapper.Object);
            Assert.IsType<NotFoundResult>(bannerController.List(count));
        }


    }
}
