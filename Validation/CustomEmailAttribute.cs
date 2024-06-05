using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace ChildrenWebApp.Validation
{
    public class CustomEmailAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return ValidationResult.Success;
            }

            var email = value.ToString();
            // This regex is more complex and will validate more email addresses
            var emailRegex = new Regex(@"^[^\s@]+@[^\s@]+\.[^\s@]+$", RegexOptions.IgnoreCase);
            if (emailRegex.IsMatch(email))
            {
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult("Invalid email address.");
            }
        }
    }
}
