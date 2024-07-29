using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;

namespace View.Controls.DbSet
{
    /// <summary>
    /// Interaction logic for ToggleContentControl.xaml
    /// </summary>
    [ContentProperty(nameof(InnerContent))]
    public partial class ToggleContentControl : UserControl
    {
        public object InnerContent
        {
            get => GetValue(InnerContentProperty);
            set => SetValue(InnerContentProperty, value);
        }

        public static readonly DependencyProperty InnerContentProperty =
            DependencyProperty.Register(nameof(InnerContent), typeof(object),
                typeof(ToggleContentControl), new PropertyMetadata(null));

        public ToggleContentControl()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            popup.IsOpen = !popup.IsOpen;
        }
    }
}
