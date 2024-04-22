using URLShortener.ModelHelpers;
using URLShortener.Models;

namespace URLShortener.Service.Url
{
    public interface IUrlService
    {
        IEnumerable<UrlResponseDto> GetAllUrls();
        URL GetById(int id);
        string ShortenUrl(string originalUrl, string tokens, string description);
        void DeleteUrl(int id);
        void UpdateUrl(int id, string description);
    }

}
