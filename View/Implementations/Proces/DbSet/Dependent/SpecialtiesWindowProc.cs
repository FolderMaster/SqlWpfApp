using System;
using System.Collections.Generic;
using System.Windows;

using View.Windows.DbSet.Dependent;
using View.Implementations.ResourceService;

using ViewModel.Interfaces;
using ViewModel.VMs.DbSet;

using Model.Dependent;
using Model.Independent;

namespace View.Implementations.Proces.DbSet.Dependent
{
    public class SpecialtiesWindowProc : WindowProc
    {
        private static string _keyResource = "Specialties";

        public SpecialtiesWindowProc(IDbContextBuilder dbContextCreator,
            IWindowResourceService windowResourceService, IMessageService messageService) :
            base(dbContextCreator, windowResourceService, messageService) { }

        protected override Window CreateWindow(IDbContextBuilder dbContextCreator,
            IWindowResourceService windowResourceService, IMessageService messageService)
        {
            var mainVM = new DbSetVM<Specialty>(dbContextCreator, windowResourceService,
                messageService);
            var dependentVM = new DbSetVM<Department>(dbContextCreator, windowResourceService,
                messageService);

            mainVM.ItemChanged += (object? sender, EventArgs e) =>
            {
                dependentVM.SelectedItem = mainVM.SelectedItem?.Department;
            };

            return new TwoGridDbSetWindow(windowResourceService, _keyResource, _keyResource,
                new List<object>()
                {
                    mainVM, dependentVM, (string nameProperty) =>
                    nameProperty != nameof(Specialty.Groups) &&
                    nameProperty != nameof(Specialty.Disciplines) &&
                    nameProperty != nameof(Specialty.Department), (string nameProperty) =>
                    nameProperty != nameof(Department.Specialties) &&
                    nameProperty != nameof(Department.Teachers)
                });
        }
    }
}