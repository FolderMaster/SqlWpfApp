using System.Globalization;
using System.Windows.Controls;

namespace View.ValidationRules
{
    /// <summary>
    /// Класс правила валидации, не допускающем пустые значения строки.
    /// </summary>
    public class NotEmptyValidationRule : ValidationRule
    {
        /// <summary>
        /// Валидирует значение.
        /// </summary>
        /// <param name="value">Значение валидации.</param>
        /// <param name="cultureInfo">Сведения о культуре.</param>
        /// <returns>Результат валидации.</returns>
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