using AutoMapper;
using Library.DTO;
using Library.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Library.DAL
{
    public class BannerRepository : IBannerRepository
    {
        private CoffeehouseSystemContext _context;
        private IMapper _mapper;
        private bool _disposed = false;

        public BannerRepository(CoffeehouseSystemContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public void AddBanner(BannerInfo banner)
        {
            try
            {
                _context.Banners.Add(_mapper.Map<BannerInfo, Banner>(banner));
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void UpdateBanner(BannerInfo banner)
        {
            Banner? checkExist = _context.Banners.AsNoTracking().FirstOrDefault(bnn => bnn.BannerId == banner.BannerId);
            if (checkExist != null)
            {
                try
                {
                    _context.Entry(_mapper.Map<BannerInfo, Banner>(banner)).State = EntityState.Modified;
                }
                catch (SqlException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            else
            {
                throw new Exception("Banner doesn't exist!");
            }
        }

        public void RemoveBanner(int bannerId)
        {
            Banner? checkExist = _context.Banners.FirstOrDefault(bnn => bnn.BannerId == bannerId);
            if (checkExist != null)
            {
                try
                {
                    _context.Banners.Remove(checkExist);
                }
                catch (SqlException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            else
            {
                throw new Exception("Banner doesn't exist!");
            }
        }

        public List<BannerInfo> GetBanners()
        {
            List<Banner> banners;

            banners = _context.Banners.ToList();

            return _mapper.Map<List<Banner>, List<BannerInfo>>(banners);
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