using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Diagnostics;

namespace GoldAggregator.Api.ConfigurationExtensions
{
    public static class ExceptionMiddlewareExtensions
    {
        public static void ConfigureExceptionHandler(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();

                    var statusCode = (int)GetStatusCodeForException(contextFeature?.Error);

                    context.Response.StatusCode = statusCode;
                    context.Response.ContentType = "application/json";

                    if (contextFeature != null)
                    {
                        // TODO header interoption for traceId
                        var traceId = contextFeature.Error?.Data["traceId"] ?? string.Empty;
                        var stackTrace = "Stacktrace is availabe in debug mode only";

#if DEBUG
                        stackTrace = contextFeature.Error.StackTrace;
#endif

                        var errorMessage = new ErrorDetails(
                            context.Response.StatusCode,
                            contextFeature.Error.GetType().Name,
                            contextFeature.Error.Message,
                            traceId.ToString(),
                            stackTrace)
                        .ToString();

                        await context.Response.WriteAsync(errorMessage);
                    }
                });
            });
        }

        record ErrorDetails(int StatusCode, string ErrorType, string Message, string TraceId, string stackTrace)
        {
            public override string ToString()
            {
                return JsonSerializer.Serialize(this);
            }
        }

        public static HttpStatusCode GetStatusCodeForException(Exception exception)
        {
            switch (exception)
            {
                case NotSupportedException:
                    return HttpStatusCode.BadRequest;
                default:
                    return HttpStatusCode.InternalServerError;
            }
        }
    }
}
