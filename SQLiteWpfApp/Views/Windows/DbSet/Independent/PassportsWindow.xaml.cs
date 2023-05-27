using System;
using System.Windows;

using SQLiteWpfApp.ViewModels.VMs;
using SQLiteWpfApp.Views.MessageBoxes;
using SQLiteWpfApp.Views.FileDialogs;

namespace SQLiteWpfApp.Views.Windows.DbSet.Independent
{
    public partial class PassportsWindow : Window
    {
        private static PassportsWindow? _instance = null;

        private static Action _action = () =>
        {
            var instance = Instance;
            instance.Show();
        };

        public static PassportsWindow Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new PassportsWindow();
                }
                return _instance;
            }
        }

        public static Action Action => _action;

        private PassportsWindow()
        {
            InitializeComponent();

            DataContext = new PassportsVM(new ErrorMessageBoxService(), () => _instance = null,
                new OpenFileDialogService("Images (*.png;*.jpeg;*.jpg)|*.png;*.jpeg;*.jpg"),
                new SaveFileDialogService("PNG image|*.png|JPEG image|*.jpeg|JPG image|*.jpg"));
        }
    }
}