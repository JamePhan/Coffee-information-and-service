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
            List<NewsInfo> newsinfo = _news.GetNews();
            if (newsinfo.Count > 0)
            {
                return Ok(newsinfo);
            }
            return NotFound();
        }

        [HttpGet("{customerId}")]
        public IActionResult Customer(int customerId)
        {
            List<NewsInfo> newsinfo = _news.GetNewsCustomer(customerId);
            if (newsinfo.Count > 0)
            {
                return Ok(newsinfo);
            }
            return NotFound();
        }

        [HttpGet("{userId}")]
        public IActionResult User(int userId)
        {
            List<NewsInfo> newsinfo = _news.GetNewsUser(userId);
            if (newsinfo.Count > 0)
            {
                return Ok(newsinfo);
            }
            return NotFound();
        }

        protected override void Dispose(bool disposing)
        {
            _news.Dispose();
            base.Dispose(disposing);
        }
    }
}