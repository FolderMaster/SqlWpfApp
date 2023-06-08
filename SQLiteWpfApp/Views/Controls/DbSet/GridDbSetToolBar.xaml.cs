using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace SQLiteWpfApp.Views.Controls.DbSet
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

        public string FilterText
        {
            get => (string)GetValue(FilterTextProperty);
            set => SetValue(FilterTextProperty, value);
        }

        public static DependencyProperty SaveCommandProperty =
            DependencyProperty.Register(nameof(SaveCommand), typeof(ICommand),
                typeof(GridDbSetToolBar), new FrameworkPropertyMetadata());

        public static DependencyProperty SearchTextProperty =
            DependencyProperty.Register(nameof(SearchText), typeof(string),
                typeof(GridDbSetToolBar), new FrameworkPropertyMetadata());

        public static DependencyProperty FilterTextProperty =
            DependencyProperty.Register(nameof(FilterText), typeof(string),
                typeof(GridDbSetToolBar), new FrameworkPropertyMetadata());

        public GridDbSetToolBar()
        {
            InitializeComponent();
        }
    }
}