using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using URLShortener.Models;

namespace URLShortener.Database
{
    public class UrlShortenerDbContext : DbContext
    {
        public UrlShortenerDbContext(DbContextOptions<UrlShortenerDbContext> options) : base(options)
        {
        }

        public DbSet<URL> Urls { get; set; }
        public DbSet<User> Users { get; set; }

    }
}
