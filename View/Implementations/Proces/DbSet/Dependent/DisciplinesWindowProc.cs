using System;
using System.Collections.Generic;
using System.Windows;

using View.Services;
using View.Windows.DbSet.Dependent;

using ViewModel.Interfaces;
using ViewModel.VMs.DbSet;

using Model.Dependent;

namespace View.Implementations.Proces.DbSet.Dependent
{
    public class DisciplinesWindowProc : WindowProc
    {
        public DisciplinesWindowProc(IDbContextCreator dbContextCreator,
            IMessageService messageService) : base(dbContextCreator, messageService) { }

        protected override Window CreateWindow(IDbContextCreator dbContextCreator,
            IMessageService messageService)
        {
            var mainVM = new DbSetVM<Discipline>(dbContextCreator, messageService);
            var dependentVM = new DbSetVM<StudyForm>(dbContextCreator, messageService);
            var dependent2VM = new DbSetVM<Specialty>(dbContextCreator, messageService);

            mainVM.ItemChanged += (object? sender, EventArgs e) =>
            {
                dependentVM.SelectedItem = mainVM.SelectedItem?.StudyForm;
                dependent2VM.SelectedItem = mainVM.SelectedItem?.Specialty;
            };

            return new ThreeGridDbSetWindow()
            {
                Title = AppResourceService.GetHeader(nameof(Discipline) + "s"),
                Icon = AppResourceService.GetIcon(nameof(Discipline) + "s"),
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
                    nameProperty != nameof(Specialty.Department)
                }
            };
        }
    }
}