using System;
using URLShortener.Database;
using URLShortener.ModelHelpers;
using URLShortener.Models;

namespace URLShortener.Service.Url
{
    public class UrlService : IUrlService
    {

        private readonly UrlShortenerDbContext _context;

        public UrlService(UrlShortenerDbContext context)
        {
            _context = context;
        }

        public IEnumerable<UrlResponseDto> GetAllUrls()
        {
            return _context.Urls
                .Select(url => new UrlResponseDto()
                {
                    Id = url.Id,
                    OriginalUrl = url.OriginalUrl,
                    ShortUrl = url.ShortUrl,
                    NrOfClicks = url.NrOfClicks,
                    UserId = url.UserId,
                })
            .ToList();
        }


        public URL GetById(int id)
        {
            return _context.Urls.FirstOrDefault(url => url.Id == id);
        }

        public string ShortenUrl(string originalUrl, int userId)
        {
            if (!_context.Users.Any(u => u.Id == userId))
            {
                throw new ArgumentException("User not found");
            }

            var existingUrl = _context.Urls.FirstOrDefault(u => u.OriginalUrl == originalUrl);
            if (existingUrl != null)
            {
                throw new InvalidOperationException("URL already exists");
            }

            var newUrl = new URL()
            {
                OriginalUrl = originalUrl.ToLower(),
                ShortUrl = GenerateShortUrl(5),
                NrOfClicks = 0,
                UserId = userId,
                DateCreated = DateTime.UtcNow,
            };

            _context.Urls.Add(newUrl);
            _context.SaveChanges();

            return newUrl.ShortUrl;
        }

        public void DeleteUrl(int id)
        {
            var urlToDelete = _context.Urls.FirstOrDefault(url => url.Id == id);
            if (urlToDelete != null)
            {
                _context.Urls.Remove(urlToDelete);
                _context.SaveChanges();
            }
        }

        public void UpdateUrl(int id, UrlUpdate updatedUrl)
        {
            var urlToUpdate = _context.Urls.FirstOrDefault(url => url.Id == id);
            if (urlToUpdate != null)
            {
                urlToUpdate.OriginalUrl = updatedUrl.OriginalUrl;
                urlToUpdate.ShortUrl = updatedUrl.ShortUrl;
                urlToUpdate.UserId = updatedUrl.UserId;

                _context.SaveChanges();
            }
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
