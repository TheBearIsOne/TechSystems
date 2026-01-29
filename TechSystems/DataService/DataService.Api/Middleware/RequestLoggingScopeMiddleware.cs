using System.Diagnostics;

namespace DataService.Api.Middleware;

public sealed class RequestLoggingScopeMiddleware(ILogger<RequestLoggingScopeMiddleware> logger)
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        var traceId = Activity.Current?.TraceId.ToString() ?? context.TraceIdentifier;
        var username = context.User.FindFirst("preferred_username")?.Value
                       ?? context.User.Identity?.Name
                       ?? "anonymous";

        using (logger.BeginScope(new Dictionary<string, object>
        {
            ["traceId"] = traceId,
            ["username"] = username
        }))
        {
            await next(context);
        }
    }
}
