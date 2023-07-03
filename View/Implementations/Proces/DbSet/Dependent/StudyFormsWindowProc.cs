using System;
using System.Collections.Generic;
using System.Windows;

using View.Windows.DbSet.Dependent;
using View.Services;

using ViewModel.Interfaces;
using ViewModel.VMs.DbSet;

using Model.Dependent;
using Model.Independent;

namespace View.Implementations.Proces.DbSet.Dependent
{
    public class StudyFormsWindowProc : WindowProc
    {
        public StudyFormsWindowProc(IDbContextCreator dbContextCreator,
            IMessageService messageService) : base(dbContextCreator, messageService) { }

        protected override Window CreateWindow(IDbContextCreator dbContextCreator,
            IMessageService messageService)
        {
            var mainVM = new DbSetVM<StudyForm>(dbContextCreator, messageService);
            var dependentVM = new DbSetVM<GradeMode>(dbContextCreator, messageService);

            mainVM.ItemChanged += (object? sender, EventArgs e) =>
            {
                dependentVM.SelectedItem = mainVM.SelectedItem?.GradeMode;
            };

            return new TwoGridDbSetWindow()
            {
                Title = AppResourceService.GetHeader(nameof(StudyForm) + "s"),
                Icon = AppResourceService.GetIcon(nameof(StudyForm) + "s"),
                DataContext = new List<object>()
                {
                    mainVM, dependentVM, (string nameProperty) =>
                    nameProperty != nameof(StudyForm.GradeMode) &&
                    nameProperty != nameof(StudyForm.Disciplines), (string nameProperty) =>
                    nameProperty != nameof(GradeMode.Grades) &&
                    nameProperty != nameof(GradeMode.StudyForms)
                }
            };
        }
    }
}