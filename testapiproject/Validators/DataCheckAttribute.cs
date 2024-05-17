using System.ComponentModel.DataAnnotations;

namespace testapiproject.Validators
{
    public class DateCheckAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var date = (DateTime?)value;
            if (date < DateTime.Today)
            {
                return new ValidationResult("The date time must be greater than today");
            }
            return ValidationResult.Success;
        }
    }
}