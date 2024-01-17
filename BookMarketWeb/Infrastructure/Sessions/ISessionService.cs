using BookMarketWeb.Extensions;

using Microsoft.Extensions.Caching.Memory;

namespace BookMarketWeb.Infrastructure.Sessions;

public interface ISessionService
{
    void Set<T>(string key, T value);
    T? Get<T>(string key);
}

public class SessionService : ISessionService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ILogger<SessionService> _logger;
    private readonly IMemoryCache _memoryCache;

    public SessionService(IHttpContextAccessor httpContextAccessor, ILogger<SessionService> logger, IMemoryCache memoryCache)
    {
        _httpContextAccessor = httpContextAccessor;
        _logger = logger;
        _memoryCache = memoryCache;
    }

    public void Set<T>(string key, T value)
    {
        var session = _httpContextAccessor.HttpContext.Session;

        _memoryCache.Set($"{key}_{session.Id}", value, TimeSpan.FromMinutes(30));
        
        _logger.LogInformation("Выставлена сессия {SessionId}", $"key_{session.Id}");
    }

    public T Get<T>(string key)
    {
        var session = _httpContextAccessor.HttpContext.Session;
        
        _logger.LogInformation("Запрошена сессия {SessionId}", $"key_{session.Id}");
        return _memoryCache.Get<T>($"{key}_{session.Id}");
    }
}