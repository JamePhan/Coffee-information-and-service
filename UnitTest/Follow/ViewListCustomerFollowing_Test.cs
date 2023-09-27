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
using AutoMapper;
using Org.BouncyCastle.Asn1.Pkcs;
using Castle.Core.Resource;

namespace Capstone_UnitTest.Controller
{
    public class ViewListCustomerFollowing_Test
    {
        private readonly Mock<CoffeehouseSystemContext> _mockContext;
        private readonly Mock<IMapper> _mockMapper;

        public ViewListCustomerFollowing_Test()
        {
            _mockContext = new Mock<CoffeehouseSystemContext>();
            _mockMapper = new Mock<IMapper>();
        }

        [Fact]
        public void TC1_ViewListCustomerFollowing_Test()
        {
            Test_ViewListCustomerFollowing_Success(1);
        }

        [Fact]
        public void TC2_ViewListCustomerFollowing_Test()
        {
            Test_ViewListCustomerFollowing_Success(2);
        }

        [Fact]
        public void TC3_ViewListCustomerFollowing_Test()
        {
            Test_ViewListCustomerFollowing_Success(3);
        }

        [Fact]
        public void TC4_ViewListCustomerFollowing_Test()
        {
            Test_ViewListCustomerFollowing_Success(0);
        }

        [Fact]
        public void TC5_ViewListCustomerFollowing_Test()
        {
            Test_ViewListCustomerFollowing_Success(9);
        }

        [Fact]
        public void TC6_ViewListCustomerFollowing_Test()
        {
            Test_ViewListCustomerFollowing_Fail(1);
        }

        public void Test_ViewListCustomerFollowing_Success(int id)
        {
            var followings = new List<Following>
            {
                new Following { FollowingId = 1, UserId = 1, CustomerId = 1 },
                new Following { FollowingId = 2, UserId = 2, CustomerId = 2 },
                new Following { FollowingId = 3, UserId = 1, CustomerId = 1 },
                new Following { FollowingId = 4, UserId = 2, CustomerId = 1 },
                new Following { FollowingId = 5, UserId = 1, CustomerId = 3 },
                new Following { FollowingId = 5, UserId = 1, CustomerId = 3 },
            };
            var mockDBFollowing = new Mock<DbSet<Following>>();
            mockDBFollowing.As<IQueryable<Following>>().Setup(m => m.Provider).Returns(followings.AsQueryable().Provider);
            mockDBFollowing.As<IQueryable<Following>>().Setup(m => m.Expression).Returns(followings.AsQueryable().Expression);
            mockDBFollowing.As<IQueryable<Following>>().Setup(m => m.ElementType).Returns(followings.AsQueryable().ElementType);
            mockDBFollowing.As<IQueryable<Following>>().Setup(m => m.GetEnumerator()).Returns(followings.AsQueryable().GetEnumerator());
            _mockContext.SetupGet(m => m.Followings).Returns(mockDBFollowing.Object);

            var followInfos = new List<FollowInfo>
            {
                new FollowInfo { FollowingId = 1,},
                new FollowInfo { FollowingId = 2,},
                new FollowInfo { FollowingId = 3, },
                new FollowInfo { FollowingId = 4, },
                new FollowInfo { FollowingId = 5,},
                new FollowInfo { FollowingId = 5,},
            };
            _mockMapper.Setup(m => m.Map<List<Following>, List<FollowInfo>>(It.IsAny<List<Following>>())).Returns(followInfos);

            FollowController followController = new FollowController(_mockContext.Object, _mockMapper.Object);

            Assert.IsType<OkObjectResult>(followController.CustomerList(id));
            _mockContext.Verify(c => c.Followings, Times.Once);
            _mockMapper.Verify(c => c.Map<List<Following>, List<FollowInfo>>(It.IsAny<List<Following>>()), Times.Once);
        }

        public void Test_ViewListCustomerFollowing_Fail(int id)
        {
            var followings = new List<Following>
            {

            };
            var mockDBFollowing = new Mock<DbSet<Following>>();
            mockDBFollowing.As<IQueryable<Following>>().Setup(m => m.Provider).Returns(followings.AsQueryable().Provider);
            mockDBFollowing.As<IQueryable<Following>>().Setup(m => m.Expression).Returns(followings.AsQueryable().Expression);
            mockDBFollowing.As<IQueryable<Following>>().Setup(m => m.ElementType).Returns(followings.AsQueryable().ElementType);
            mockDBFollowing.As<IQueryable<Following>>().Setup(m => m.GetEnumerator()).Returns(followings.AsQueryable().GetEnumerator());
            _mockContext.SetupGet(m => m.Followings).Returns(mockDBFollowing.Object);

            var followInfos = new List<FollowInfo>
            {

            };
            _mockMapper.Setup(m => m.Map<List<Following>, List<FollowInfo>>(It.IsAny<List<Following>>())).Returns(followInfos);

            FollowController followController = new FollowController(_mockContext.Object, _mockMapper.Object);

            Assert.IsType<NotFoundResult>(followController.CustomerList(id));
            _mockContext.Verify(c => c.Followings, Times.Once);
            _mockMapper.Verify(c => c.Map<List<Following>, List<FollowInfo>>(It.IsAny<List<Following>>()), Times.Once);
        }

    }
}
