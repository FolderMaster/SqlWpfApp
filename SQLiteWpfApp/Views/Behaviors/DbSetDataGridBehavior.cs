using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using Microsoft.Xaml.Behaviors;

using SQLiteWpfApp.Views.ValidationRules;

namespace SQLiteWpfApp.Views.Behaviors
{
    public class DbSetDataGridBehavior : Behavior<DataGrid>
    {
        public Func<string, bool> CreateColumnPredicate
        {
            get => (Func<string, bool>)GetValue(CreateColumnPredicateProperty);
            set => SetValue(CreateColumnPredicateProperty, value);
        }

        public static DependencyProperty CreateColumnPredicateProperty =
            DependencyProperty.Register(nameof(CreateColumnPredicate), typeof(Func<string, bool>),
                typeof(DbSetDataGridBehavior), new FrameworkPropertyMetadata());

        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.AutoGeneratingColumn += AutoGeneratingColumn;
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            AssociatedObject.AutoGeneratingColumn -= AutoGeneratingColumn;
        }

        private void AutoGeneratingColumn(object? sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.Column is DataGridBoundColumn column)
            {
                if (CreateColumnPredicate == null || CreateColumnPredicate(e.PropertyName))
                {
                    column.Header = Application.Current.Resources[e.PropertyName + "Header"]
                        as string;
                    var columnBinding = column.Binding as Binding;
                    columnBinding.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
                    columnBinding.ValidationRules.Add(new NotEmptyValidationRule());
                }
                else
                {
                    e.Column = null;
                }
            }
        }
    }
}