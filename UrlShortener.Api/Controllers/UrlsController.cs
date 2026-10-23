using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UrlShortener.Api.DTOs;
using UrlShortener.Api.Services;

namespace UrlShortener.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UrlsController : ControllerBase
    {
        private readonly IUrlShortenerService _service;

        public UrlsController(IUrlShortenerService service)
        {
            _service = service;
        }

        [HttpPost]
        [ProducesResponseType(typeof(ShortUrlResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ShortUrlResponse>> Create(
            [FromBody] CreateShortUrlRequest request,
            CancellationToken cancellationToken)
        {
            var result = await _service.CreateShortUrlAsync(
                request,
                cancellationToken);

            return StatusCode(StatusCodes.Status201Created, result);
        }
    }
}
