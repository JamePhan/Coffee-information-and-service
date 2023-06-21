using Library.DTO;

namespace Library.DAL
{
    public interface IScheduleRepository : IDisposable
    {
        List<ScheduleInfo> GetSchedulesUser(int userId);

        List<ScheduleInfo> GetSchedulesCustomer(int customerId);

        void Save();
    }
}