using AutoMapper;
using Library.DTO;
using Library.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.DAL
{
    public class NewsRepository : INewsRepository
    {
        private readonly CoffeehouseSystemContext _context;
        private readonly IMapper _mapper;
        private bool _disposed = false;

        public NewsRepository(CoffeehouseSystemContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public List<NewsInfo> GetNews()
        {
            List<Event> news = _context.Events.Include(events => events.Schedules).ToList();
            return _mapper.Map<List<Event>, List<NewsInfo>>(news);
        }

        public List<NewsInfo> GetNewsUser(int userId)
        {
            List<Event> news = _context.Events.Include(events => events.Schedules).Where(ev => ev.UserId.Equals(userId)).ToList();
            return _mapper.Map<List<Event>, List<NewsInfo>>(news);
        }

        public List<NewsInfo> GetNewsCustomer(int customerId)
        {
            List<NewsInfo> news = new();

            //Get all the IDs of users being followed by current customer
            List<int> following = _context.Followings
                .Where(follow => follow.CustomerId.Equals(customerId))
                .Select(follow => follow.UserId)
                .Where(id => id.HasValue)
                .Select(id => id.Value)
                .ToList();

            for (int i = 0; i < following.Count; i++)
            {
                news.AddRange(GetNewsUser(following[i]));
            }

            return news;
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