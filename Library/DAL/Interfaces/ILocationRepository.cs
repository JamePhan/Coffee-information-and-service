using Library.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.DAL
{
    public interface ILocationRepository : IDisposable
    {
        List<LocationInfo> GetLocations(int count);

        void AddLocation(LocationInfo location);

        void RemoveLocation(int locationId);

        void Save();
    }
}