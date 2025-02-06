namespace UserManagementAPI.Interface;

public interface ICacheStrategy
{
    void Set(string key, object value, TimeSpan? expiration);
    T Get<T>(string key);
}
