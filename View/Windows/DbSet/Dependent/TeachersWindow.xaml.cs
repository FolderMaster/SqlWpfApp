using System;
using System.Collections.Generic;
using System.Windows;

using Model.Dependent;
using Model.Independent;
using ViewModel.VMs.DbSet;
using View.Implementations.MessageBoxes;
using ViewModel.Interfaces;

namespace View.Windows.DbSet.Dependent
{
    public partial class TeachersWindow : Window
    {
        private static TeachersWindow? _instance = null;

        private static Action _call = () =>
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

        public static Action Call => _call;

        public static IDataBaseContextCreator? DataBaseContextCreator { get; set; }

        public TeachersWindow()
        {
            InitializeComponent();

            var messageService = new ErrorMessageBoxService();

            DataContext = new List<object>()
            {
                new ControlDbSetVM<Teacher>(DataBaseContextCreator, messageService),
                new DbSetVM<Department>(DataBaseContextCreator, messageService),
                new DbSetVM<Position>(DataBaseContextCreator, messageService),
                new DbSetVM<Person>(DataBaseContextCreator, messageService),
                (Action)(() => _instance = null)
            };
        }
    }
}