using System;
using System.Collections.Generic;
using System.Windows;

using View.Windows.DbSet.Dependent;
using View.Implementations.ResourceService;

using ViewModel.Interfaces;
using ViewModel.VMs.DbSet;

using Model.Dependent;

namespace View.Implementations.Proces.DbSet.Dependent
{
    public class DisciplinesWindowProc : WindowProc
    {
        private static string _keyResource = nameof(Discipline) + "s";

        public DisciplinesWindowProc(IDbContextBuilder dbContextCreator,
            IWindowResourceService windowResourceService, IMessageService messageService) :
            base(dbContextCreator, windowResourceService, messageService) { }

        protected override Window CreateWindow(IDbContextBuilder dbContextCreator,
            IWindowResourceService windowResourceService, IMessageService messageService)
        {
            var mainVM = new DbSetVM<Discipline>(dbContextCreator, windowResourceService,
                messageService);
            var dependentVM = new DbSetVM<StudyForm>(dbContextCreator, windowResourceService,
                messageService);
            var dependent2VM = new DbSetVM<Specialty>(dbContextCreator, windowResourceService,
                messageService);

            mainVM.SelectedItemChanged += (object? sender, EventArgs e) =>
            {
                dependentVM.SelectedItem = mainVM.SelectedItem?.StudyForm;
                dependent2VM.SelectedItem = mainVM.SelectedItem?.Specialty;
            };

            return new ThreeGridDbSetWindow(windowResourceService, _keyResource, _keyResource,
                new List<object>()
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
                });
        }
    }
}