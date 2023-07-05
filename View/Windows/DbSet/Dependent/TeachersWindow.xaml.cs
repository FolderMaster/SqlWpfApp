using System.Collections.Generic;
using System.Windows;

using ViewModel.VMs.DbSet;
using ViewModel.Interfaces;

using Model.Dependent;
using Model.Independent;

namespace View.Windows.DbSet.Dependent
{
    public partial class TeachersWindow : Window
    {
        public TeachersWindow(IDbContextBuilder dbContextCreator, IResourceService resourceService,
            IMessageService messageService)
        {
            InitializeComponent();

            DataContext = new List<object>()
            {
                new ControlDbSetVM<Teacher>(dbContextCreator, resourceService, messageService),
                new DbSetVM<Department>(dbContextCreator, resourceService, messageService),
                new DbSetVM<Position>(dbContextCreator, resourceService, messageService),
                new DbSetVM<Person>(dbContextCreator, resourceService, messageService)
            };
        }
    }
}