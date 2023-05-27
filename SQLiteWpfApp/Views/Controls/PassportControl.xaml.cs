using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

using SQLiteWpfApp.Models.Independent;

namespace SQLiteWpfApp.Views.Controls
{
    public partial class PassportControl : UserControl
    {
        public Passport Passport
        {
            get => (Passport)GetValue(PassportProperty);
            set => SetValue(PassportProperty, value);
        }

        public bool IsReadOnly
        {
            get => (bool)GetValue(IsReadOnlyProperty);
            set => SetValue(IsReadOnlyProperty, value);
        }

        public ICommand LoadImageCommand
        {
            get => (ICommand)GetValue(LoadImageCommandProperty);
            set => SetValue(LoadImageCommandProperty, value);
        }

        public ICommand SaveImageCommand
        {
            get => (ICommand)GetValue(SaveImageCommandProperty);
            set => SetValue(SaveImageCommandProperty, value);
        }

        public static DependencyProperty PassportProperty =
            DependencyProperty.Register(nameof(Passport), typeof(Passport),
                typeof(PassportControl), new FrameworkPropertyMetadata());

        public static DependencyProperty IsReadOnlyProperty =
            DependencyProperty.Register(nameof(IsReadOnly), typeof(bool),
                typeof(PassportControl), new FrameworkPropertyMetadata());

        public static DependencyProperty LoadImageCommandProperty =
            DependencyProperty.Register(nameof(LoadImageCommand), typeof(ICommand),
                typeof(PassportControl), new FrameworkPropertyMetadata());

        public static DependencyProperty SaveImageCommandProperty =
            DependencyProperty.Register(nameof(SaveImageCommand), typeof(ICommand),
                typeof(PassportControl), new FrameworkPropertyMetadata());

        public PassportControl()
        {
            InitializeComponent();
        }
    }
}