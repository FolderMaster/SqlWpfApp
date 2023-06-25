using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

using Model.Independent;
using ViewModel.Services;

namespace View.Controls.DbSet
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

        public IMessageService MessageService
        {
            get => (IMessageService)GetValue(MessageServiceProperty);
            set => SetValue(MessageServiceProperty, value);
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

        public static DependencyProperty MessageServiceProperty = DependencyProperty.Register
            (nameof(MessageService), typeof(IMessageService), typeof(PassportControl),
            new FrameworkPropertyMetadata());

        public PassportControl()
        {
            InitializeComponent();
        }
    }
}