using System.Globalization;
using System.Windows.Controls;

namespace SQLiteWpfApp.Views.ValidationRules
{
    public class NotEmptyValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (value == null || string.IsNullOrEmpty(value.ToString()))
            {
                return new ValidationResult(false, "Value cannot be empty!");
            }
            else
            {
                return ValidationResult.ValidResult;
            }
        }
    }
}