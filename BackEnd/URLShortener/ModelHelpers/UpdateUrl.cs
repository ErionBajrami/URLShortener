namespace URLShortener.ModelHelpers
{
    public class UpdateUrl
    {
        public string OriginalUrl { get; set; }
        public string ShortUrl { get; set; }
        public int? UserId { get; set; }
    }
}
