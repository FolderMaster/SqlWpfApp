using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace SQLiteWpfApp.Views.Controls
{
    public partial class GridDbSetToolBar : UserControl
    {
        public ICommand SaveCommand
        {
            get => (ICommand)GetValue(SaveCommandProperty);
            set => SetValue(SaveCommandProperty, value);
        }

        public string SearchText
        {
            get => (string)GetValue(SearchTextProperty);
            set => SetValue(SearchTextProperty, value);
        }

        public bool IsFilter
        {
            get => (bool)GetValue(IsFilterProperty);
            set => SetValue(IsFilterProperty, value);
        }

        public static DependencyProperty SaveCommandProperty =
            DependencyProperty.Register(nameof(SaveCommand), typeof(ICommand),
                typeof(GridDbSetToolBar), new FrameworkPropertyMetadata());

        public static DependencyProperty SearchTextProperty =
            DependencyProperty.Register(nameof(SearchText), typeof(string),
                typeof(GridDbSetToolBar), new FrameworkPropertyMetadata());

        public static DependencyProperty IsFilterProperty =
            DependencyProperty.Register(nameof(IsFilter), typeof(bool),
                typeof(GridDbSetToolBar), new FrameworkPropertyMetadata());

        public GridDbSetToolBar()
        {
            InitializeComponent();
        }
    }
}