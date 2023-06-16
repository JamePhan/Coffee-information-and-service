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
    public class BannerController : Controller
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

        [HttpPut]
        public IActionResult Add(BannerInfo banner)
        {
            try
            {
                _banner.AddBanner(banner);
                _banner.Save();
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPatch]
        public IActionResult Update(BannerInfo banner)
        {
            try
            {
                _banner.UpdateBanner(banner);
                _banner.Save();
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _banner.RemoveBanner(id);
                _banner.Save();
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        protected override void Dispose(bool disposing)
        {
            _banner.Dispose();
            base.Dispose(disposing);
        }
    }
}