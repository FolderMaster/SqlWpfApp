using System;
using System.Collections.Generic;
using System.Windows;

using Model.Dependent;
using Model.Independent;
using ViewModel.VMs.DbSet;
using View.MessageBoxes;

namespace View.Windows.DbSet.Dependent
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
                new ControlDbSetVM<TeacherDisciplineConnection>(messageService),
                new DbSetVM<Teacher>(messageService), new DbSetVM<Discipline>(messageService),
                new DbSetVM<Role>(messageService),
                (Action)(() => _instance = null)
            };
        }
    }
}