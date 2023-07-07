using Library.DTO;

namespace Library.DAL
{
    public interface IEventRepository : IDisposable
    {
        List<EventInfo> GetEvents(int count);

        List<EventInfo> GetLastest(int count);

        void AddEvent(EventInfo eventInfo);

        void Save();
    }
}