using System;
using System.Windows;
using System.Collections.Generic;

using ViewModel.VMs.DbSet;
using View.Implementations.FileDialogs;
using View.Implementations.MessageBoxes;
using ViewModel.Interfaces;

namespace View.Windows.DbSet.Independent
{
    public partial class PassportsWindow : Window
    {
        private static PassportsWindow? _instance = null;

        private static Action _call = () =>
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

        public static Action Call => _call;

        public static IDataBaseContextCreator? DataBaseContextCreator { get; set; }

        private PassportsWindow()
        {
            InitializeComponent();

            DataContext = new List<object>()
            {
                new PassportsVM(DataBaseContextCreator, new ErrorMessageBoxService(),
                new OpenFileDialogService("Images (*.png;*.jpeg;*.jpg)|*.png;*.jpeg;*.jpg"),
                new SaveFileDialogService("PNG image|*.png|JPEG image|*.jpeg|JPG image|*.jpg")),
                (Action)(() => _instance = null)
            };
        }
    }
}