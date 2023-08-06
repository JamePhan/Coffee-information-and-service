using Library.DTO;

namespace Library.DAL.Interfaces
{
    public interface IWaitingRepository : IDisposable
    {
        List<WaitingInfo> GetWaitings();

        WaitingInfo? GetWaiting(int id);

        void AddWaiting(int id);

        void RemoveWaiting(int id);

        void Save();
    }
}