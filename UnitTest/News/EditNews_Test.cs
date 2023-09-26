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
    public class EditNews_Test
    {
        private readonly Mock<CoffeehouseSystemContext> _mockContext;
        private readonly Mock<IMapper> _mockMapper;

        public EditNews_Test()
        {
            _mockContext = new Mock<CoffeehouseSystemContext>();
            _mockMapper = new Mock<IMapper>();
        }

        [Fact]
        public void TC1_EditNews_Test()
        {
            var newsNew = new NewsInfo();
            newsNew.NewsId = 1;
   
            newsNew.Title = "News 5";
            Test_EditNews(newsNew);
        }

        [Fact]
        public void TC2_EditNews_Test()
        {
            var newsNew = new NewsInfo();
            newsNew.NewsId = 1;
          
            newsNew.Title = "News 3";
            Test_EditNews(newsNew);
        }

        [Fact]
        public void TC3_EditNews_Test()
        {
            var newsNew = new NewsInfo();
            newsNew.NewsId = 3;
         
            newsNew.Title = "News 2";
            Test_EditNews(newsNew);
        }

        [Fact]
        public void TC4_EditNews_Test()
        {
            var newsNew = new NewsInfo();
            newsNew.NewsId = 2;
 
            newsNew.Title = "News 6";
            Test_EditNews(newsNew);
        }

        public void Test_EditNews(NewsInfo newInfo)
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
            mockDBNew.Setup(x => x.Add(It.IsAny<News>())).Callback<News>(news.Add);
            _mockContext.SetupGet(m => m.News).Returns(mockDBNew.Object);

            var newsNew = new News();
            if (newInfo == null)
            {
                newsNew = null;
            }
            else
            {
                newsNew.NewsId = newInfo.NewsId;
     
                newsNew.Title = newInfo.Title;
            }

            _mockMapper.Setup(m => m.Map<NewsInfo, News>(It.IsAny<NewsInfo>())).Returns(newsNew);

            NewsController newController = new NewsController(_mockContext.Object, _mockMapper.Object);

            var result = newController.Update(newInfo);
            _mockContext.Verify(c => c.News, Times.Exactly(1));
            _mockMapper.Verify(c => c.Map<NewsInfo, News>(It.IsAny<NewsInfo>()), Times.Once);

            //Assert.IsType<OkObjectResult>(result); 
            //_mockContext.Verify(c => c.SaveChanges(), Times.Once);
            //Assert.Equal(5, _mockContext.Object.News.Count());

        }
    }
}
