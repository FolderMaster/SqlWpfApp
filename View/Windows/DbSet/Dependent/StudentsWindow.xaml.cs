using System.Collections.Generic;
using System.Windows;

using ViewModel.VMs.DbSet;
using ViewModel.Interfaces;

using Model.Dependent;
using Model.Independent;

namespace View.Windows.DbSet.Dependent
{
    public partial class StudentsWindow : Window
    {
        public StudentsWindow(IDbContextBuilder dbContextCreator, IResourceService resourceService,
            IMessageService messageService)
        {
            InitializeComponent();

            DataContext = new List<object>()
            {
                new ControlDbSetVM<Student>(dbContextCreator, resourceService, messageService),
                new DbSetVM<Group>(dbContextCreator, resourceService, messageService),
                new DbSetVM<Scholarship>(dbContextCreator, resourceService, messageService),
                new DbSetVM<Person>(dbContextCreator, resourceService, messageService)
            };
        }
    }
}