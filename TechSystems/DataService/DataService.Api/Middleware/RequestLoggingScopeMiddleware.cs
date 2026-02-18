using System.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace DataService.Api.Middleware;

public sealed class RequestLoggingScopeMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<RequestLoggingScopeMiddleware> _logger;

    public RequestLoggingScopeMiddleware(
        RequestDelegate next,
        ILogger<RequestLoggingScopeMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var traceId = Activity.Current?.TraceId.ToString() ?? context.TraceIdentifier;
        var username = context.User.FindFirst("preferred_username")?.Value
                       ?? context.User.Identity?.Name
                       ?? "anonymous";

        using (_logger.BeginScope(new Dictionary<string, object>
        {
            ["traceId"] = traceId,
            ["username"] = username
        }))
        {
            await _next(context);
        }
    }
}
