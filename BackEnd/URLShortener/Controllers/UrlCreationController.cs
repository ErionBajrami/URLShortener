using System.Security.Cryptography;
using System.Text;
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

            // it was
            // ShortUrl = GenerateShortURL(5), <-- meaning the length of the shortened url
            // I found it unnecessary to specify 
            // the length of the shortened url

            var exists = _context.Urls.FirstOrDefault(x => x.OriginalUrl == url);
            
            if (exists != null)
            {
                return BadRequest("URL exists");
            }

            if (!string.IsNullOrEmpty(url))
            {
                string keywordToValidUrl = url.Replace("%3a%2f%2f", "://");
                // url = _context.Urls.FirstOrDefault(x => x.OriginalUrl.ToLower().Contains(keywordToValidUrl.ToLower()));
            }
            
            string ShortenedUrl = GenerateShortURL(url);
            
            
            var newUrl = new URL()
            {
                OriginalUrl = url.ToLower(),
                ShortUrl = ShortenedUrl,
                NrOfClicks = 0,
                UserId = null,
                DateCreated = DateTime.UtcNow,
            };

            _context.Urls.Add(newUrl);
            _context.SaveChanges();
            return Ok(newUrl.ShortUrl);
        }
        //private string GenerateShortUrl(int length)
        //{
        //    var random = new Random();
        //    const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        //
        //    return new string(
        //        Enumerable.Repeat(chars, length)
        //        .Select(s => s[random.Next(s.Length)])
        //       .ToArray()
        //        );
        //
        //}



        // Changed it to this
        // Uses a Base62Encode to convert each character
        // into a series of characters using 
        // a system of 62 unique symbols
        // This process maps characters to specific numeric values
        // and then represents them using the base-62 set,
        // creating a compact original string
        // then it hashes it with MD5
        // =================
        // MD5 hashing transforms input data
        // into a fixed-length 128-bit hash value
        private string GenerateShortURL(string originalURL)
        {
            string md5Hash = ComputeMD5Hash(originalURL);
            string base62Encoded = Base62Encode(md5Hash);

            // Take the first 7 characters as the short URL key
            string shortURLKey = base62Encoded.Substring(0, originalURL.Length);

            return shortURLKey;
        }

        private string ComputeMD5Hash(string input)
        {
            using (MD5 md5 = MD5.Create())
            {
                byte[] inputBytes = Encoding.UTF8.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                // Convert hash to hexadecimal string
                return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
            }
        }

        private string Base62Encode(string input)
        {
            const string base62Chars = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
            byte[] bytes = Convert.FromBase64String(input);
            System.Numerics.BigInteger hashValue = new System.Numerics.BigInteger(bytes);

            StringBuilder sb = new StringBuilder();
            while (hashValue > 0)
            {
                int remainder = (int)(hashValue % 62);
                sb.Insert(0, base62Chars[remainder]);
                hashValue /= 62;
            }

            return sb.ToString();
        }
    }
}
