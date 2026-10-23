using UrlShortener.Api.Domain.Entities;
using UrlShortener.Api.DTOs;

namespace UrlShortener.Api.Services
{
    public interface IUrlShortenerService
    {
        Task<ShortUrlResponse> CreateShortUrlAsync(
        CreateShortUrlRequest request,
        CancellationToken cancellationToken = default);

        Task<UrlMapping?> GetByShortCodeAsync(
            string shortCode,
            CancellationToken cancellationToken = default);
    }
}
