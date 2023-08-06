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

        NewsInfo? GetNews(int id);

        void CreateNews(NewsInfo news);

        void UpdateNews(NewsInfo news);

        void DeleteNews(int id);

        void Save();
    }
}