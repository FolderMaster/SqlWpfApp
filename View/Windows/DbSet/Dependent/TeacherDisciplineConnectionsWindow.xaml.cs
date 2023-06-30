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
    public partial class TeacherDisciplineConnectionsWindow : Window
    {
        private static TeacherDisciplineConnectionsWindow? _instance = null;

        private static Action _call = () =>
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

        public static Action Call => _call;

        public static IDataBaseContextCreator? DataBaseContextCreator { get; set; }

        public TeacherDisciplineConnectionsWindow()
        {
            InitializeComponent();

            var messageService = new ErrorMessageBoxService();

            DataContext = new List<object>()
            {
                new ControlDbSetVM<TeacherDisciplineConnection>(DataBaseContextCreator, messageService),
                new DbSetVM<Teacher>(DataBaseContextCreator, messageService),
                new DbSetVM<Discipline>(DataBaseContextCreator,messageService),
                new DbSetVM<Role>(DataBaseContextCreator, messageService),
                (Action)(() => _instance = null)
            };
        }
    }
}