using UserManagementAPI.Interface;
namespace UserManagementAPI.Services;

public class CacheContext
{
    private readonly ICacheStrategy _cacheStrategy;

    public CacheContext(ICacheStrategy cacheStrategy)
    {
        _cacheStrategy = cacheStrategy;
    }

    public void Set(string key, object value, TimeSpan expiration)
    {
        _cacheStrategy.Set(key, value, expiration);
    }

    public T Get<T>(string key)
    {
        return _cacheStrategy.Get<T>(key);
    }
}

