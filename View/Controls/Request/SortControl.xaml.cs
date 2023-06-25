using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

using ViewModel.Classes;

namespace View.Controls.Request
{
    public partial class SortControl : UserControl
    {
        public ObservableCollection<SortItem> SortCollection
        {
            get => (ObservableCollection<SortItem>)GetValue(SortCollectionProperty);
            set => SetValue(SortCollectionProperty, value);
        }

        public ObservableCollection<string> ColumnNames
        {
            get => (ObservableCollection<string>)GetValue(ColumnNamesProperty);
            set => SetValue(ColumnNamesProperty, value);
        }

        public static DependencyProperty SortCollectionProperty =
            DependencyProperty.Register(nameof(SortCollection),
                typeof(ObservableCollection<SortItem>), typeof(SortControl),
                new FrameworkPropertyMetadata());

        public static DependencyProperty ColumnNamesProperty =
            DependencyProperty.Register(nameof(ColumnNames), typeof(ObservableCollection<string>),
                typeof(SortControl), new FrameworkPropertyMetadata());

        public SortControl()
        {
            InitializeComponent();
        }
    }
}