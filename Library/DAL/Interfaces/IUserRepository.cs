﻿using Library.DTO;
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

        List<UserInfo> GetUsers(int count);

        List<UserInfo> GetUsers(string name);

        List<UserInfo> GetUsersBanned(int count);

        void AddUser(UserInfo user);

        void UpdateUser(UserInfo user);

        void Save();
    }
}