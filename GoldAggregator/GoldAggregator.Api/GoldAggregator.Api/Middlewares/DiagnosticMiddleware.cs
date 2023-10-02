using System.Diagnostics;

namespace GoldAggregator.Api.Middlewares
{
    public class DiagnosticMiddleware
    {
        // private readonly ILogger _logger;
        private readonly RequestDelegate _next;

        public DiagnosticMiddleware(RequestDelegate next)
        {
            _next = next;
            // _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, ILogger logger)
        {
            Stopwatch sw = Stopwatch.StartNew();

            logger.LogInformation($"Before API action: {sw.ElapsedMilliseconds} ms");

            await _next(context);

            sw.Stop();
            var elapsedTime = sw.ElapsedTicks / (Stopwatch.Frequency / (1000L * 1000L));

            logger.LogInformation($"After API action: {elapsedTime}");
        }

    }
}
