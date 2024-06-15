namespace portfolioApi.Components.Middleware
{
    public class ApiKeyMiddleware
    {
        private readonly RequestDelegate _next;
        private const string APIKEYNAME = "API_KEY";
        private readonly ILogger<ApiKeyMiddleware> _logger;

        public ApiKeyMiddleware(RequestDelegate next, ILogger<ApiKeyMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            _logger.LogInformation($"Request Path: {context.Request.Path}");

            // Allow Swagger requests and static files without API key
            if (context.Request.Path.StartsWithSegments("/swagger") ||
                context.Request.Path.StartsWithSegments("/portfolio/images"))
            {
                _logger.LogInformation("Bypassing API Key check for static files or Swagger.");
                await _next(context);
                return;
            }

            if (!context.Request.Headers.TryGetValue(APIKEYNAME, out var extractedApiKey))
            {
                context.Response.StatusCode = 401; // Unauthorized
                await context.Response.WriteAsync("Api Key was not provided.");
                _logger.LogWarning("API Key was not provided.");
                return;
            }

            var apiKey = Environment.GetEnvironmentVariable("API_KEY");
            _logger.LogInformation($"Extracted API Key: {extractedApiKey}");
            _logger.LogInformation($"Expected API Key: {apiKey}");


            if (apiKey == null)
            {
                _logger.LogError("API Key environment variable not found.");
                context.Response.StatusCode = 500; // Internal Server Error
                await context.Response.WriteAsync("Internal Server Error: API Key not configured.");
                return;
            }

            if (!apiKey.Equals(extractedApiKey))
            {
                context.Response.StatusCode = 401; // Unauthorized
                await context.Response.WriteAsync("Unauthorized client.");
                _logger.LogWarning("Unauthorized client. Provided API key does not match.");
                return;
            }

            await _next(context);
        }
    }
}
