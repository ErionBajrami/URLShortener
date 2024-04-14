using Microsoft.AspNetCore.Mvc;
using URLShortener.Database;
using URLShortener.Models;

namespace URLShortener.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UrlCreationController : Controller
    {
        private UrlShortenerDbContext  _context;    
        public UrlCreationController(UrlShortenerDbContext context)
        {
            _context = context;
        }
        [HttpPost]
        public IActionResult ShortenUrl(string url)
        {
            var newUrl = new URL()
            {
                OriginalUrl = url,
                ShortUrl = GenerateShortUrl(5),
                NrOfClicks = 0,
                UserId = null,
                DateCreated = DateTime.UtcNow,
            };

            _context.Urls.Add(newUrl);
            _context.SaveChanges();
            return Ok(newUrl.ShortUrl);
        }
        private string GenerateShortUrl(int length)
        {
            var random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

            return new string(
                Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)])
                .ToArray()
                );

        }
    }
}
