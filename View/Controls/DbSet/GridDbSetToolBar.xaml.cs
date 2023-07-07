using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace View.Controls.DbSet
{
    /// <summary>
    /// Класс панели инструментов для таблицы для работы с представлением таблицы базы данных с
    /// текстами поиска и фильтра, командой сохранения.
    /// </summary>
    public partial class GridDbSetToolBar : UserControl
    {
        /// <summary>
        /// Возвращает и задаёт команду сохранения.
        /// </summary>
        public ICommand SaveCommand
        {
            get => (ICommand)GetValue(SaveCommandProperty);
            set => SetValue(SaveCommandProperty, value);
        }

        /// <summary>
        /// Возвращает и задаёт текст поиска.
        /// </summary>
        public string SearchText
        {
            get => (string)GetValue(SearchTextProperty);
            set => SetValue(SearchTextProperty, value);
        }

        /// <summary>
        /// Возвращает и задаёт текст фильтра.
        /// </summary>
        public string FilterText
        {
            get => (string)GetValue(FilterTextProperty);
            set => SetValue(FilterTextProperty, value);
        }

        /// <summary>
        /// Свойство зависимости <see cref="SaveCommand"/>.
        /// </summary>
        public static DependencyProperty SaveCommandProperty =
            DependencyProperty.Register(nameof(SaveCommand), typeof(ICommand),
                typeof(GridDbSetToolBar), new FrameworkPropertyMetadata());

        /// <summary>
        /// Свойство зависимости <see cref="SearchText"/>.
        /// </summary>
        public static DependencyProperty SearchTextProperty =
            DependencyProperty.Register(nameof(SearchText), typeof(string),
                typeof(GridDbSetToolBar), new FrameworkPropertyMetadata());

        /// <summary>
        /// Свойство зависимости <see cref="FilterText"/>.
        /// </summary>
        public static DependencyProperty FilterTextProperty =
            DependencyProperty.Register(nameof(FilterText), typeof(string),
                typeof(GridDbSetToolBar), new FrameworkPropertyMetadata());

        /// <summary>
        /// Создаёт экземпляр класса <see cref="GridDbSetToolBar"/> по умолчанию.
        /// </summary>
        public GridDbSetToolBar()
        {
            InitializeComponent();
        }
    }
}