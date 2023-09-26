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
using System.Reflection;

namespace Capstone_UnitTest.Controller
{
    public class Follow_Test
    {
        private readonly Mock<CoffeehouseSystemContext> _mockContext;
        private readonly Mock<IMapper> _mockMapper;

        public Follow_Test()
        {
            _mockContext = new Mock<CoffeehouseSystemContext>();
            _mockMapper = new Mock<IMapper>();
        }

        [Fact]
        public void TC1_ViewListFollowingShopCoffee_Test()
        {
            FollowInfo follow = new FollowInfo();
            follow.FollowingId = 1;

            Test_Follow(follow);
        }

        [Fact]
        public void TC2_ViewListFollowingShopCoffee_Test()
        {
            FollowInfo follow = new FollowInfo();
            follow.FollowingId = 2;
  
            Test_Follow(follow);
        }

        [Fact]
        public void TC3_ViewListFollowingShopCoffee_Test()
        {
            FollowInfo follow = new FollowInfo();
            follow.FollowingId = 3;
   
            Test_Follow(follow);
        }

        [Fact]
        public void TC4_ViewListFollowingShopCoffee_Test()
        {
            FollowInfo follow = new FollowInfo();
            follow = null;
            Test_Follow(follow);
        }


        public void Test_Follow(FollowInfo follow)
        {
            var followings = new List<Following>();

            var mockDBFollowing = new Mock<DbSet<Following>>();
            mockDBFollowing.As<IQueryable<Following>>().Setup(m => m.Provider).Returns(followings.AsQueryable().Provider);
            mockDBFollowing.As<IQueryable<Following>>().Setup(m => m.Expression).Returns(followings.AsQueryable().Expression);
            mockDBFollowing.As<IQueryable<Following>>().Setup(m => m.ElementType).Returns(followings.AsQueryable().ElementType);
            mockDBFollowing.As<IQueryable<Following>>().Setup(m => m.GetEnumerator()).Returns(followings.AsQueryable().GetEnumerator());
            _mockContext.SetupGet(m => m.Followings).Returns(mockDBFollowing.Object);

            var followNew = new Following();
            if (follow == null)
            {
                followNew = null;
                _mockMapper.Setup(m => m.Map<FollowInfo, Following>(It.IsAny<FollowInfo>())).Returns(followNew);
            }
            else
            {
                followNew.FollowingId = 1;
                followNew.UserId = 1;
                followNew.CustomerId = 1;
                _mockMapper.Setup(m => m.Map<FollowInfo, Following>(It.IsAny<FollowInfo>())).Returns(followNew);
            }
            FollowController followController = new FollowController(_mockContext.Object, _mockMapper.Object);

            Assert.IsType<OkResult>(followController.Follow(follow));
            _mockContext.Verify(c => c.Followings.Add(followNew), Times.Once);
            _mockMapper.Verify(c => c.Map<FollowInfo, Following>(It.IsAny<FollowInfo>()), Times.Once);
        }


    }
}
