using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using ViewModel.Classes;

namespace View.ValidationRules
{
    public class ParameterValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            var parameter = value as Parameter;
            if (!parameter.Type.IsAssignableFrom(value.GetType()))
            {
                return new ValidationResult(false, $"Invalid value type. Expected: " +
                    $"{parameter.Type}");
            }

            return ValidationResult.ValidResult;
        }
    }
}
