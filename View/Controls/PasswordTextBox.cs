using System.Windows;
using System.Windows.Controls;

namespace View.Controls
{
    public class PasswordTextBox : TextBox
    {
        private bool isChangePassword = false;

        public string Password
        {
            get => (string)GetValue(PasswordProperty);
            set => SetValue(PasswordProperty, value);
        }

        public char MaskSymbol
        {
            get => (char)GetValue(MaskSymbolProperty);
            set => SetValue(MaskSymbolProperty, value);
        }

        public static readonly DependencyProperty PasswordProperty =
            DependencyProperty.Register(nameof(Password), typeof(string), typeof(PasswordTextBox),
                new FrameworkPropertyMetadata(string.Empty,
                    FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                    new PropertyChangedCallback(OnPasswordChanged)));

        public static readonly DependencyProperty MaskSymbolProperty =
            DependencyProperty.Register(nameof(MaskSymbol), typeof(char),
                typeof(PasswordTextBox), new FrameworkPropertyMetadata('●'));

        protected override void OnTextChanged(TextChangedEventArgs e)
        {
            base.OnTextChanged(e);
            var caretIndex = CaretIndex;
            if (!isChangePassword)
            {
                foreach (var change in e.Changes)
                {
                    Password = Password.Remove(change.Offset, change.RemovedLength).Insert
                        (change.Offset, Text.Substring(change.Offset, change.AddedLength));
                }
            }
            Text = MaskPassword(MaskSymbol, Text.Length);
            CaretIndex = caretIndex;
        }

        private static void OnPasswordChanged(DependencyObject d,
            DependencyPropertyChangedEventArgs e)
        {
            var control = (PasswordTextBox)d;
            var newText = e.NewValue != null ? (string)e.NewValue : "";
            control.Password = newText;
            control.isChangePassword = true;
            control.Text = MaskPassword(control.MaskSymbol, newText.Length);
            control.isChangePassword = false;
        }

        private static string MaskPassword(char symbol, int count) => new string(symbol, count);
    }
}
