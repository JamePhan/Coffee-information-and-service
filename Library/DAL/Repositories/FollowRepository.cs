using AutoMapper;
using Library.DTO;
using Library.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.DAL
{
    public class FollowRepository : IFollowRepository
    {
        private readonly CoffeehouseSystemContext _context;
        private readonly IMapper _mapper;
        private bool _disposed = false;

        public FollowRepository(CoffeehouseSystemContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public void AddFollow(FollowInfo follow)
        {
            Following? checkExist = _context.Followings.FirstOrDefault(following => following.CustomerId == follow.Customer.CustomerId && following.UserId == follow.User.UserId);
            if (checkExist == null)
            {
                try
                {
                    _context.Followings.Add(_mapper.Map<FollowInfo, Following>(follow));
                }
                catch (SqlException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            else
            {
                throw new Exception("Already followed!");
            }
        }

        public List<FollowInfo> GetFollowingUsers(int customerId)
        {
            List<Following> followings = _context.Followings
                .Include(follow => follow.User)
                .Include(follow => follow.Customer)
                .Where(follow => follow.CustomerId == customerId).ToList();
            return _mapper.Map<List<Following>, List<FollowInfo>>(followings);
        }

        public List<FollowInfo> GetFollowingCustomers(int userId)
        {
            List<Following> followings = _context.Followings
                .Include(follow => follow.User)
                .Include(follow => follow.Customer)
                .Where(follow => follow.UserId == userId).ToList();
            return _mapper.Map<List<Following>, List<FollowInfo>>(followings);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed && disposing)
            {
                _context.Dispose();
            }
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}