using static System.Net.Mime.MediaTypeNames;

namespace RestaurantAPI.Middleware
{
    public class TimingMiddleware
    {
        private readonly ILogger _logger;
        private readonly RequestDelegate _next;
        public TimingMiddleware(ILogger<TimingMiddleware> logger,
            RequestDelegate next) 
        {
            _logger = logger;
            _next = next;
        }

        public async Task Invoke(HttpContext ctx) 
        {
            var start = DateTime.UtcNow;
            await _next.Invoke(ctx);
            _logger.LogInformation($"Request {ctx.Request.Path} obsłużony w czasie: {(DateTime.UtcNow - start).TotalSeconds} s");
            Console.WriteLine($"Request {ctx.Request.Path} obsłużony w czasie: {(DateTime.UtcNow - start).TotalSeconds} s");
        }
    }

    public static class TimingExtensions
    {
        public static IApplicationBuilder UseTiming(this IApplicationBuilder app)
        {
            return app.UseMiddleware<TimingMiddleware>();
        }
    }
}
