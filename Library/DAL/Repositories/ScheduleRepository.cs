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

        public void BookSchedule(ScheduleInfo schedule)
        {
            try
            {
                _context.Schedules.Add(_mapper.Map<ScheduleInfo, Schedule>(schedule));
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void UpdateSchedule(ScheduleInfo scheduleinfo)
        {
            Schedule? checkExist = _context.Schedules.AsNoTracking().FirstOrDefault(schedule => schedule.ScheduleId.Equals(scheduleinfo.ScheduleId));
            if (checkExist != null)
            {
                _context.Entry(_mapper.Map<ScheduleInfo, Schedule>(scheduleinfo)).State = EntityState.Modified;
            }
            else
            {
                throw new Exception("Schedule doesn't exist.");
            }
        }

        public void DeleteSchedule(int scheduleId)
        {
            Schedule? checkExist = _context.Schedules.FirstOrDefault(schedule => schedule.ScheduleId.Equals(scheduleId));
            if (checkExist != null)
            {
                _context.Schedules.Remove(checkExist);
            }
            else
            {
                throw new Exception("Schedule doesn't exist.");
            }
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