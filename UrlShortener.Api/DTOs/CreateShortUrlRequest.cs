using System.ComponentModel.DataAnnotations;

namespace UrlShortener.Api.DTOs
{
    public class CreateShortUrlRequest
    {
        [Required]
        [MaxLength(2048)]
        [Url]
        public string OriginalUrl { get; set; } = string.Empty;
    }
}
