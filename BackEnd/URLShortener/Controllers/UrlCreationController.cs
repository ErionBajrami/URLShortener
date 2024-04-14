﻿using Microsoft.AspNetCore.Mvc;
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

            var newUrl = new URL()
            {
                OriginalUrl = url,
                ShortUrl = GenerateShortURL(url),
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
            string shortURLKey = base62Encoded.Substring(0, 7);

            return shortURLKey;
        }

        private string ComputeMD5Hash(string input)
        {
            using (var md5 = System.Security.Cryptography.MD5.Create())
            {
                byte[] inputBytes = System.Text.Encoding.UTF8.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
            }
        }

        private string Base62Encode(string input)
        {
            const string base62Chars = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
            byte[] bytes = Convert.FromBase64String(input);
            System.Numerics.BigInteger hashValue = new System.Numerics.BigInteger(bytes);

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
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
