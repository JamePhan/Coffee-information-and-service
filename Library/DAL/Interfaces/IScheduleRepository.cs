using Library.DTO;

namespace Library.DAL
{
    public interface IScheduleRepository : IDisposable
    {
        List<ScheduleInfo> GetSchedulesUser(int userId);

        List<ScheduleInfo> GetSchedulesCustomer(int customerId);

        void BookSchedule(ScheduleInfo schedule);

        void UpdateSchedule(ScheduleInfo schedule);

        void DeleteSchedule(int scheduleId);

        void Save();
    }
}