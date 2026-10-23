using Microsoft.EntityFrameworkCore;

namespace UrlShortener.Api.Data
{
    public class UrlShortenerDbContext:DbContext
    {
        public UrlShortenerDbContext(DbContextOptions<UrlShortenerDbContext> options):base(options)
        {
            
        }
    }
}
