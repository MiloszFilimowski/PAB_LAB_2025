using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace CarDealer.WebApi.Middleware
{
    public class CustomHeaderMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<CustomHeaderMiddleware> _logger;

        public CustomHeaderMiddleware(RequestDelegate next, ILogger<CustomHeaderMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Log all incoming request headers
            foreach (var header in context.Request.Headers)
            {
                _logger.LogInformation($"Header: {header.Key} = {header.Value}");
            }

            // Add a custom header to the response
            context.Response.OnStarting(() => {
                context.Response.Headers.Add("X-Powered-By", "CarDealer");
                return Task.CompletedTask;
            });

            await _next(context);
        }
    }
} 