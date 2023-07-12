using AutoMapper;
using Library.DTO;
using Library.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.DAL
{
    public class UserRepository : IUserRepository
    {
        private readonly CoffeehouseSystemContext _context;
        private readonly IMapper _mapper;
        private bool _disposed = false;

        public UserRepository(CoffeehouseSystemContext context)
        {
            _context = context;
        }

        public UserRepository(CoffeehouseSystemContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public User? GetUserByEmail(string email)
        {
            return _context.Users.FirstOrDefault(u => u.Email.Equals(email));
        }

        public User? GetUserByAccountId(int accountId)
        {
            return _context.Users.FirstOrDefault(u => u.AccountId.Equals(accountId));
        }

        public List<UserInfo> GetUsers(int count)
        {
            List<User> users;
            if (count > 0)
            {
                users = _context.Users.Take(count).ToList();
            }
            else
            {
                users = _context.Users.ToList();
            }

            return _mapper.Map<List<User>, List<UserInfo>>(users);
        }

        public List<UserInfo> GetUsers(string name)
        {
            List<User> users = _context.Users.Where(u => u.CoffeeShopName.Contains(name.Trim(), StringComparison.OrdinalIgnoreCase)).ToList();
            return _mapper.Map<List<User>, List<UserInfo>>(users);
        }

        public List<UserInfo> GetUsersBanned(int count)
        {
            List<User> users;
            if (count > 0)
            {
                users = _context.Users
                    .Include(user => user.Account)
                    .Where(user => user.Account.IsBanned == true)
                    .Take(count).ToList();
            }
            else
            {
                users = _context.Users
                    .Include(user => user.Account)
                    .Where(user => user.Account.IsBanned == true)
                    .ToList();
            }

            return _mapper.Map<List<User>, List<UserInfo>>(users);
        }

        public void UpdateUser(UserInfo user)
        {
            User? checkExist = _context.Users.FirstOrDefault(u => u.UserId.Equals(user.UserId));
            if (checkExist != null)
            {
                try
                {
                    checkExist = _mapper.Map<UserInfo, User>(user);
                    _context.Entry(checkExist).State = EntityState.Modified;
                }
                catch (SqlException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            else
            {
                throw new Exception("User doesn't exist!");
            }
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed && disposing)
            {
                _context.Dispose();
            }
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}