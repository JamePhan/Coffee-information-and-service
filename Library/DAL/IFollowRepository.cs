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

        List<FollowInfo> GetFollowingUsers(int customerId);

        void Save();
    }
}