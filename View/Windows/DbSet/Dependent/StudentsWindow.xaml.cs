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
    public partial class StudentsWindow : Window
    {
        private static StudentsWindow? _instance = null;

        private static Action _call = () =>
        {
            var instance = Instance;
            instance.Show();
        };

        public static StudentsWindow Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new StudentsWindow();
                }
                return _instance;
            }
        }

        public static Action Call => _call;

        public static IDataBaseContextCreator? DataBaseContextCreator { get; set; }

        public StudentsWindow()
        {
            InitializeComponent();

            var messageService = new ErrorMessageBoxService();

            DataContext = new List<object>()
            {
                new ControlDbSetVM<Student>(DataBaseContextCreator, messageService),
                new DbSetVM<Group>(DataBaseContextCreator, messageService),
                new DbSetVM<Scholarship>(DataBaseContextCreator, messageService),
                new DbSetVM<Person>(DataBaseContextCreator, messageService),
                (Action)(() => _instance = null)
            };
        }
    }
}