using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using UserManagementAPI.Helpers;
using UserManagementAPI.Interface;
using UserManagementAPI.Models;

namespace UserManagementAPI.Services;

public class UserService : IUserService
{
    private readonly CacheContext _cacheContext;
    private readonly AppSettings _appSettings;
    private readonly JWTTokenHelper _jwtHelper;

    public UserService(IOptions<AppSettings> appSettings, IMemoryCache memoryCache)
    {
        _appSettings = appSettings.Value;
        _jwtHelper = new JWTTokenHelper(appSettings);
        ICacheStrategy cacheStrategy;

        if (_appSettings.CacheStrategy=="memory")
        {
            cacheStrategy = new InMemoryUserStore();
        }
        else
        {
            cacheStrategy = new InMemoryCacheStrategy(memoryCache);
        }
        _cacheContext = new CacheContext(cacheStrategy);
    }

    public bool RegisterUser(User userInput)
    {
        try
        {
            userInput.Password = EncryptDecryptHelper.EncryptData(userInput.Password);
            userInput.ConfirmPassword = EncryptDecryptHelper.EncryptData(userInput.ConfirmPassword);

            userInput.Id = InMemoryUserStore.userCache.Count + 1;

            // Store the user in the in-memory store.
            _cacheContext.Set(userInput.Email, userInput, new TimeSpan(10));
        }   
        catch(Exception ex) {
            //log error msg
            return false;
        }
        return true;
    }

    public User GetUser(string Email) {
        User respose = _cacheContext.Get<User>(Email);
        if(respose==null)
        {
            return null;
        } 
        return respose;
    }

    public AuthenticateResponse? Authenticate(AuthenticateRequest model)
    {
        User user = _cacheContext.Get<User>(model.Email);
        string inputPassword = EncryptDecryptHelper.EncryptData(model.Password);

        // return null if user not found and in case username is invalid
        if (user == null || !inputPassword.Equals(user.Password))
        {
            return null;
        }

        // authentication successful so generate jwt token
        var token = _jwtHelper.GenerateJwtToken(user);

        return new AuthenticateResponse(user, token);
    }
}
