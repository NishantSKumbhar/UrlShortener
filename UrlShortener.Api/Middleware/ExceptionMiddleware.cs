using System.Text.Json;

namespace UrlShortener.Api.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(
            RequestDelegate next,
            ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (ArgumentException ex)
            {
                _logger.LogWarning(
                    ex,
                    "Invalid request for {Path}",
                    context.Request.Path);

                await WriteErrorResponse(
                    context,
                    StatusCodes.Status400BadRequest,
                    "Invalid request.",
                    "INVALID_REQUEST");
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "Unhandled exception while processing {Path}",
                    context.Request.Path);

                await WriteErrorResponse(
                    context,
                    StatusCodes.Status500InternalServerError,
                    "An unexpected error occurred.",
                    "INTERNAL_SERVER_ERROR");
            }
        }

        private static async Task WriteErrorResponse(
            HttpContext context,
            int statusCode,
            string message,
            string errorCode)
        {
            if (context.Response.HasStarted)
            {
                throw new InvalidOperationException(
                    "Cannot write an error response after the response has started.");
            }

            context.Response.Clear();
            context.Response.StatusCode = statusCode;
            context.Response.ContentType = "application/json";

            var response = new
            {
                statusCode,
                message,
                errorCode,
                traceId = context.TraceIdentifier
            };

            await context.Response.WriteAsync(
                JsonSerializer.Serialize(response));
        }
    }
}
