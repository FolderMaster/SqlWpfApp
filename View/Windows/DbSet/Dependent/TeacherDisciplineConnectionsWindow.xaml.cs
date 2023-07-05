using System.Collections.Generic;
using System.Windows;

using ViewModel.VMs.DbSet;
using ViewModel.Interfaces;

using Model.Dependent;
using Model.Independent;

namespace View.Windows.DbSet.Dependent
{
    public partial class TeacherDisciplineConnectionsWindow : Window
    {
        public TeacherDisciplineConnectionsWindow(IDbContextBuilder dbContextCreator,
            IResourceService resourceService, IMessageService messageService)
        {
            InitializeComponent();

            DataContext = new List<object>()
            {
                new ControlDbSetVM<TeacherDisciplineConnection>(dbContextCreator, resourceService,
                    messageService),
                new DbSetVM<Teacher>(dbContextCreator, resourceService, messageService),
                new DbSetVM<Discipline>(dbContextCreator, resourceService, messageService),
                new DbSetVM<Role>(dbContextCreator, resourceService, messageService)
            };
        }
    }
}