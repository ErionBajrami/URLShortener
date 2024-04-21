using URLShortener.ModelHelpers;
using URLShortener.Models;

namespace URLShortener.Service.Url
{
    public interface IUrlService
    {
        IEnumerable<UrlResponseDto> GetAllUrls();
        URL GetById(int id);
        string ShortenUrl(string originalUrl, int userId);
        void DeleteUrl(int id);
        void UpdateUrl(int id, UrlUpdate updatedUrl);
    }

}
