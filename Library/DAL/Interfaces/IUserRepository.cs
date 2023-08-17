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

        User? GetUserByAccountId(int accountId);

        List<UserInfo> GetUsers();

        List<UserInfo> GetUsers(string name);

        List<UserInfo> GetUsersBanned();

        UserInfo? GetUser(int id);

        void AddUser(UserInfo user, int AccountId);

        void UpdateUser(UserInfo user, int AccountId);

        void Save();
    }
}