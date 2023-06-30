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
    public class StudyFormsWindow : TwoGridDbSetWindow
    {
        private static StudyFormsWindow? _instance = null;

        private static Action _call = () =>
        {
            var instance = Instance;
            instance.Show();
        };

        public static StudyFormsWindow Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new StudyFormsWindow();
                }
                return _instance;
            }
        }

        public static Action Call => _call;

        public static IDataBaseContextCreator? DataBaseContextCreator { get; set; }

        private StudyFormsWindow() : base()
        {
            Title = AppResourceService.GetHeader(nameof(StudyForm));
            Icon = AppResourceService.GetIcon(nameof(StudyForm));

            var messageService = new ErrorMessageBoxService();

            var mainVM = new DbSetVM<StudyForm>(DataBaseContextCreator, messageService);
            var dependentVM = new DbSetVM<GradeMode>(DataBaseContextCreator, messageService);

            mainVM.ItemChanged += (object? sender, EventArgs e) =>
            {
                dependentVM.SelectedItem = mainVM.SelectedItem?.GradeMode;
            };

            DataContext = new List<object>()
            {
                mainVM, dependentVM, (string nameProperty) =>
                nameProperty != nameof(StudyForm.GradeMode) &&
                nameProperty != nameof(StudyForm.Disciplines), (string nameProperty) =>
                nameProperty != nameof(GradeMode.Grades) &&
                nameProperty != nameof(GradeMode.StudyForms),
                (Action)(() => _instance = null)
            };
        }
    }
}