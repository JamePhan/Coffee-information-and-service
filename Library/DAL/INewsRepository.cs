using Library.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.DAL
{
    public interface INewsRepository : IDisposable
    {
        List<NewsInfo> GetNews();

        List<NewsInfo> GetNewsUser(int userId);

        List<NewsInfo> GetNewsCustomer(int customerId);

        void Save();
    }
}