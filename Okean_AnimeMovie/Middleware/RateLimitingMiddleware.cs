using System.Collections.Concurrent;

namespace Okean_AnimeMovie.Middleware;

public class RateLimitingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ConcurrentDictionary<string, RateLimitInfo> _rateLimitStore;
    private readonly int _maxRequests;
    private readonly TimeSpan _window;

    public RateLimitingMiddleware(RequestDelegate next, int maxRequests = 100, int windowMinutes = 1)
    {
        _next = next;
        _maxRequests = maxRequests;
        _window = TimeSpan.FromMinutes(windowMinutes);
        _rateLimitStore = new ConcurrentDictionary<string, RateLimitInfo>();
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var clientId = GetClientId(context);
        var rateLimitInfo = _rateLimitStore.GetOrAdd(clientId, _ => new RateLimitInfo());

        if (IsRateLimitExceeded(rateLimitInfo))
        {
            context.Response.StatusCode = 429; // Too Many Requests
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync("{\"error\":\"Rate limit exceeded. Please try again later.\"}");
            return;
        }

        rateLimitInfo.IncrementRequest();
        await _next(context);
    }

    private string GetClientId(HttpContext context)
    {
        // Use IP address as client identifier
        return context.Connection.RemoteIpAddress?.ToString() ?? "unknown";
    }

    private bool IsRateLimitExceeded(RateLimitInfo rateLimitInfo)
    {
        var now = DateTime.UtcNow;
        
        // Clean old requests outside the window
        rateLimitInfo.CleanOldRequests(now, _window);
        
        return rateLimitInfo.RequestCount >= _maxRequests;
    }
}

public class RateLimitInfo
{
    private readonly ConcurrentQueue<DateTime> _requests = new();
    public int RequestCount => _requests.Count;

    public void IncrementRequest()
    {
        _requests.Enqueue(DateTime.UtcNow);
    }

    public void CleanOldRequests(DateTime now, TimeSpan window)
    {
        var cutoff = now - window;
        
        while (_requests.TryPeek(out var oldestRequest) && oldestRequest < cutoff)
        {
            _requests.TryDequeue(out _);
        }
    }
}
