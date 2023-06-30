using System;
using System.Collections.Generic;

using Model.Dependent;
using ViewModel.VMs.DbSet;
using View.Implementations.MessageBoxes;
using View.Services;
using ViewModel.Interfaces;

namespace View.Windows.DbSet.Dependent
{
    public class DisciplinesWindow : ThreeGridDbSetWindow
    {
        private static DisciplinesWindow? _instance = null;

        private static Action _call = () =>
        {
            var instance = Instance;
            instance.Show();
        };

        public static DisciplinesWindow Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new DisciplinesWindow();
                }
                return _instance;
            }
        }

        public static IDataBaseContextCreator? DataBaseContextCreator { get; set; }

        public static Action Call => _call;

        private DisciplinesWindow() : base()
        {
            Title = AppResourceService.GetHeader(nameof(Discipline));
            Icon = AppResourceService.GetIcon(nameof(Discipline));

            var messageService = new ErrorMessageBoxService();

            var mainVM = new DbSetVM<Discipline>(DataBaseContextCreator, messageService);
            var dependentVM = new DbSetVM<StudyForm>(DataBaseContextCreator, messageService);
            var dependent2VM = new DbSetVM<Specialty>(DataBaseContextCreator, messageService);

            mainVM.ItemChanged += (object? sender, EventArgs e) =>
            {
                dependentVM.SelectedItem = mainVM.SelectedItem?.StudyForm;
                dependent2VM.SelectedItem = mainVM.SelectedItem?.Specialty;
            };

            DataContext = new List<object>()
            {
                mainVM, dependentVM, dependent2VM,
                (string nameProperty) => nameProperty != nameof(Discipline.Specialty) &&
                nameProperty != nameof(Discipline.StudentConnections) &&
                nameProperty != nameof(Discipline.TeacherConnections) &&
                nameProperty != nameof(Discipline.StudyForm) &&
                nameProperty != nameof(Discipline.GradeStatements),
                (string nameProperty) => nameProperty != nameof(StudyForm.Disciplines) &&
                nameProperty != nameof(StudyForm.GradeMode),
                (string nameProperty) => nameProperty != nameof(Specialty.Groups) &&
                nameProperty != nameof(Specialty.Disciplines) &&
                nameProperty != nameof(Specialty.Department),
                (Action)(() => _instance = null)
            };
        }
    }
}