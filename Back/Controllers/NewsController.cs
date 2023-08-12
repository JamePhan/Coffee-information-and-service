using AutoMapper;
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
    public class NewsController : Controller
    {
        private readonly INewsRepository _news;

        public NewsController(CoffeehouseSystemContext context, IMapper mapper)
        {
            _news = new NewsRepository(context, mapper);
        }

        [HttpGet]
        public IActionResult List()
        {
            List<NewsInfo> news = _news.GetNews();
            return Ok(news);
        }

        [HttpGet("{id}")]
        public IActionResult Detail(int id)
        {
            NewsInfo? news = _news.GetNews(id);
            if (news != null)
            {
                return Ok(news);
            }
            return NotFound();
        }

        [Authorize(Roles = "User")]
        [HttpPut]
        public IActionResult Create(NewsInfo news)
        {
            try
            {
                _news.CreateNews(news);
                _news.Save();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "User")]
        [HttpPatch]
        public IActionResult Update(NewsInfo news)
        {
            try
            {
                _news.UpdateNews(news);
                _news.Save();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "User")]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _news.DeleteNews(id);
                _news.Save();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        protected override void Dispose(bool disposing)
        {
            _news.Dispose();
            base.Dispose(disposing);
        }
    }
}