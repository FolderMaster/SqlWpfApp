using Microsoft.Xaml.Behaviors;

using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

using View.ValidationRules;

namespace View.Behaviors
{
    /// <summary>
    /// Класс поведения <see cref="DataGrid"/> для представления таблицы из базы данных.
    /// </summary>
    public class DbSetDataGridBehavior : Behavior<DataGrid>
    {
        /// <summary>
        /// Возвращает и задаёт предикат создания столбцов.
        /// </summary>
        public Func<string, bool> CreateColumnPredicate
        {
            get => (Func<string, bool>)GetValue(CreateColumnPredicateProperty);
            set => SetValue(CreateColumnPredicateProperty, value);
        }

        /// <summary>
        /// Возвращает и задаёт названия выбранного свойства.
        /// </summary>
        public string SelectedPropertyName
        {
            get => (string)GetValue(SelectedPropertyNameProperty);
            set => SetValue(SelectedPropertyNameProperty, value);
        }

        /// <summary>
        /// Свойство зависимости <see cref="CreateColumnPredicate"/>.
        /// </summary>
        public static DependencyProperty CreateColumnPredicateProperty =
            DependencyProperty.Register(nameof(CreateColumnPredicate), typeof(Func<string, bool>),
                typeof(DbSetDataGridBehavior), new FrameworkPropertyMetadata());

        /// <summary>
        /// Свойство зависимости <see cref="SelectedPropertyName"/>.
        /// </summary>
        public static DependencyProperty SelectedPropertyNameProperty =
            DependencyProperty.Register(nameof(SelectedPropertyName), typeof(string),
                typeof(DbSetDataGridBehavior), new FrameworkPropertyMetadata(""));

        /// <summary>
        /// Прикрепляет поведение к элементу управления.
        /// </summary>
        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.AutoGeneratingColumn += AutoGeneratingColumn;
            AssociatedObject.SelectedCellsChanged += SelectedCellsChanged;
        }

        /// <summary>
        /// Открепляет поведение к элементу управления.
        /// </summary>
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