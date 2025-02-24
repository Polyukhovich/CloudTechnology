using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using WebApplication2.Models;
using System.IO;

namespace WebApplication2.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Route("sticker{num}.webp")]
        public IActionResult GetSticker(string num)
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", $"sticker{num}.webp");
            if (System.IO.File.Exists(filePath))
            {
                return PhysicalFile(filePath, "image/webp");
            }
            return NotFound("Image not found");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
