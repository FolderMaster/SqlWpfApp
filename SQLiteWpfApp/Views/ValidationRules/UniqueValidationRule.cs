using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Media;
using System.Collections.Specialized;
using System.Collections;
using System.Windows.Controls.Primitives;
using System.Collections.Generic;
using System.Windows.Markup;

namespace SQLiteWpfApp.Views.ValidationRules
{
    [ContentProperty(nameof(UniqueValidationRuleDependencyObject))]
    public class UniqueValidationRule : ValidationRule
    { 
        public UniqueValidationRuleDependencyObject UniqueValidationRuleDependencyObject
            { get; set; }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            //var property = Type.GetProperty(PropertyName as string);
            //foreach(var item in Collection)
            //{
            //    if(item != SelectedItem)
            //    {
            //        if(property.GetValue(item) != value)
            //        {
            //            return new ValidationResult(false, "Value can be unique!");
            //        }
            //    }
            //}
            return ValidationResult.ValidResult;
        }
    }
}