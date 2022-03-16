using System.ComponentModel.DataAnnotations;

namespace ApplicationCore.validators;

public class MaximumAllowedYearAttribute: ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        var userEnterYear = ((DateTime) value).Year;
        if (DateTime.Now.Year - userEnterYear < 18)
        {
            return new ValidationResult("User Register must be 18 or over");
        }
        return ValidationResult.Success;
    }
}