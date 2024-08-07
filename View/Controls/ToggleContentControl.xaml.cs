using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;

namespace View.Controls
{
    /// <summary>
    /// Interaction logic for ToggleContentControl.xaml
    /// </summary>
    [ContentProperty(nameof(InnerContent))]
    public partial class ToggleContentControl : UserControl
    {
        public static readonly DependencyProperty InnerContentProperty =
            DependencyProperty.Register(nameof(InnerContent), typeof(object),
                typeof(ToggleContentControl));

        public static readonly DependencyProperty ButtonContentProperty =
            DependencyProperty.Register(nameof(ButtonContent), typeof(object),
                typeof(ToggleContentControl));

        public object InnerContent
        {
            get => GetValue(InnerContentProperty);
            set => SetValue(InnerContentProperty, value);
        }

        public object ButtonContent
        {
            get => GetValue(ButtonContentProperty);
            set => SetValue(ButtonContentProperty, value);
        }

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
