using Microsoft.Xaml.Behaviors;

using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

using ViewModel.Interfaces.Services;

namespace View.Behaviors
{
    /// <summary>
    /// Класс поведения <see cref="DataGrid"/> для представления таблицы из базы данных.
    /// </summary>
    public class DbSetDataGridBehavior : Behavior<DataGrid>
    {
        /// <summary>
        /// Возвращает и задаёт сервис ресурсов.
        /// </summary>
        public IResourceService ResourceService
        {
            get => (IResourceService)GetValue(ResourceServiceProperty);
            set => SetValue(ResourceServiceProperty, value);
        }

        public IEnumerable<string> Properties
        {
            get => (IEnumerable<string>)GetValue(PropertiesProperty);
            set => SetValue(PropertiesProperty, value);
        }

        /// <summary>
        /// Свойство зависимости <see cref="ResourceServicePredicate"/>.
        /// </summary>
        public static DependencyProperty ResourceServiceProperty =
            DependencyProperty.Register(nameof(ResourceService), typeof(IResourceService),
                typeof(DbSetDataGridBehavior));

        public static DependencyProperty PropertiesProperty =
            DependencyProperty.Register(nameof(Properties), typeof(IEnumerable<string>),
                typeof(DbSetDataGridBehavior));

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
            if (Properties.Contains(e.PropertyName))
            {
                if (e.Column is DataGridBoundColumn column)
                {
                    column.Header = ResourceService.GetString(e.PropertyName + "Header");
                    var columnBinding = column.Binding as Binding;
                    columnBinding.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
                }
            }
            else
            {
                e.Cancel = true;
            }
        }
    }
}