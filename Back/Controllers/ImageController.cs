using Back.Utilities;
using Microsoft.AspNetCore.Mvc;
using System.Web;

namespace Back.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class ImageController : Controller
    {
        [HttpPost]
        public IActionResult Upload(IFormFile file)
        {
            if (!CheckFileType.IsImage(file))
            {
                return BadRequest("Please upload an image!");
            }

            string filename = file.FileName;

            string uniqueName = Guid.NewGuid().ToString() + '_' + filename;

            var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/", uniqueName);

            file.CopyTo(new FileStream(imagePath, FileMode.Create));

            return Ok(imagePath);
        }
    }
}