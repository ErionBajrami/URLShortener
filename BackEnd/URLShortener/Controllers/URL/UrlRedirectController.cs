using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using URLShortener.Database;
using URLShortener.Models;
using URLShortener.Service;

namespace URLShortener.Controllers
{

    [ApiController]
    [Route("/")]
    [EnableCors]
    public class URLRedirectController : ControllerBase
    {
        private UrlShortenerDbContext _context;
        public URLRedirectController(UrlShortenerDbContext context)
        {
            _context = context;
        }

        [HttpGet("{shortUrl}")]
        public IActionResult RedirectShortUrl(string shortUrl)
        {

            try
            {
                // Retrieve the original long URL from the database
                var urlMapping = _context.Urls.FirstOrDefault(u => u.ShortUrl == shortUrl);

                if (urlMapping != null)
                {
                    // Update click count
                    urlMapping.NrOfClicks++;
                    _context.SaveChanges();

                    // Redirect to the original URL
                   // return Ok(urlMapping.OriginalUrl);

                    return Ok(urlMapping.OriginalUrl);
                }
                else
                {
                    return NotFound(); // Short URL not found
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }
    }
}