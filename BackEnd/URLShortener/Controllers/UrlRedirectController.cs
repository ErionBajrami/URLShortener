using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using URLShortener.Database;
using URLShortener.Models;

namespace URLShortener.Controllers
{

    [ApiController]
    [Route("api/Redirect")]
    public class UrlRedirectController : ControllerBase
    {
        private UrlShortenerDbContext _context;
        public UrlRedirectController(UrlShortenerDbContext context)
        {
            _context = context;
        }

        [HttpGet("/{shortUrl}")]
        public IActionResult RedirectShortUrl(string shortUrl)
        {
            // Retrieve the original long URL from the database or storage
            string OriginalUrl = RetrieveLongUrlFromDatabase(shortUrl);

            if (string.IsNullOrEmpty(OriginalUrl))
            {
                return NotFound();
            }

            // Perform the redirect
            return RedirectPermanent(OriginalUrl);
        }

        private string RetrieveLongUrlFromDatabase(string shortUrl)
        {
            // Implement logic to retrieve the long URL associated with the given short URL
            // Example: query the database or lookup in a storage



            // Nuk e di a bon i cant test it now
            var urlMappings = _context.Urls.FirstOrDefault(x => x.ShortUrl == shortUrl);
            if(urlMappings != null)
            {
                return urlMappings.OriginalUrl;
            }


            return null; // Short URL not found in mappings
        }
    }
}
