using Microsoft.Extensions.Caching.Memory;
using System.Collections.Concurrent;
using UserManagementAPI.Interface;
using UserManagementAPI.Models;

namespace UserManagementAPI.Services;

public class InMemoryUserStore : ICacheStrategy
{
    public static ConcurrentDictionary<string, User> userCache = new ConcurrentDictionary<string, User>();

    public T Get<T>(string key)
    {
        if (userCache.TryGetValue(key, out User user))
        {
            if (user is T value)
            {
                return value;
            }
        }
        return default;
    }

    public void Set(string key, object value, TimeSpan? expiration)
    {
        if (value is User user)
        {
            userCache[key] = user;
        }
        else
        {
            throw new ArgumentException("Expected value of type User", nameof(value));
        }
    }
}
