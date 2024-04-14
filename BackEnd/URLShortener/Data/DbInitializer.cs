using URLShortener.Database;
using URLShortener.Models;

namespace URLShortener.Data
{
    public static class DbInitializer
    {
        public static void SeedDefaultData(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var dbContext = serviceScope.ServiceProvider.GetService<UrlShortenerDbContext>();

                if (!dbContext.Users.Any())
                {
                    dbContext.Users.Add(new User
                    {
                        FullName = "simple name",
                        Email = "email@gmail.com"
                    });
                    dbContext.SaveChanges();
                }

                if (!dbContext.Urls.Any())
                {
                    dbContext.Urls.Add(new URL()
                    {
                        OriginalUrl = "https://gjirafa.com",
                        ShortUrl = "ndh",
                        NrOfClicks = 1,
                        DateCreated = DateTime.Now,
                        User = dbContext.Users.First()
                    });
                    dbContext.SaveChanges();
                }
            }
        }
    }
}
