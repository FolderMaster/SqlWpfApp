using System;
using System.Collections.Generic;
using System.Windows;

using ViewModel.VMs.Request;
using View.MessageBoxes;
using View.PrintDialogs;

namespace View.Windows
{
    public partial class ReportsWindow : Window
    {
        private static ReportsWindow? _instance = null;

        private static Action _action = () =>
        {
            var instance = Instance;
            instance.Show();
        };

        public static ReportsWindow Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new ReportsWindow();
                }
                return _instance;
            }
        }

        public static Action Action => _action;

        private ReportsWindow()
        {
            InitializeComponent();

            DataContext = new List<object>()
            {
                new ReportsVM(new ErrorMessageBoxService(),
                new PrintDialogService()),
                (Action)(() => _instance = null)
            };
        }
    }
}