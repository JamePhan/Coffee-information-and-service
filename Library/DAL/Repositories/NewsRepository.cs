using AutoMapper;
using Library.DTO;
using Library.Models;
using Microsoft.Data.SqlClient;

namespace Library.DAL
{
    public class NewsRepository : INewsRepository
    {
        private readonly CoffeehouseSystemContext _context;
        private readonly IMapper _mapper;
        private bool _disposed = false;

        public NewsRepository(CoffeehouseSystemContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public List<NewsInfo> GetNews()
        {
            List<News> news = _context.News.ToList();
            return _mapper.Map<List<News>, List<NewsInfo>>(news);
        }

        public void CreateNews(NewsInfo news)
        {
            try
            {
                _context.News.Add(_mapper.Map<NewsInfo, News>(news));
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void UpdateNews(NewsInfo news)
        {
            News? checkExist = _context.News.FirstOrDefault(n => n.NewsId.Equals(news.NewsId));
            if (checkExist != null)
            {
                try
                {
                    _context.Entry(_mapper.Map<NewsInfo, News>(news)).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                }
                catch (SqlException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            else
            {
                throw new Exception("News doesn't exist!");
            }
        }

        public void DeleteNews(int id)
        {
            News? checkExist = _context.News.FirstOrDefault(n => n.NewsId.Equals(id));
            if (checkExist != null)
            {
                try
                {
                    _context.News.Remove(checkExist);
                }
                catch (SqlException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            else
            {
                throw new Exception("News doesn't exist!");
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