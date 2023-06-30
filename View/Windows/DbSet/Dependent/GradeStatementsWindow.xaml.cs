using System;
using System.Collections.Generic;
using System.Windows;

using Model.Dependent;
using ViewModel.VMs.DbSet;
using View.Implementations.MessageBoxes;
using ViewModel.Interfaces;

namespace View.Windows.DbSet.Dependent
{
    public partial class GradeStatementsWindow : Window
    {
        private static GradeStatementsWindow? _instance = null;

        private static Action _call = () =>
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

        public static IDataBaseContextCreator? DataBaseContextCreator { get; set; }

        public static Action Call => _call;

        public GradeStatementsWindow()
        {
            InitializeComponent();

            var messageService = new ErrorMessageBoxService();

            DataContext = new List<object>()
            {
                new ControlDbSetVM<GradeStatement>(DataBaseContextCreator, messageService),
                new DbSetVM<Discipline>(DataBaseContextCreator, messageService),
                new DbSetVM<Student>(DataBaseContextCreator, messageService),
                new DbSetVM<Teacher>(DataBaseContextCreator, messageService),
                new DbSetVM<Grade>(DataBaseContextCreator, messageService),
                (Action)(() => _instance = null)
            };
        }
    }
}