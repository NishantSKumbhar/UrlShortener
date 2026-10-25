using System.ComponentModel.DataAnnotations;

namespace UrlShortener.Api.DTOs
{
    public class CreateShortUrlRequest
    {
        [Required(ErrorMessage = "Original URL is required.")]
        [StringLength(
        2048,
        MinimumLength = 1,
        ErrorMessage = "URL must not exceed 2048 characters.")]
        [Url]
        public string OriginalUrl { get; set; } = string.Empty;
    }
}
