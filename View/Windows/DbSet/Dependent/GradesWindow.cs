using System;
using System.Collections.Generic;

using Model.Dependent;
using Model.Independent;
using ViewModel.VMs.DbSet;
using View.Implementations.MessageBoxes;
using View.Services;
using ViewModel.Interfaces;

namespace View.Windows.DbSet.Dependent
{
    public class GradesWindow : TwoGridDbSetWindow
    {
        private static GradesWindow? _instance = null;

        private static Action _call = () =>
        {
            var instance = Instance;
            instance.Show();
        };

        public static GradesWindow Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new GradesWindow();
                }
                return _instance;
            }
        }

        public static IDataBaseContextCreator? DataBaseContextCreator { get; set; }

        public static Action Call => _call;

        private GradesWindow() : base()
        {
            Title = AppResourceService.GetHeader(nameof(Grade));
            Icon = AppResourceService.GetIcon(nameof(Grade));

            var messageService = new ErrorMessageBoxService();

            var mainVM = new DbSetVM<Grade>(DataBaseContextCreator, messageService);
            var dependentVM = new DbSetVM<GradeMode>(DataBaseContextCreator, messageService);

            mainVM.ItemChanged += (object? sender, EventArgs e) =>
            {
                dependentVM.SelectedItem = mainVM.SelectedItem?.GradeMode;
            };

            DataContext = new List<object>()
            {
                mainVM, dependentVM,
                (string nameProperty) => nameProperty != nameof(Grade.GradeStatements) &&
                nameProperty != nameof(Grade.GradeMode), (string nameProperty) =>
                nameProperty != nameof(GradeMode.Grades) &&
                nameProperty != nameof(GradeMode.StudyForms),
                (Action)(() => _instance = null)
            };
        }
    }
}