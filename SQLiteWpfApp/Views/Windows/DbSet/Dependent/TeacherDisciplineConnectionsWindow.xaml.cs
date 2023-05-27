using System;
using System.Collections.Generic;
using System.Windows;

using SQLiteWpfApp.Models.Dependent;
using SQLiteWpfApp.Models.Independent;
using SQLiteWpfApp.ViewModels.VMs;
using SQLiteWpfApp.Views.MessageBoxes;

namespace SQLiteWpfApp.Views.Windows.DbSet.Dependent
{
    public partial class TeacherDisciplineConnectionsWindow : Window
    {
        private static TeacherDisciplineConnectionsWindow? _instance = null;

        private static Action _action = () =>
        {
            var instance = Instance;
            instance.Show();
        };

        public static TeacherDisciplineConnectionsWindow Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new TeacherDisciplineConnectionsWindow();
                }
                return _instance;
            }
        }

        public static Action Action => _action;

        public TeacherDisciplineConnectionsWindow()
        {
            InitializeComponent();

            var messageService = new ErrorMessageBoxService();

            DataContext = new List<object>()
            {
                new ControlDbSetVM<TeacherDisciplineConnection>(messageService,
                () => _instance = null), new DbSetVM<Teacher>(messageService),
                new DbSetVM<Discipline>(messageService), new DbSetVM<Role>(messageService)
            };
        }
    }
}