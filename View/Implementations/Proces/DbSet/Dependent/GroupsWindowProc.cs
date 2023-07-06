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
    public class GroupsWindowProc : WindowProc
    {
        private static string _keyResource = nameof(Group) + "s";

        public GroupsWindowProc(IDbContextBuilder dbContextCreator,
            IWindowResourceService windowResourceService, IMessageService messageService) :
            base(dbContextCreator, windowResourceService, messageService) { }

        protected override Window CreateWindow(IDbContextBuilder dbContextCreator,
            IWindowResourceService windowResourceService, IMessageService messageService)
        {
            var mainVM = new DbSetVM<Group>(dbContextCreator, windowResourceService,
                messageService);
            var dependentVM = new DbSetVM<Specialty>(dbContextCreator,
                windowResourceService, messageService);

            mainVM.SelectedItemChanged += (object? sender, EventArgs e) =>
            {
                dependentVM.SelectedItem = mainVM.SelectedItem?.Specialty;
            };

            return new TwoGridDbSetWindow(windowResourceService, _keyResource, _keyResource,
                new List<object>()
                {
                    mainVM, dependentVM,
                    (string nameProperty) => nameProperty != nameof(Group.Students) &&
                    nameProperty != nameof(Group.Specialty), (string nameProperty) =>
                    nameProperty != nameof(Specialty.Groups) &&
                    nameProperty != nameof(Specialty.Department) &&
                    nameProperty != nameof(Specialty.Disciplines)
                });
        }
    }
}