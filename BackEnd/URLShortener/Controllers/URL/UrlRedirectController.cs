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
            // Retrieve the original long URL from the database or storage
            var url = RetrieveLongUrlFromDatabase(shortUrl);

            if (string.IsNullOrEmpty(url))
            {
                return NotFound();
            }
            
            
            UpdateClicks(shortUrl);
            _context.SaveChanges();
            
            
            // Perform the redirect
            return Redirect(url);
        }
        
        private string RetrieveLongUrlFromDatabase(string shortUrl)
        {
            var urlMapping = _context.Urls.FirstOrDefault(u => u.ShortUrl == shortUrl);
            
            if (urlMapping != null)
            {
                return urlMapping.OriginalUrl;
            }


            return null; // Short URL not found in mappings
        }

        private void UpdateClicks(string shortUrl)
        {
            var entity = _context.Urls.FirstOrDefault(x => x.ShortUrl == shortUrl);

            entity.NrOfClicks += 1;
        }
    }
}
