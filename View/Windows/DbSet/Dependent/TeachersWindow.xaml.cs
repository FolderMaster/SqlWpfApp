using System;
using System.Collections.Generic;
using System.Windows;

using Model.Dependent;
using Model.Independent;
using ViewModel.VMs.DbSet;
using View.MessageBoxes;

namespace View.Windows.DbSet.Dependent
{
    public partial class TeachersWindow : Window
    {
        private static TeachersWindow? _instance = null;

        private static Action _action = () =>
        {
            var instance = Instance;
            instance.Show();
        };

        public static TeachersWindow Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new TeachersWindow();
                }
                return _instance;
            }
        }

        public static Action Action => _action;

        public TeachersWindow()
        {
            InitializeComponent();

            var messageService = new ErrorMessageBoxService();

            DataContext = new List<object>()
            {
                new ControlDbSetVM<Teacher>(messageService),
                new DbSetVM<Department>(messageService), new DbSetVM<Position>(messageService),
                new DbSetVM<Person>(messageService),
                (Action)(() => _instance = null)
            };
        }
    }
}