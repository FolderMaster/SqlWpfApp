using System;
using System.Collections.Generic;
using System.Windows;

using ViewModel.VMs.Request;
using View.Implementations;
using View.Implementations.MessageBoxes;
using ViewModel.Interfaces;

namespace View.Windows
{
    public partial class ReportsWindow : Window
    {
        private static ReportsWindow? _instance = null;

        private static Action _call = () =>
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

        public static Action Call => _call;

        public static IDataBaseContextCreator? DataBaseContextCreator { get; set; }

        private ReportsWindow()
        {
            InitializeComponent();

            DataContext = new List<object>()
            {
                new ReportsVM(DataBaseContextCreator, new ErrorMessageBoxService(),
                new PrintDialogService()),
                (Action)(() => _instance = null)
            };
        }
    }
}