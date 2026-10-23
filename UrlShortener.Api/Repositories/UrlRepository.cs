using Microsoft.EntityFrameworkCore;
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
        

        public Task<UrlMapping?> GetByOriginalUrlAsync(string originalUrl, CancellationToken cancellationToken = default)
        {
            return _context.UrlMappings
            .AsNoTracking()
            .FirstOrDefaultAsync(
                x => x.OriginalUrl == originalUrl,
                cancellationToken);
        }

        public Task<UrlMapping?> GetByShortCodeAsync(string shortCode, CancellationToken cancellationToken = default)
        {
            return _context.UrlMappings
            .AsNoTracking()
            .FirstOrDefaultAsync(
                x => x.ShortCode == shortCode,
                cancellationToken);
        }

        public async Task AddAsync(UrlMapping urlMapping, CancellationToken cancellationToken = default)
        {
            _context.UrlMappings.Add(urlMapping);

            try
            {
                await _context.SaveChangesAsync(cancellationToken);
            }
            catch
            {
                // Remove the failed entity from the DbContext's change tracker.
                _context.Entry(urlMapping).State = EntityState.Detached;
                throw;
            }
        }
    }
}
