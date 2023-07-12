using Library.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.DAL
{
    public class AdminRepository : IAdminRepository
    {
        private readonly CoffeehouseSystemContext _context;
        private bool _disposed = false;

        public AdminRepository(CoffeehouseSystemContext context)
        {
            _context = context;
        }

        public Admin? GetAdminByAccountId(int accountId)
        {
            return _context.Admins.FirstOrDefault(admin => admin.AccountId.Equals(accountId));
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