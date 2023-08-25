using Library.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.DAL
{
    public interface IFollowRepository : IDisposable
    {
        void AddFollow(FollowInfo follow);

        List<CustomerFollowInfo> GetFollowingUsers(int customerId);

        List<FollowInfo> GetFollowingCustomers(int userId);

        void RemoveFollow(FollowInfo follow);

        void Save();
    }
}