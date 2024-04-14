using Microsoft.AspNetCore.Identity;
using System;
namespace URLShortener.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public String? FullName { get; set; }
        public List<URL> Urls { get; set; }
        public User()
        {
            Urls = new List<URL>();
        }
    }
}
