namespace URLShortener.Service.Url
{
    public interface IUrlValidationService
    {
        bool IsValidUrl(string url);
    }
}
