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
    public class ScheduleRepository : IScheduleRepository
    {
        private readonly CoffeehouseSystemContext _context;
        private readonly IMapper _mapper;
        private bool _disposed = false;

        public ScheduleRepository(CoffeehouseSystemContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public List<ScheduleInfo> GetSchedulesUser(int userId)
        {
            List<Schedule> schedules = _context.Schedules
                .Include(schedule => schedule.Event)
                .ThenInclude(ev => ev.User)
                .Where(schedule => schedule.Event.User.UserId.Equals(userId))
                .ToList();
            return _mapper.Map<List<Schedule>, List<ScheduleInfo>>(schedules);
        }

        public List<ScheduleInfo> GetSchedulesCustomer(int customerId)
        {
            List<Schedule> schedules = _context.Schedules
                .Include(schedule => schedule.Event)
                .ThenInclude(ev => ev.User)
                .Where(schedule => schedule.CustomerId.Equals(customerId))
                .ToList();
            return _mapper.Map<List<Schedule>, List<ScheduleInfo>>(schedules);
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