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
                .Include(schedule => schedule.Event)
                    .ThenInclude(ev => ev.Location)
                .Include(schedule => schedule.Event)
                    .ThenInclude(ev => ev.GroupImage)
                    .ThenInclude(group => group.Image)
                .Include(schedule => schedule.Customer)
                .Where(schedule => schedule.Event.User.UserId.Equals(userId))
                .ToList();
            return _mapper.Map<List<Schedule>, List<ScheduleInfo>>(schedules);
        }

        public List<ScheduleInfo> GetSchedulesCustomer(int customerId)
        {
            List<Schedule> schedules = _context.Schedules
                .Include(schedule => schedule.Event)
                    .ThenInclude(ev => ev.User)
                .Include(schedule => schedule.Event)
                    .ThenInclude(ev => ev.Location)
                .Include(schedule => schedule.Event)
                    .ThenInclude(ev => ev.GroupImage)
                    .ThenInclude(group => group.Image)
                .Include(schedule => schedule.Customer)
                .Where(schedule => schedule.CustomerId.Equals(customerId))
                .ToList();
            return _mapper.Map<List<Schedule>, List<ScheduleInfo>>(schedules);
        }

        public void BookSchedule(ScheduleInfo schedule)
        {
            try
            {
                Event? eventToBook = _context.Events.AsNoTracking().FirstOrDefault(e => e.EventId.Equals(schedule.Event.EventId)) ?? throw new Exception("Can't find event to book!");

                if (eventToBook.SeatCount < schedule.TicketCount) throw new Exception("Event doesn't have enough seat!");

                eventToBook.SeatCount -= schedule.TicketCount;

                _context.Entry(eventToBook).State = EntityState.Modified;
                _context.Schedules.Add(_mapper.Map<ScheduleInfo, Schedule>(schedule));
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void UpdateSchedule(ScheduleInfo schedule)
        {
            Schedule? checkExist = _context.Schedules.AsNoTracking().FirstOrDefault(s => s.ScheduleId.Equals(schedule.ScheduleId));
            if (checkExist != null)
            {
                Event? eventToBook = _context.Events.AsNoTracking().FirstOrDefault(e => e.EventId.Equals(schedule.Event.EventId)) ?? throw new Exception("Can't find event to book!");
                eventToBook.SeatCount += checkExist.TicketCount;

                if (eventToBook.SeatCount < schedule.TicketCount) throw new Exception("Event doesn't have enough seat!");

                eventToBook.SeatCount -= schedule.TicketCount;

                _context.Entry(eventToBook).State = EntityState.Modified;
                checkExist = _mapper.Map<ScheduleInfo, Schedule>(schedule);
                _context.Entry(checkExist).State = EntityState.Modified;
            }
            else
            {
                throw new Exception("Schedule doesn't exist.");
            }
        }

        public void DeleteSchedule(int scheduleId)
        {
            Schedule? checkExist = _context.Schedules.FirstOrDefault(s => s.ScheduleId.Equals(scheduleId));
            if (checkExist != null)
            {
                Event? ev = _context.Events.AsNoTracking().FirstOrDefault(e => e.EventId.Equals(checkExist.EventId)) ?? throw new Exception("Can't find event!");
                ev.SeatCount += checkExist.TicketCount;
                _context.Entry(ev).State = EntityState.Modified;
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