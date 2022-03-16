using System.ComponentModel.DataAnnotations;

namespace ApplicationCore.Models;

public class UserLoginRequestModel
{
    [Required(ErrorMessage = "Email should not be empty")]
    [EmailAddress(ErrorMessage = "Email should be in right format")]
    [StringLength(50, ErrorMessage = "Email cannot exceed 50 characters")]
    public string Email { get; set; }
    
    [Required(ErrorMessage = "Password Name should not be empty!")]
    public string Password { get; set; }
}