using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using Microsoft.Xaml.Behaviors;

using View.ValidationRules;

namespace View.Behaviors
{
    public class DbSetDataGridBehavior : Behavior<DataGrid>
    {
        public Func<string, bool> CreateColumnPredicate
        {
            get => (Func<string, bool>)GetValue(CreateColumnPredicateProperty);
            set => SetValue(CreateColumnPredicateProperty, value);
        }

        public string SelectedPropertyName
        {
            get => (string)GetValue(SelectedPropertyNameProperty);
            set => SetValue(SelectedPropertyNameProperty, value);
        }

        public static DependencyProperty CreateColumnPredicateProperty =
            DependencyProperty.Register(nameof(CreateColumnPredicate), typeof(Func<string, bool>),
                typeof(DbSetDataGridBehavior), new FrameworkPropertyMetadata());

        public static DependencyProperty SelectedPropertyNameProperty =
            DependencyProperty.Register(nameof(SelectedPropertyName), typeof(string),
                typeof(DbSetDataGridBehavior), new FrameworkPropertyMetadata(""));

        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.AutoGeneratingColumn += AutoGeneratingColumn;
            AssociatedObject.SelectedCellsChanged += SelectedCellsChanged;
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            AssociatedObject.AutoGeneratingColumn -= AutoGeneratingColumn;
            AssociatedObject.SelectedCellsChanged -= SelectedCellsChanged;
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

        private void SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            var dataGrid = (DataGrid)sender;
            if (dataGrid.SelectedCells.Count > 0)
            {
                if (dataGrid.SelectedCells[0].Column is DataGridBoundColumn boundColumn)
                {
                    var binding = boundColumn.Binding as Binding;
                    SelectedPropertyName = binding != null ? binding.Path.Path : "";
                }
            }
            else
            {
                SelectedPropertyName = "";
            }
        }
    }
}