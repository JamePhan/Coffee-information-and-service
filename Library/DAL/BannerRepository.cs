using AutoMapper;
using Library.DTO;
using Library.Models;

namespace Library.DAL
{
    public class BannerRepository : IBannerRepository
    {
        private CoffeehouseSystemContext _context;
        private IMapper _mapper;

        public BannerRepository(CoffeehouseSystemContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public List<BannerInfo> GetBanners(int count)
        {
            List<Banner> banners = new();
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
    }
}