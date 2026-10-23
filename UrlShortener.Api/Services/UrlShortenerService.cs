using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using UrlShortener.Api.Domain.Entities;
using UrlShortener.Api.DTOs;
using UrlShortener.Api.Repositories;

namespace UrlShortener.Api.Services
{
    public class UrlShortenerService : IUrlShortenerService
    {
        private readonly IUrlRepository _repository;
        private readonly IConfiguration _configuration;
        private const int CodeLength = 7;
        private const int MaxAttempts = 5;

        private const string Characters =
            "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
        public UrlShortenerService(IUrlRepository urlRepository, IConfiguration configuration)
        {
            _repository = urlRepository;
            _configuration = configuration;
        }
        public async Task<ShortUrlResponse> CreateShortUrlAsync(CreateShortUrlRequest request, CancellationToken cancellationToken = default)
        {
            if (!Uri.TryCreate(
                request.OriginalUrl,
                UriKind.Absolute,
                out var uri) ||
            (uri.Scheme != Uri.UriSchemeHttp &&
             uri.Scheme != Uri.UriSchemeHttps))
            {
                throw new ArgumentException(
                    "Only absolute HTTP and HTTPS URLs are supported.");
            }

            // 1. Return an existing mapping if the exact URL is already saved.
            var existing = await _repository.GetByOriginalUrlAsync(
                request.OriginalUrl,
                cancellationToken);

            if (existing is not null)
            {
                return MapToResponse(existing);
            }

            // 2. Generate and attempt to save a new unique code.
            for (var attempt = 0; attempt < MaxAttempts; attempt++)
            {
                var mapping = new UrlMapping
                {
                    OriginalUrl = request.OriginalUrl,
                    ShortCode = GenerateShortCode(),
                    CreatedAtUtc = DateTime.UtcNow
                };

                try
                {
                    await _repository.AddAsync(mapping, cancellationToken);
                    return MapToResponse(mapping);
                }
                catch (DbUpdateException)
                {
                    // A concurrent request may have saved the same original URL.
                    var existingAfterFailure =
                        await _repository.GetByOriginalUrlAsync(
                            request.OriginalUrl,
                            cancellationToken);

                    if (existingAfterFailure is not null)
                    {
                        return MapToResponse(existingAfterFailure);
                    }

                    // Otherwise, assume a possible short-code collision
                    // and try another code.
                }
            }

            throw new InvalidOperationException(
                "Could not generate a unique short code. Please try again.");

        }

        public Task<UrlMapping?> GetByShortCodeAsync(string shortCode, CancellationToken cancellationToken = default)
        {
            return _repository.GetByShortCodeAsync(
            shortCode,
            cancellationToken);
        }

        private static string GenerateShortCode()
        {
            var code = new char[CodeLength];

            for (var i = 0; i < CodeLength; i++)
            {
                var index = RandomNumberGenerator.GetInt32(Characters.Length);
                code[i] = Characters[index];
            }

            return new string(code);
        }


        private ShortUrlResponse MapToResponse(UrlMapping mapping)
        {
            var baseUrl = _configuration["ShortUrlBaseUrl"];

            if (string.IsNullOrWhiteSpace(baseUrl))
            {
                throw new InvalidOperationException(
                    "ShortUrlBaseUrl configuration is missing.");
            }

            return new ShortUrlResponse
            {
                ShortCode = mapping.ShortCode,
                ShortUrl = $"{baseUrl.TrimEnd('/')}/{mapping.ShortCode}",
                OriginalUrl = mapping.OriginalUrl,
                CreatedAtUtc = mapping.CreatedAtUtc
            };
        }
    }
}
