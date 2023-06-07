using Library.DTO;

namespace Library.DAL
{
    public interface IEventRepository
    {
        List<EventInfo> GetEvents();
    }
}