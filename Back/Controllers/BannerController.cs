﻿using AutoMapper;
using Library.DAL;
using Library.DTO;
using Library.Models;
using Microsoft.AspNetCore.Authorization;
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

        [HttpGet]
        public IActionResult List()
        {
            List<BannerInfo> banners = _banner.GetBanners();
            if (banners.Count > 0)
            {
                return Ok(banners);
            }
            return NotFound();
        }

        //[Authorize(Roles = "User")]
        [HttpPost]
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

        [HttpGet]
        public IActionResult Detail(int id)
        {
            BannerInfo? banner = _banner.DetailBanner(id);
            if (banner != null)
            {
                return Ok(banner);
            }
            return NotFound();
        }

        //[Authorize(Roles = "User")]
        [HttpPut]
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

        //[Authorize(Roles = "User")]
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