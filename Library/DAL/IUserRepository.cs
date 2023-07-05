using Library.DTO;
using Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.DAL
{
    public interface IUserRepository : IDisposable
    {
        User? GetUserByEmail(string email);

        List<UserInfo> GetUsers(int count);

        List<UserInfo> GetUsers(string name);

        List<UserInfo> GetUsersBanned(int count);

        void UpdateUser(UserInfo user);

        void Save();
    }
}