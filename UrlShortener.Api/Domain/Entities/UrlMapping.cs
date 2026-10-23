namespace UrlShortener.Api.Domain.Entities
{
    public class UrlMapping
    {
        public int Id { get; set; }

        public string OriginalUrl { get; set; } = string.Empty;

        public string ShortCode { get; set; } = string.Empty;

        public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;
    }
}
