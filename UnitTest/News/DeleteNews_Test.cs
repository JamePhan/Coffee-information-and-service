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
using AngleSharp.Dom;

namespace Capstone_UnitTest.Controller
{
    public class DeleteNews_Test
    {
        private readonly Mock<CoffeehouseSystemContext> _mockContext;
        private readonly Mock<IMapper> _mockMapper;

        public DeleteNews_Test()
        {
            _mockContext = new Mock<CoffeehouseSystemContext>();
            _mockMapper = new Mock<IMapper>();
        }

        [Fact]
        public void TC1_DeleteNews_Test()
        {
            Test_DeleteNews(2);
        }

        [Fact]
        public void TC2_DeleteNews_Test()
        {
            Test_DeleteNews(1);
        }

        [Fact]
        public void TC3_DeleteNews_Test()
        {
            Test_DeleteNews(3);
        }

        [Fact]
        public void TC4_DeleteNews_Test()
        {
            Test_DeleteNews_Fail(7);
        }


        public void Test_DeleteNews(int id)
        {
            var news = new List<News>
            {
                new News { NewsId = 1, UserId = 1, Title = "News 1" },
                new News { NewsId = 2, UserId = 1, Title = "News 1" },
                new News { NewsId = 3, UserId = 2, Title = "News 2" },
                new News { NewsId = 4, UserId = 3, Title = "News 3" },
            };
            News newNew = new News();
            var mockDBNew = new Mock<DbSet<News>>();
            mockDBNew.As<IQueryable<News>>().Setup(m => m.Provider).Returns(news.AsQueryable().Provider);
            mockDBNew.As<IQueryable<News>>().Setup(m => m.Expression).Returns(news.AsQueryable().Expression);
            mockDBNew.As<IQueryable<News>>().Setup(m => m.ElementType).Returns(news.AsQueryable().ElementType);
            mockDBNew.As<IQueryable<News>>().Setup(m => m.GetEnumerator()).Returns(news.AsQueryable().GetEnumerator());
            mockDBNew.Setup(x => x.Remove(It.IsAny<News>())).Callback<News>((entity) => news.Remove(entity));

            _mockContext.SetupGet(m => m.News).Returns(mockDBNew.Object);

            NewsController newController = new NewsController(_mockContext.Object, _mockMapper.Object);

            Assert.IsType<OkResult>(newController.Delete(id));
            _mockContext.Verify(c => c.News, Times.Exactly(2));
            _mockContext.Verify(c => c.SaveChanges(), Times.Once);
            Assert.Equal(3, _mockContext.Object.News.Count());
        }

        public void Test_DeleteNews_Fail(int id)
        {
            var news = new List<News>
            {
                new News { NewsId = 1, UserId = 1, Title = "News 1" },
                new News { NewsId = 2, UserId = 1, Title = "News 1" },
                new News { NewsId = 3, UserId = 2, Title = "News 2" },
                new News { NewsId = 4, UserId = 3, Title = "News 3" },
            };
            News newNew = new News();
            var mockDBNew = new Mock<DbSet<News>>();
            mockDBNew.As<IQueryable<News>>().Setup(m => m.Provider).Returns(news.AsQueryable().Provider);
            mockDBNew.As<IQueryable<News>>().Setup(m => m.Expression).Returns(news.AsQueryable().Expression);
            mockDBNew.As<IQueryable<News>>().Setup(m => m.ElementType).Returns(news.AsQueryable().ElementType);
            mockDBNew.As<IQueryable<News>>().Setup(m => m.GetEnumerator()).Returns(news.AsQueryable().GetEnumerator());
            mockDBNew.Setup(x => x.Remove(It.IsAny<News>())).Callback<News>((entity) => news.Remove(entity));

            _mockContext.SetupGet(m => m.News).Returns(mockDBNew.Object);

            NewsController newController = new NewsController(_mockContext.Object, _mockMapper.Object);

            Assert.IsType<BadRequestObjectResult>(newController.Delete(id));
            _mockContext.Verify(c => c.News, Times.Exactly(1));
            _mockContext.Verify(c => c.SaveChanges(), Times.Never);
            Assert.Equal(4, _mockContext.Object.News.Count());
        }
    }
}
