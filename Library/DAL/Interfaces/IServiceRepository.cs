using Library.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.DAL
{
    public interface IServiceRepository : IDisposable
    {
        ServiceInfo? GetService(int id);

        List<ServiceInfo> GetServices(int count);

        void AddService(ServiceInfo service);

        void UpdateService(ServiceInfo service);

        void RemoveService(int serviceId);

        void Save();
    }
}