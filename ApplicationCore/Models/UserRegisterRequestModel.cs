using System.ComponentModel.DataAnnotations;
using ApplicationCore.validators;

namespace ApplicationCore.Models;

public class UserRegisterRequestModel
{
    
    [Required]
    [StringLength(50, ErrorMessage = "Email cannot exceed 50 characters")]
    [EmailAddress(ErrorMessage = "Email should be right format")]
    public string Email { get; set; }
    
    [Required(ErrorMessage = "Password Name should not be empty!")]
    [RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[#$^+=!*()@%&]).{8,}$", ErrorMessage =
        "Password Should have minimum 8 with at least one upper, lower, number and special character")] 
    public string Password { get; set; }
    
    [Required(ErrorMessage = "Date of birth should not be empty")]
    [MinimumAllowedYear]
    [MaximumAllowedYear]
    public DateTime DateOfBirth { get; set; }
    
    [Required(ErrorMessage = "First Name cannot be empty")]
    [StringLength(50, ErrorMessage = "First Name cannot exceed 50 characters")]
    public string FirstName { get; set; }
    
    [Required(ErrorMessage = "First Name cannot be empty")]
    [StringLength(50, ErrorMessage = "First Name cannot exceed 50 characters")]
    public string LastName { get; set; }
    
}