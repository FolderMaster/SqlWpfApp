using System.Collections.Generic;
using System.Windows;

using View.Services;
using View.Windows.DbSet.Independent;

using ViewModel.Interfaces;
using ViewModel.VMs.DbSet;

using Model.Independent;

namespace View.Implementations.Proces.DbSet.Independent
{
    public class RolesWindowProc : WindowProc
    {
        public RolesWindowProc(IDbContextCreator dbContextCreator,
            IMessageService messageService) : base(dbContextCreator, messageService) { }

        protected override Window CreateWindow(IDbContextCreator dbContextCreator,
            IMessageService messageService) => new GridDbSetWindow()
            {
                Title = AppResourceService.GetHeader(nameof(Role) + "s"),
                Icon = AppResourceService.GetIcon(nameof(Role) + "s"),
                DataContext = new List<object>()
                {
                    new DbSetVM<Role>(dbContextCreator, messageService),
                    (string nameProperty) => nameProperty != nameof(Role.Connections)
                }
            };
    }
}