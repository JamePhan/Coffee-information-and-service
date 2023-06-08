using AutoMapper;
using Library.DTO;
using Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.DAL
{
    public class UserRepository : IUserRepository, IDisposable
    {
        private CoffeehouseSystemContext _context;
        private IMapper _mapper;
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