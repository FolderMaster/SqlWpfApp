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
    public class SpecialtiesWindowProc : WindowProc
    {
        public SpecialtiesWindowProc(IDbContextCreator dbContextCreator,
            IMessageService messageService) : base(dbContextCreator, messageService) { }

        protected override Window CreateWindow(IDbContextCreator dbContextCreator,
            IMessageService messageService)
        {
            var mainVM = new DbSetVM<Specialty>(dbContextCreator, messageService);
            var dependentVM = new DbSetVM<Department>(dbContextCreator, messageService);

            mainVM.ItemChanged += (object? sender, EventArgs e) =>
            {
                dependentVM.SelectedItem = mainVM.SelectedItem?.Department;
            };

            return new TwoGridDbSetWindow()
            {
                Title = AppResourceService.GetHeader("Specialties"),
                Icon = AppResourceService.GetIcon("Specialties"),
                DataContext = new List<object>()
                {
                    mainVM, dependentVM, (string nameProperty) =>
                    nameProperty != nameof(Specialty.Groups) &&
                    nameProperty != nameof(Specialty.Disciplines) &&
                    nameProperty != nameof(Specialty.Department), (string nameProperty) =>
                    nameProperty != nameof(Department.Specialties) &&
                    nameProperty != nameof(Department.Teachers)
                }
            };
        }
    }
}