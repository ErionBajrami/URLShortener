using URLShortener.Models;

namespace URLShortener.DTOs.User
{
    public class UserUrls
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public DateTime? CreatedAt { get; set; }
        public List<URL> Urls { get; set; }

        public UserUrls()
        {
            Urls = new List<URL>();
        }
    }
}
