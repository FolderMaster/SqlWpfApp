using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

using ViewModel.Classes;

namespace View.Controls.DbSet
{
    /// <summary>
    /// Класс панели инструментов для элемента управления для работы с представлением таблицы базы
    /// данных с текстами поиска и фильтра, количеством элементов финального локального
    /// представления таблицы из базы данных и командами переходов к первому, предыдущему,
    /// следующему и последнему элементу, добавления и удаления элемента, сохранения.
    /// </summary>
    public partial class ControlDbSetToolBar : UserControl
    {
        /// <summary>
        /// Возвращает и задаёт команду перехода к первому элементу.
        /// </summary>
        public ICommand FirstCommand
        {
            get => (ICommand)GetValue(FirstCommandProperty);
            set => SetValue(FirstCommandProperty, value);
        }

        /// <summary>
        /// Возвращает и задаёт команду перехода к предыдущему элементу.
        /// </summary>
        public ICommand BackCommand
        {
            get => (ICommand)GetValue(BackCommandProperty);
            set => SetValue(BackCommandProperty, value);
        }

        /// <summary>
        /// Возвращает и задаёт выбранный номер.
        /// </summary>
        public int SelectedNumber
        {
            get => (int)GetValue(SelectedNumberProperty);
            set => SetValue(SelectedNumberProperty, value);
        }

        /// <summary>
        /// Возвращает и задаёт количество элементов финального локального представления таблицы из
        /// базы данных.
        /// </summary>
        public int Count
        {
            get => (int)GetValue(CountProperty);
            set => SetValue(CountProperty, value);
        }

        /// <summary>
        /// Возвращает и задаёт команду перехода к следующему элементу.
        /// </summary>
        public ICommand NextCommand
        {
            get => (ICommand)GetValue(NextCommandProperty);
            set => SetValue(NextCommandProperty, value);
        }

        /// <summary>
        /// Возвращает и задаёт команду перехода к последнему элементу.
        /// </summary>
        public ICommand LastCommand
        {
            get => (ICommand)GetValue(LastCommandProperty);
            set => SetValue(LastCommandProperty, value);
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
        /// Возвращает и задаёт команду добавления элемента.
        /// </summary>
        public ICommand AddCommand
        {
            get => (ICommand)GetValue(AddCommandProperty);
            set => SetValue(AddCommandProperty, value);
        }

        /// <summary>
        /// Возвращает и задаёт команду удаления элемента.
        /// </summary>
        public ICommand RemoveCommand
        {
            get => (ICommand)GetValue(RemoveCommandProperty);
            set => SetValue(RemoveCommandProperty, value);
        }

        /// <summary>
        /// Возвращает и задаёт команду сохранения.
        /// </summary>
        public ICommand SaveCommand
        {
            get => (ICommand)GetValue(SaveCommandProperty);
            set => SetValue(SaveCommandProperty, value);
        }

        /// <summary>
        /// Свойство зависимости <see cref="FirstCommand"/>.
        /// </summary>
        public static DependencyProperty FirstCommandProperty =
            DependencyProperty.Register(nameof(FirstCommand), typeof(ICommand),
                typeof(ControlDbSetToolBar), new FrameworkPropertyMetadata());

        /// <summary>
        /// Свойство зависимости <see cref="BackCommand"/>.
        /// </summary>
        public static DependencyProperty BackCommandProperty =
            DependencyProperty.Register(nameof(BackCommand), typeof(ICommand),
                typeof(ControlDbSetToolBar), new FrameworkPropertyMetadata());

        /// <summary>
        /// Свойство зависимости <see cref="SelectedNumber"/>.
        /// </summary>
        public static DependencyProperty SelectedNumberProperty =
            DependencyProperty.Register(nameof(SelectedNumber), typeof(int),
                typeof(ControlDbSetToolBar), new FrameworkPropertyMetadata());

        /// <summary>
        /// Свойство зависимости <see cref="Count"/>.
        /// </summary>
        public static DependencyProperty CountProperty =
            DependencyProperty.Register(nameof(Count), typeof(int),
                typeof(ControlDbSetToolBar), new FrameworkPropertyMetadata());

        /// <summary>
        /// Свойство зависимости <see cref="NextCommand"/>.
        /// </summary>
        public static DependencyProperty NextCommandProperty =
            DependencyProperty.Register(nameof(NextCommand), typeof(ICommand),
                typeof(ControlDbSetToolBar), new FrameworkPropertyMetadata());

        /// <summary>
        /// Свойство зависимости <see cref="LastCommand"/>.
        /// </summary>
        public static DependencyProperty LastCommandProperty =
            DependencyProperty.Register(nameof(LastCommand), typeof(ICommand),
                typeof(ControlDbSetToolBar), new FrameworkPropertyMetadata());

        /// <summary>
        /// Свойство зависимости <see cref="SearchText"/>.
        /// </summary>
        public static DependencyProperty SearchTextProperty =
            DependencyProperty.Register(nameof(SearchText), typeof(string),
                typeof(ControlDbSetToolBar), new FrameworkPropertyMetadata());

        /// <summary>
        /// Свойство зависимости <see cref="FilterText"/>.
        /// </summary>
        public static DependencyProperty FilterTextProperty =
            DependencyProperty.Register(nameof(FilterText), typeof(string),
                typeof(ControlDbSetToolBar), new FrameworkPropertyMetadata());

        /// <summary>
        /// Свойство зависимости <see cref="AddCommand"/>.
        /// </summary>
        public static DependencyProperty AddCommandProperty =
            DependencyProperty.Register(nameof(AddCommand), typeof(ICommand),
                typeof(ControlDbSetToolBar), new FrameworkPropertyMetadata());

        /// <summary>
        /// Свойство зависимости <see cref="RemoveCommand"/>.
        /// </summary>
        public static DependencyProperty RemoveCommandProperty =
            DependencyProperty.Register(nameof(RemoveCommand), typeof(ICommand),
                typeof(ControlDbSetToolBar), new FrameworkPropertyMetadata());

        /// <summary>
        /// Свойство зависимости <see cref="SaveCommand"/>.
        /// </summary>
        public static DependencyProperty SaveCommandProperty =
            DependencyProperty.Register(nameof(SaveCommand), typeof(ICommand),
                typeof(ControlDbSetToolBar), new FrameworkPropertyMetadata());

        /// <summary>
        /// Создаёт экземпляр класса <see cref="ControlDbSetToolBar"/> по умолчанию.
        /// </summary>
        public ControlDbSetToolBar()
        {
            InitializeComponent();
        }
    }
}