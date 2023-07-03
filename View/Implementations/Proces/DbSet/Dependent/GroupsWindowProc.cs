using System;
using System.Collections.Generic;
using System.Windows;

using View.Windows.DbSet.Dependent;

using ViewModel.Interfaces;
using ViewModel.VMs.DbSet;

using Model.Dependent;
using View.Services;

namespace View.Implementations.Proces.DbSet.Dependent
{
    public class GroupsWindowProc : WindowProc
    {
        public GroupsWindowProc(IDbContextCreator dbContextCreator,
            IMessageService messageService) : base(dbContextCreator, messageService) { }

        protected override Window CreateWindow(IDbContextCreator dbContextCreator,
            IMessageService messageService)
        {
            var mainVM = new DbSetVM<Group>(dbContextCreator, messageService);
            var dependentVM = new DbSetVM<Specialty>(dbContextCreator, messageService);

            mainVM.ItemChanged += (object? sender, EventArgs e) =>
            {
                dependentVM.SelectedItem = mainVM.SelectedItem?.Specialty;
            };

            return new TwoGridDbSetWindow()
            {
                Title = AppResourceService.GetHeader(nameof(Group) + "s"),
                Icon = AppResourceService.GetIcon(nameof(Group) + "s"),
                DataContext = new List<object>()
                {
                    mainVM, dependentVM,
                    (string nameProperty) => nameProperty != nameof(Group.Students) &&
                    nameProperty != nameof(Group.Specialty), (string nameProperty) =>
                    nameProperty != nameof(Specialty.Groups) &&
                    nameProperty != nameof(Specialty.Department) &&
                    nameProperty != nameof(Specialty.Disciplines)
                }
            };
        }
    }
}