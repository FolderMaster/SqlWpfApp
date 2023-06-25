using System;
using System.Windows;
using System.Collections.Generic;

using ViewModel.VMs.DbSet;
using View.MessageBoxes;
using View.FileDialogs;

namespace View.Windows.DbSet.Independent
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

            DataContext = new List<object>()
            {
                new PassportsVM(new ErrorMessageBoxService(),
                new OpenFileDialogService("Images (*.png;*.jpeg;*.jpg)|*.png;*.jpeg;*.jpg"),
                new SaveFileDialogService("PNG image|*.png|JPEG image|*.jpeg|JPG image|*.jpg")),
                (Action)(() => _instance = null)
            };
        }
    }
}