using Microsoft.AspNetCore.Mvc;
using URLShortener.Database;
using URLShortener.Models;

namespace URLShortener.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UrlController : Controller
    {
        private UrlShortenerDbContext _context;
        public UrlController(UrlShortenerDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetUrls()
        {
            var allUrls = _context
                .Urls //Select only the neccessary attributes to display
                .Select(url => new DisplayURL()
                {
                    Id = url.Id,
                    OriginalUrl = url.OriginalUrl,
                    ShortUrl = url.ShortUrl,
                    NrOfClicks = url.NrOfClicks,
                    UserId = url.UserId,
                })
                .ToList();
            if(allUrls != null)
            {
                return Ok(allUrls);
            }
            return NotFound("Database is empty");
        }

        [HttpDelete]
        public IActionResult Remove(int id)
        {
            var urlToDelete = _context.Urls.FirstOrDefault(url => url.Id == id);
            _context.Urls.Remove(urlToDelete);
            _context.SaveChanges();

            if(urlToDelete != null)
            {
                return Ok("Url deleted successfully");
            }
            return NotFound("Couldn't find url with the specified id: " + id);
        }


        // Dont know if it works, Didnt test,
        // Cant test hala smu ka
        // ndreq the database problem
        // edhe pse kesh tu fol me do programera te gjirafes
        [HttpGet("Search")]
        public IActionResult SearchUrl(string UrlName)
        {
            if (string.IsNullOrEmpty(UrlName))
            {
                return BadRequest("Please type something...");
            }

            var results = _context.Urls
                .Where(url => url.OriginalUrl.Contains(UrlName.ToLower()))
                .ToList()
                .Select(url => new DisplayURL()
                {
                    UserId = url.UserId,
                    OriginalUrl = url.OriginalUrl,
                    ShortUrl = url.ShortUrl,
                    NrOfClicks = url.NrOfClicks
                })
                .ToList();

            if(results.Any())
            {
                return Ok(results);
            }

            return NotFound("No matching Urls");
        }

        /*
        [HttpPost("Shorten")]
        public IActionResult GetShortenUrl(string url)
        {
            var newUrl = new URL()
            {
                OriginalUrl = url,
                ShortUrl = GenerateShortUrl(6),
                NrOfClicks = 0,
                UserId = 1, //TODO: after login is made
                DateCreated = DateTime.UtcNow
            };
            return Ok(newUrl.ShortUrl);
        } */
    }
}
