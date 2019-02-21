using System.ComponentModel.DataAnnotations;

namespace HomeBudget2.Validation_Attributes
{
    public class NotEqualToZero : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null &&value.ToString() == "0")
            {
                return new ValidationResult("Amount can't be = 0");
            }
            else
            {
                return ValidationResult.Success;
            }
        }
    }
}