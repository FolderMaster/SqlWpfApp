﻿using System;
using System.Windows;
using System.Windows.Controls;

namespace View.Controls
{
    public class MaskedTextBox : TextBox
    {
        public bool IsMasked
        {
            get => (bool)GetValue(IsMaskedProperty);
            private set => SetValue(IsMaskedProperty, value);
        }

        public string OriginalText
        {
            get => (string)GetValue(OriginalTextProperty);
            set => SetValue(OriginalTextProperty, value);
        }

        public Func<string, string> MaskPredicate
        {
            get => (Func<string, string>)GetValue(MaskPredicateProperty);
            set => SetValue(MaskPredicateProperty, value);
        }

        public static readonly DependencyProperty IsMaskedProperty =
            DependencyProperty.Register(nameof(IsMasked), typeof(bool),
                typeof(MaskedTextBox), new FrameworkPropertyMetadata(true));

        public static readonly DependencyProperty OriginalTextProperty =
            DependencyProperty.Register(nameof(OriginalText), typeof(string),
                typeof(MaskedTextBox), new FrameworkPropertyMetadata(string.Empty,
                    FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                    new PropertyChangedCallback(OnOriginalTextChanged)));

        public static readonly DependencyProperty MaskPredicateProperty =
            DependencyProperty.Register(nameof(MaskPredicate), typeof(Func<string, string>),
                typeof(MaskedTextBox), new FrameworkPropertyMetadata((string text) =>
                    new string('●', text.Length)));

        public MaskedTextBox() : base()
        {
            GotFocus += MaskedTextBox_GotFocus;
            LostFocus += MaskedTextBox_LostFocus;
            TextChanged += MaskedTextBox_TextChanged;
        }

        private void MaskedTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(!IsMasked)
            {
                OriginalText = Text;
            }
        }

        private void MaskedTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            IsMasked = true;
            Text = MaskPredicate(OriginalText);
        }

        private void MaskedTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            IsMasked = false;
            Text = OriginalText;
        }

        private static void OnOriginalTextChanged(DependencyObject d,
            DependencyPropertyChangedEventArgs e)
        {
            var control = (MaskedTextBox)d;
            var newText = (string)e.NewValue;
            if (control.IsMasked)
            {
                control.Text = control.MaskPredicate(newText);
            }
            else
            {
                control.Text = newText;
            }
        }
    }
}