namespace URLShortener.Models
{
    public class URL
    {
        public int Id { get; set; }
        public string OriginalUrl { get; set; }
        public string ShortUrl { get; set; }
        public int NrOfClicks { get; set; }
        public int? UserId { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateUpdated { get; set; }
        public User User { get; set; }
    }
}
