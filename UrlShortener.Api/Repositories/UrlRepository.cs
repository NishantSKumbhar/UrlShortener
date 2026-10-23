using UrlShortener.Api.Data;
using UrlShortener.Api.Domain.Entities;

namespace UrlShortener.Api.Repositories
{
    public class UrlRepository : IUrlRepository
    {
        private readonly UrlShortenerDbContext _context;

        public UrlRepository(UrlShortenerDbContext dbContext)
        {
            _context = dbContext;
        }
        public Task AddAsync(UrlMapping urlMapping, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<UrlMapping?> GetByOriginalUrlAsync(string originalUrl, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<UrlMapping?> GetByShortCodeAsync(string shortCode, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
