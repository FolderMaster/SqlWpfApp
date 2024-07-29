using Microsoft.Xaml.Behaviors;

using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

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
        /// Свойство зависимости <see cref="CreateColumnPredicate"/>.
        /// </summary>
        public static DependencyProperty CreateColumnPredicateProperty =
            DependencyProperty.Register(nameof(CreateColumnPredicate), typeof(Func<string, bool>),
                typeof(DbSetDataGridBehavior), new FrameworkPropertyMetadata());

        /// <summary>
        /// Прикрепляет поведение к элементу управления.
        /// </summary>
        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.AutoGeneratingColumn += AutoGeneratingColumn;
        }

        /// <summary>
        /// Открепляет поведение к элементу управления.
        /// </summary>
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
                }
                else
                {
                    e.Column = null;
                }
            }
        }
    }
}