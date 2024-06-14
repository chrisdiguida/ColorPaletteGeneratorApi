using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace ColorPaletteGeneratorApi.Attributes
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
    public class HexColorAttribute() : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            string errorMessage = $"The field \"{validationContext.DisplayName}\" is not a valid HEX color.";
            string parsedValue = value?.ToString();
            if (!IsValidHexColor(parsedValue)) return new ValidationResult(errorMessage);
            return ValidationResult.Success;
        }

        private static bool IsValidHexColor(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return false;

            string pattern = "^#(?:[0-9a-fA-F]{3}){1,2}$";
            Regex regex = new(pattern, RegexOptions.IgnoreCase);

            return regex.IsMatch(input);
        }
    }
}
