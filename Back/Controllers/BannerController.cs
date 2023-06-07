using AutoMapper;
using Library.DAL;
using Library.DTO;
using Library.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Back.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class BannerController : ControllerBase
    {
        private readonly IBannerRepository _banner;

        public BannerController(CoffeehouseSystemContext context, IMapper mapper)
        {
            _banner = new BannerRepository(context, mapper);
        }

        [HttpGet("{count}")]
        public IActionResult List(int count)
        {
            List<BannerInfo> banners = _banner.GetBanners(count);
            if (banners.Count > 0)
            {
                return Ok(banners);
            }
            return NotFound();
        }
    }
}