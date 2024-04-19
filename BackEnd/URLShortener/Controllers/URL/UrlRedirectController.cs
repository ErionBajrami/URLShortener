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
            // // Retrieve the original long URL from the database or storage
            // var url = RetrieveLongUrlFromDatabase(shortUrl);
            //
            // if (string.IsNullOrEmpty(url))
            // {
            //     return NotFound();
            // }
            //
            //
            // UpdateClicks(shortUrl);
            // _context.SaveChanges();
            //
            //
            //
            // // Perform the redirect
            // return new RedirectResult(url);
            // // return Redirect(url);
        }
        
        // private string RetrieveLongUrlFromDatabase(string shortUrl)
        // {
        //     var urlMapping = _context.Urls.FirstOrDefault(u => u.ShortUrl == shortUrl);
        //     
        //     if (urlMapping != null)
        //     {
        //         return urlMapping.OriginalUrl;
        //     }
        //
        //
        //     return null; // Short URL not found in mappings
        // }
        //
        // private void UpdateClicks(string shortUrl)
        // {
        //     var entity = _context.Urls.FirstOrDefault(x => x.ShortUrl == shortUrl);
        //
        //     entity.NrOfClicks += 1;
        // }
    }
}
