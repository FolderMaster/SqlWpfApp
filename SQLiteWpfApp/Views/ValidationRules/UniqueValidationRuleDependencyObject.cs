using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;

namespace SQLiteWpfApp.Views.ValidationRules
{
    public class UniqueValidationRuleDependencyObject : DependencyObject
    {
        public string PropertyName
        {
            get => (string)GetValue(PropertyNameProperty);
            set => SetValue(PropertyNameProperty, value);
        }

        public Type Type
        {
            get => (Type)GetValue(TypeProperty);
            set => SetValue(TypeProperty, value);
        }

        public object SelectedItem
        {
            get => (object)GetValue(SelectedItemProperty);
            set => SetValue(SelectedItemProperty, value);
        }

        public ObservableCollection<object> Items
        {
            get => (ObservableCollection<object>)GetValue(ItemsProperty);
            set => SetValue(ItemsProperty, value);
        }

        public static DependencyProperty PropertyNameProperty { get; set; } =
            DependencyProperty.Register("PropertyName", typeof(string),
                typeof(UniqueValidationRule), new UIPropertyMetadata(null));

        public static DependencyProperty TypeProperty { get; set; } =
            DependencyProperty.Register("Type", typeof(Type),
                typeof(UniqueValidationRule), new UIPropertyMetadata(null));

        public static DependencyProperty SelectedItemProperty { get; private set; } =
            DependencyProperty.Register("SelectedItem", typeof(object),
                typeof(UniqueValidationRule), new UIPropertyMetadata(null));

        public static DependencyProperty ItemsProperty { get; private set; } =
            DependencyProperty.Register("Items", typeof(ObservableCollection<object>),
                typeof(UniqueValidationRule), new UIPropertyMetadata(new List<object>()));
    }
}