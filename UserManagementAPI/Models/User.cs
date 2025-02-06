using System.ComponentModel.DataAnnotations;

namespace UserManagementAPI.Models;

public class User
{

    public int? Id { get; set; }

    [Required]
    public string FirstName { get; set; }

    [Required]
    public string LastName { get; set; }

    [Required]
    [EmailAddress(ErrorMessage = "Invalid email address format.")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Password is required.")]
    [StringLength(25, MinimumLength = 8, ErrorMessage = "Password must be at least 8 characters long.")]
    public string Password { get; set; }

    [Required(ErrorMessage = "Confirm is required.")]
    [Compare("Password", ErrorMessage = "Passwords do not match.")]
    public string ConfirmPassword { get; set; }

    [StringLength(15, ErrorMessage = "Phone number cannot be longer than 15 characters.")]
    [RegularExpression(@"^\+?\d{1,4}?[\s\-]?\(?\d{1,3}?\)?[\s\-]?\d{3}[\s\-]?\d{4}$",
      ErrorMessage = "Phone number is not in a valid format.")]
    public string? PhoneNumber { get; set; }

    [DataType(DataType.Date)]
    public string? DOB { get; set; }
}
