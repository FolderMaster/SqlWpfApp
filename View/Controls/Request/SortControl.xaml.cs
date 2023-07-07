using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

using ViewModel.Classes;

namespace View.Controls.Request
{
    /// <summary>
    /// Класс элемента управления для редактирования сортировки с коллекцией сортировки и
    /// коллекцией названий столбцов.
    /// </summary>
    public partial class SortControl : UserControl
    {
        /// <summary>
        /// Возвращает и задаёт коллекцию сортировки.
        /// </summary>
        public ObservableCollection<SortItem> SortCollection
        {
            get => (ObservableCollection<SortItem>)GetValue(SortCollectionProperty);
            set => SetValue(SortCollectionProperty, value);
        }

        /// <summary>
        /// Возвращает и задаёт коллекцию названий столбцов.
        /// </summary>
        public ObservableCollection<string> ColumnNames
        {
            get => (ObservableCollection<string>)GetValue(ColumnNamesProperty);
            set => SetValue(ColumnNamesProperty, value);
        }

        /// <summary>
        /// Свойство зависимости <see cref="SortCollection"/>.
        /// </summary>
        public static DependencyProperty SortCollectionProperty =
            DependencyProperty.Register(nameof(SortCollection),
                typeof(ObservableCollection<SortItem>), typeof(SortControl),
                new FrameworkPropertyMetadata());

        /// <summary>
        /// Свойство зависимости <see cref="ColumnNames"/>.
        /// </summary>
        public static DependencyProperty ColumnNamesProperty =
            DependencyProperty.Register(nameof(ColumnNames), typeof(ObservableCollection<string>),
                typeof(SortControl), new FrameworkPropertyMetadata());

        /// <summary>
        /// Создаёт экземпляр класса <see cref="SortControl"/> по умолчанию.
        /// </summary>
        public SortControl()
        {
            InitializeComponent();
        }
    }
}