using AutoMapper;
using Library.DTO;
using Library.Models;

namespace Library.DAL
{
    public class BannerRepository : IBannerRepository, IDisposable
    {
        private CoffeehouseSystemContext _context;
        private IMapper _mapper;
        private bool _disposed = false;

        public BannerRepository(CoffeehouseSystemContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public List<BannerInfo> GetBanners(int count)
        {
            List<Banner> banners;
            if (count > 0)
            {
                banners = _context.Banners.Take(count).ToList();
            }
            else
            {
                banners = _context.Banners.ToList();
            }
            return _mapper.Map<List<Banner>, List<BannerInfo>>(banners);
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