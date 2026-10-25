using UrlShortener.Api.Domain.Entities;

namespace UrlShortener.Api.Repositories
{
    public interface IUrlRepository
    {
        Task<UrlMapping?> GetByOriginalUrlHashAsync(
        string originalUrlHash,
        CancellationToken cancellationToken = default);

        Task<UrlMapping?> GetByShortCodeAsync(
            string shortCode,
            CancellationToken cancellationToken = default);

        Task AddAsync(
            UrlMapping urlMapping,
            CancellationToken cancellationToken = default);
    }
}
