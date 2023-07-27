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
using Org.BouncyCastle.Asn1.Pkcs;

namespace Capstone_UnitTest.Controller
{
    public class ViewListNews_Test
    {
        private readonly Mock<CoffeehouseSystemContext> _mockContext;
        private readonly Mock<IMapper> _mockMapper;

        public ViewListNews_Test()
        {
            _mockContext = new Mock<CoffeehouseSystemContext>();
            _mockMapper = new Mock<IMapper>();
        }

        [Fact]
        public void TC1_ViewListNews_Test()
        {
            Test_ViewListNews();
        }

        public void Test_ViewListNews()
        {
            var news = new List<News>
            {
                new News { NewsId = 1, UserId = 1, Title = "News 1" },
                new News { NewsId = 2, UserId = 1, Title = "News 1" },
                new News { NewsId = 3, UserId = 2, Title = "News 2" },
                new News { NewsId = 4, UserId = 3, Title = "News 3" },
            };

            var mockDBNew = new Mock<DbSet<News>>();
            mockDBNew.As<IQueryable<News>>().Setup(m => m.Provider).Returns(news.AsQueryable().Provider);
            mockDBNew.As<IQueryable<News>>().Setup(m => m.Expression).Returns(news.AsQueryable().Expression);
            mockDBNew.As<IQueryable<News>>().Setup(m => m.ElementType).Returns(news.AsQueryable().ElementType);
            mockDBNew.As<IQueryable<News>>().Setup(m => m.GetEnumerator()).Returns(news.AsQueryable().GetEnumerator());
            _mockContext.SetupGet(m => m.News).Returns(mockDBNew.Object);

            var newsInfos = new List<NewsInfo>
            {
                new NewsInfo { NewsId = 1, UserId = 1, Title = "News 1" },
                new NewsInfo { NewsId = 2, UserId = 1, Title = "News 1" },
                new NewsInfo { NewsId = 3, UserId = 2, Title = "News 2" },
                new NewsInfo { NewsId = 4, UserId = 3, Title = "News 3" },
            };

            _mockMapper.Setup(m => m.Map<List<News>, List<NewsInfo>>(It.IsAny<List<News>>())).Returns(newsInfos);

            NewsController newController = new NewsController(_mockContext.Object, _mockMapper.Object);

            Assert.IsType<OkObjectResult>(newController.List());
            _mockContext.Verify(c => c.News, Times.Once);
            _mockMapper.Verify(c => c.Map<List<News>, List<NewsInfo>>(It.IsAny<List<News>>()), Times.Once);
        }


    }
}
