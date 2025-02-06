using Microsoft.Extensions.Caching.Memory;
using UserManagementAPI.Interface;

namespace UserManagementAPI.Services;

public class InMemoryCacheStrategy : ICacheStrategy
{
    private readonly IMemoryCache _memoryCache;

    public InMemoryCacheStrategy(IMemoryCache memoryCache)
    {
        _memoryCache = memoryCache;
    }

    public void Set(string key, object value, TimeSpan? expiration)
    {
        var cacheOptions = new MemoryCacheEntryOptions();
        if (expiration.HasValue)
        {
            cacheOptions.SetAbsoluteExpiration(expiration.Value);
        }
        else
        {
            // Optional: Set sliding expiration or leave it as default
            cacheOptions.SetSlidingExpiration(TimeSpan.FromMinutes(30));
        }

        _memoryCache.Set(key, value, cacheOptions);
    }

    public T Get<T>(string key)
    {
        if (_memoryCache.TryGetValue(key, out T cachedData))
        {
            return cachedData;
        }
        return default;
    }

    // Remove data from memory cache
    public void RemoveCache(string key)
    {
        _memoryCache.Remove(key);
    }
}

