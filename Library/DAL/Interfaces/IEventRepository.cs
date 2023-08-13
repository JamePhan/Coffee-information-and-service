using Library.DTO;

namespace Library.DAL
{
    public interface IEventRepository : IDisposable
    {
        List<EventInfo> GetEvents();

        EventInfo? GetEvent(int id);

        void AddEvent(EventInfo eventInfo);

        void Save();
    }
}