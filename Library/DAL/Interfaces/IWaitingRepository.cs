using Library.DTO;

namespace Library.DAL.Interfaces
{
    public interface IWaitingRepository : IDisposable
    {
        List<WaitingInfo> GetWaitings();

        void AddWaiting(CustomerInfo customer);

        void RemoveWaiting(int id);

        void Save();
    }
}