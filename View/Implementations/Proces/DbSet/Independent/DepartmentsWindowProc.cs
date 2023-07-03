using System.Collections.Generic;
using System.Windows;

using View.Services;
using View.Windows.DbSet.Independent;

using ViewModel.Interfaces;
using ViewModel.VMs.DbSet;

using Model.Independent;

namespace View.Implementations.Proces.DbSet.Independent
{
    public class DepartmentsWindowProc : WindowProc
    {
        public DepartmentsWindowProc(IDbContextCreator dbContextCreator,
            IMessageService messageService) : base(dbContextCreator, messageService) { }

        protected override Window CreateWindow(IDbContextCreator dbContextCreator,
            IMessageService messageService) => new GridDbSetWindow()
            {
                Title = AppResourceService.GetHeader(nameof(Department) + "s"),
                Icon = AppResourceService.GetIcon(nameof(Department) + "s"),
                DataContext = new List<object>()
                {
                    new DbSetVM<Department>(dbContextCreator, messageService),
                    (string nameProperty) => nameProperty != nameof(Department.Specialties) &&
                    nameProperty != nameof(Department.Teachers)
                }
            };
    }
}