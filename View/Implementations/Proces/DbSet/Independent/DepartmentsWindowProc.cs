using System.Collections.Generic;
using System.Windows;

using View.Windows.DbSet.Independent;
using View.Implementations.ResourceService;

using ViewModel.Interfaces;
using ViewModel.VMs.DbSet;

using Model.Independent;

namespace View.Implementations.Proces.DbSet.Independent
{
    public class DepartmentsWindowProc : WindowProc
    {
        private static string _keyResource = nameof(Department) + "s";

        public DepartmentsWindowProc(IDbContextBuilder dbContextCreator,
            IWindowResourceService windowResourceService, IMessageService messageService) :
            base(dbContextCreator, windowResourceService, messageService) { }

        protected override Window CreateWindow(IDbContextBuilder dbContextCreator,
            IWindowResourceService windowResourceService, IMessageService messageService) =>
            new GridDbSetWindow(windowResourceService, _keyResource, _keyResource,
                new List<object>()
                {
                    new DbSetVM<Department>(dbContextCreator, windowResourceService,
                        messageService),
                    (string nameProperty) => nameProperty != nameof(Department.Specialties) &&
                    nameProperty != nameof(Department.Teachers)
                });
    }
}