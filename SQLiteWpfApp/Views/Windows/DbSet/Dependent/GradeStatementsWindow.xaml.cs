using System;
using System.Collections.Generic;
using System.Windows;

using SQLiteWpfApp.Models.Dependent;
using SQLiteWpfApp.ViewModels.VMs;
using SQLiteWpfApp.Views.MessageBoxes;

namespace SQLiteWpfApp.Views.Windows.DbSet.Dependent
{
    public partial class GradeStatementsWindow : Window
    {
        private static GradeStatementsWindow? _instance = null;

        private static Action _action = () =>
        {
            var instance = Instance;
            instance.Show();
        };

        public static GradeStatementsWindow Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new GradeStatementsWindow();
                }
                return _instance;
            }
        }

        public static Action Action => _action;

        public GradeStatementsWindow()
        {
            InitializeComponent();

            var messageService = new ErrorMessageBoxService();

            DataContext = new List<object>()
            {
                new ControlDbSetVM<GradeStatement>(messageService, () => _instance = null),
                new DbSetVM<Discipline>(messageService), new DbSetVM<Student>(messageService),
                new DbSetVM<Teacher>(messageService), new DbSetVM<Grade>(messageService)
            };
        }
    }
}