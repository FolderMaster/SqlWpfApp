using System;
using System.Collections.Generic;
using System.Windows;

using SQLiteWpfApp.ViewModels.VMs.Request;
using SQLiteWpfApp.Views.MessageBoxes;
using SQLiteWpfApp.Views.PrintDialogs;

namespace SQLiteWpfApp.Views.Windows
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
                new ReportsVM(new ErrorMessageBoxService(), new PrintDialogService("Print document!")),
                (Action)(() => _instance = null)
            };
        }
    }
}