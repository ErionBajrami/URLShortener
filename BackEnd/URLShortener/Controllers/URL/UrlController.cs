using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using URLShortener.Database;
using URLShortener.ModelHelpers;
using URLShortener.Models;

namespace URLShortener.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors]
    public class URLController : Controller
    {
        private UrlShortenerDbContext _context;
        public URLController(UrlShortenerDbContext context)
        {
            _context = context;
        }
        

        [HttpGet]
        public IActionResult GetUrls()
        {
            var allUrls = _context
                .Urls //Select only the neccessary attributes to display
                .Select(url => new UrlResponseDto()
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

        [HttpGet("{id}")]
        public IActionResult GetUrl(int id)
        {
            var url = _context.Urls.FirstOrDefault(url => url.Id == id);
            if(url == null)
            {
                return NotFound("Couldn't find url with the specified id: " + id);
            }
            return Ok(url);
        }

        
        [HttpPost]
        public IActionResult ShortenUrl(string url)
        {
            var exists = _context.Urls.FirstOrDefault(x => x.OriginalUrl == url);
            
            if (exists != null)
            {
                return BadRequest("URL exists");
            }

            // if (!string.IsNullOrEmpty(url))
            // {
            //     string keywordToValidUrl = url.Replace("%3a%2f%2f", "://");
            //     // url = _context.Urls.FirstOrDefault(x => x.OriginalUrl.ToLower().Contains(keywordToValidUrl.ToLower()));
            // }
            
            var newUrl = new URL()
            {
                OriginalUrl = url.ToLower(),
                ShortUrl = GenerateShortUrl(5),
                NrOfClicks = 0,
                UserId = null,
                DateCreated = DateTime.UtcNow,
            };

            _context.Urls.Add(newUrl);
            _context.SaveChanges();
            return Ok(newUrl.ShortUrl);
        }


        [HttpDelete]
        public IActionResult Remove(int id)
        {
            var urlToDelete = _context.Urls.FirstOrDefault(url => url.Id == id);
            
            if(urlToDelete == null)
            {
                return NotFound("Couldn't find url with the specified id: " + id);
            }
            
            _context.Urls.Remove(urlToDelete);
            _context.SaveChanges();

            return Ok("URL DELETED SUCESSFULLY " + id);
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

        [HttpPut("{id}")]
        public IActionResult UpdateUrl(int id, [FromBody] UrlUpdate updated)
        {
            var urlToUpdate = _context.Urls.FirstOrDefault(url => url.Id == id);

            if (urlToUpdate == null)
            {
                return NotFound("Couldn't find url with the specified id :" +id);
            }

            //Update the properties of the URL object based on the provided Url
            urlToUpdate.OriginalUrl = updated.OriginalUrl;
            updated.ShortUrl = updated.ShortUrl;
            updated.UserId = updated.UserId;

            _context.SaveChanges();
            return Ok("URL updated successfully");
        }
    }
}
