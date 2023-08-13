using Back.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using System.Web;

namespace Back.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class ImageController : Controller
    {
        private readonly string _folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/");

        public ImageController()
        {
            Directory.CreateDirectory(_folderPath);
        }

        [HttpPost]
        public IActionResult Upload(IFormFile file)
        {
            if (!CheckFileType.IsImage(file))
            {
                return BadRequest("Please upload an image!");
            }

            string filename = file.FileName;

            string uniqueName = WebEncoders.Base64UrlEncode(Guid.NewGuid().ToByteArray()) + '_' + filename;

            var imagePath = Path.Combine(_folderPath, uniqueName);

            file.CopyTo(new FileStream(imagePath, FileMode.Create));

            return Ok($"{Request.Scheme}://{Request.Host.Value}/images/{uniqueName}");
        }
    }
}