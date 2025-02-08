namespace UserManagementAPI.Controllers;

using Microsoft.AspNetCore.Mvc;
using UserManagementAPI.Interface;
using UserManagementAPI.Models;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet("Test")]
    public IActionResult Test()
    {
        return Ok("Success");
    }

        [HttpPost("Login")]
    public IActionResult Authenticate([FromForm] AuthenticateRequest input)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var response = _userService.Authenticate(input);
        if (response == null)
        {
            return Unauthorized(ApplicationMessages.InvalidUserNamePassword);
        }
        return Ok(response);
    }

    [HttpPost("Register")]
    public IActionResult CreateUser([FromForm] User userInput)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        // Check if the email already exists in the system
        var existingUser = _userService.GetUser(userInput.Email);

        if (existingUser != null)
        {
            return Conflict(ApplicationMessages.ConflictEmail);
        }
        try
        {
            // If email is unique, hash the password and store the user.
            bool respone = _userService.RegisterUser(userInput);

            if (respone)
            {
                return Ok(ApplicationMessages.SuccessfullyRegistered);
            }

            // If registration fails, return a 500 Internal Server Error with a message
            return StatusCode(500, ApplicationMessages.InternalError);
        }
        catch (Exception ex)
        {
            // Log the exception 
            // Return a generic internal server error
            return StatusCode(500, ApplicationMessages.InternalError);
        }

    }
}
