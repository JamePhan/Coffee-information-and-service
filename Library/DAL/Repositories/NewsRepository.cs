using AutoMapper;
using Library.DTO;
using Library.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

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
            List<News> news = _context.News.Include(group => group.GroupImage).ThenInclude(image => image.Image).Include(user => user.User).ToList();
            return _mapper.Map<List<News>, List<NewsInfo>>(news);
        }

        public NewsInfo? GetNews(int id)
        {
            News? news = _context.News.Include(group => group.GroupImage).ThenInclude(image => image.Image).Include(user => user.User).FirstOrDefault(n => n.NewsId.Equals(id));
            if (news != null)
            {
                return _mapper.Map<News, NewsInfo>(news);
            }
            return null;
        }

        public void CreateNews(NewsInfo news)
        {
            try
            {
                _context.Images.Add(new Image { Image1 = news.ImageUrl });
                _context.SaveChanges();

                int imageId = _context.Images.OrderBy(image => image.ImageId).LastOrDefault().ImageId;

                _context.GroupImages.Add(new GroupImage { ImageId = imageId });
                _context.SaveChanges();

                int groupId = _context.GroupImages.OrderBy(group => group.GroupImageId).LastOrDefault().GroupImageId;

                int userId = _context.Users.FirstOrDefault(user => user.CoffeeShopName.ToLower().Equals(news.CoffeeShopName.ToLower())).UserId;

                News toAdd = _mapper.Map<NewsInfo, News>(news);
                toAdd.GroupImageId = groupId;
                toAdd.UserId = userId;
                toAdd.CreatedDate = DateTime.Now;

                _context.News.Add(toAdd);
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void UpdateNews(NewsInfo news)
        {
            News? checkExist = _context.News.AsNoTracking().FirstOrDefault(n => n.NewsId.Equals(news.NewsId));
            if (checkExist != null)
            {
                try
                {
                    GroupImage? newsGroupImage = _context.GroupImages.AsNoTracking().FirstOrDefault(g => g.GroupImageId.Equals(checkExist.GroupImageId));

                    if (newsGroupImage != null)
                    {
                        Image? newsImage = _context.Images.FirstOrDefault(image => image.ImageId.Equals(newsGroupImage.ImageId));
                        if (newsImage != null) newsImage.Image1 = news.ImageUrl;
                    }

                    int userId = _context.Users.FirstOrDefault(user => user.CoffeeShopName.ToLower().Equals(news.CoffeeShopName.ToLower())).UserId;

                    News toUpdate = _mapper.Map<NewsInfo, News>(news);
                    toUpdate.GroupImageId = newsGroupImage.GroupImageId;
                    toUpdate.UserId = userId;
                    toUpdate.CreatedDate = DateTime.Now;

                    _context.Entry(toUpdate).State = EntityState.Modified;
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