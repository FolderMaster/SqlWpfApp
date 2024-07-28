using System.Windows;
using System.Windows.Controls;

using ViewModel.Classes;

namespace View.Controls.DbSet
{
    /// <summary>
    /// Interaction logic for DbStatusBar.xaml
    /// </summary>
    public partial class DbSetStatusBar : UserControl
    {
        public static DependencyProperty TableChangesSetProperty =
            DependencyProperty.Register(nameof(TableChangesSet), typeof(TableChangesSet),
                typeof(DbSetStatusBar), new FrameworkPropertyMetadata());

        public TableChangesSet TableChangesSet
        {
            get => (TableChangesSet)GetValue(TableChangesSetProperty);
            set => SetValue(TableChangesSetProperty, value);
        }

        public DbSetStatusBar()
        {
            InitializeComponent();
        }
    }
}
