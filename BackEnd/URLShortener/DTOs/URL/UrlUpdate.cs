namespace URLShortener.ModelHelpers
{
    public class UrlUpdate
    {
        public string OriginalUrl { get; set; }
        public string ShortUrl { get; set; }
        public int? UserId { get; set; }
        public string Description { get; set; } 
    }
}
