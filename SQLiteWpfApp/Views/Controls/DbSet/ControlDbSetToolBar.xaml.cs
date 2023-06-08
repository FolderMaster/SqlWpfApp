using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace SQLiteWpfApp.Views.Controls.DbSet
{
    public partial class ControlDbSetToolBar : UserControl
    {
        public ICommand FirstCommand
        {
            get => (ICommand)GetValue(FirstCommandProperty);
            set => SetValue(FirstCommandProperty, value);
        }

        public ICommand BackCommand
        {
            get => (ICommand)GetValue(BackCommandProperty);
            set => SetValue(BackCommandProperty, value);
        }

        public int SelectedNumber
        {
            get => (int)GetValue(SelectedNumberProperty);
            set => SetValue(SelectedNumberProperty, value);
        }

        public int Count
        {
            get => (int)GetValue(CountProperty);
            set => SetValue(CountProperty, value);
        }

        public ICommand NextCommand
        {
            get => (ICommand)GetValue(NextCommandProperty);
            set => SetValue(NextCommandProperty, value);
        }

        public ICommand LastCommand
        {
            get => (ICommand)GetValue(LastCommandProperty);
            set => SetValue(LastCommandProperty, value);
        }

        public string SearchText
        {
            get => (string)GetValue(SearchTextProperty);
            set => SetValue(SearchTextProperty, value);
        }

        public string FilterText
        {
            get => (string)GetValue(FilterTextProperty);
            set => SetValue(FilterTextProperty, value);
        }

        public ICommand AddCommand
        {
            get => (ICommand)GetValue(AddCommandProperty);
            set => SetValue(AddCommandProperty, value);
        }

        public ICommand RemoveCommand
        {
            get => (ICommand)GetValue(RemoveCommandProperty);
            set => SetValue(RemoveCommandProperty, value);
        }

        public ICommand SaveCommand
        {
            get => (ICommand)GetValue(SaveCommandProperty);
            set => SetValue(SaveCommandProperty, value);
        }

        public static DependencyProperty FirstCommandProperty =
            DependencyProperty.Register(nameof(FirstCommand), typeof(ICommand),
                typeof(ControlDbSetToolBar), new FrameworkPropertyMetadata());

        public static DependencyProperty BackCommandProperty =
            DependencyProperty.Register(nameof(BackCommand), typeof(ICommand),
                typeof(ControlDbSetToolBar), new FrameworkPropertyMetadata());

        public static DependencyProperty SelectedNumberProperty =
            DependencyProperty.Register(nameof(SelectedNumber), typeof(int),
                typeof(ControlDbSetToolBar), new FrameworkPropertyMetadata());

        public static DependencyProperty CountProperty =
            DependencyProperty.Register(nameof(Count), typeof(int),
                typeof(ControlDbSetToolBar), new FrameworkPropertyMetadata());

        public static DependencyProperty NextCommandProperty =
            DependencyProperty.Register(nameof(NextCommand), typeof(ICommand),
                typeof(ControlDbSetToolBar), new FrameworkPropertyMetadata());

        public static DependencyProperty LastCommandProperty =
            DependencyProperty.Register(nameof(LastCommand), typeof(ICommand),
                typeof(ControlDbSetToolBar), new FrameworkPropertyMetadata());

        public static DependencyProperty SearchTextProperty =
            DependencyProperty.Register(nameof(SearchText), typeof(string),
                typeof(ControlDbSetToolBar), new FrameworkPropertyMetadata());

        public static DependencyProperty FilterTextProperty =
            DependencyProperty.Register(nameof(FilterText), typeof(string),
                typeof(ControlDbSetToolBar), new FrameworkPropertyMetadata());

        public static DependencyProperty AddCommandProperty =
            DependencyProperty.Register(nameof(AddCommand), typeof(ICommand),
                typeof(ControlDbSetToolBar), new FrameworkPropertyMetadata());

        public static DependencyProperty RemoveCommandProperty =
            DependencyProperty.Register(nameof(RemoveCommand), typeof(ICommand),
                typeof(ControlDbSetToolBar), new FrameworkPropertyMetadata());

        public static DependencyProperty SaveCommandProperty =
            DependencyProperty.Register(nameof(SaveCommand), typeof(ICommand),
                typeof(ControlDbSetToolBar), new FrameworkPropertyMetadata());

        public ControlDbSetToolBar()
        {
            InitializeComponent();
        }
    }
}