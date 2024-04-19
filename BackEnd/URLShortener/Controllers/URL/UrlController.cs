using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
            if(allUrls.Any())
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
        public IActionResult ShortenUrl(string url, int userId)
        {
            if (!IsValidUrl(url))
            {
                return BadRequest("Invalid URL format. Please provide a valid URL starting with 'http://' or 'https://'.");
            }
            
            var exists = _context.Urls.FirstOrDefault(x => x.OriginalUrl == url);
            
            if (exists != null)
            {
                return BadRequest("URL exists");
            }
            
            
            var user = _context.Users.FirstOrDefault(u => u.Id == userId);
            if (user == null)
            {
                return NotFound("User not found");
            }

            var newUrl = new URL()
            {
                OriginalUrl = url.ToLower(),
                ShortUrl = GenerateShortUrl(5),
                NrOfClicks = 0,
                UserId = userId,
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
            //TODO: USER ID DOES NOT GET UPDATED!!!
            //Update the properties of the URL object based on the provided Url
            var userExists = _context.Users.Any(user => user.Id == updated.UserId);
            if (!userExists)
            {
                return NotFound("Couldn't find user with the specified ID: " + updated.UserId);
            }


            urlToUpdate.OriginalUrl = updated.OriginalUrl;
            urlToUpdate.ShortUrl = updated.ShortUrl;
            urlToUpdate.UserId = updated.UserId;

            _context.SaveChanges();
            return Ok("URL updated successfully");
        }
        
        private bool IsValidUrl(string url)
        {
            // Check if URL starts with 'http://' or 'https://'
            return Uri.TryCreate(url, UriKind.Absolute, out Uri uriResult)
                   && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
        }
    }
}
