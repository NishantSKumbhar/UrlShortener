using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UrlShortener.Api.Services;

namespace UrlShortener.Api.Controllers
{
    [Route("")]
    [ApiController]
    public class RedirectController : ControllerBase
    {
        private readonly IUrlShortenerService _service;

        public RedirectController(IUrlShortenerService service)
        {
            _service = service;
        }

        [HttpGet("{code}")]
        public async Task<IActionResult> RedirectToOriginalUrl(
            string code,
            CancellationToken cancellationToken)
        {
            var mapping = await _service.GetByShortCodeAsync(
                code,
                cancellationToken);

            if (mapping is null)
            {
                return NotFound(new
                {
                    message = "Short URL not found."
                });
            }

            return Redirect(mapping.OriginalUrl);
        }
    }
}
