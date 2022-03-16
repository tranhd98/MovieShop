using System.ComponentModel.DataAnnotations;

namespace ApplicationCore.validators;

public class MinimumAllowedYearAttribute: ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        var userEnteredYear = ((DateTime) value).Year;
        if (userEnteredYear < 1900)
        {
            return new ValidationResult("Year should be no less than 1990");
        }
        return ValidationResult.Success;
    }
}