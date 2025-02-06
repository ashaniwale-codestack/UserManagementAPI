using UserManagementAPI.Models;

namespace UserManagementAPI.Interface;

public interface IUserService
{
    AuthenticateResponse Authenticate(AuthenticateRequest model);
   
    User GetUser(string Email);

    bool RegisterUser(User user);
}