using System.Collections.Generic;
using System.Windows;

using ViewModel.Interfaces;
using ViewModel.VMs.DbSet;

using Model.Dependent;

namespace View.Windows.DbSet.Dependent
{
    public partial class GradeStatementsWindow : Window
    {
        public GradeStatementsWindow(IDbContextBuilder dbContextCreator,
            IResourceService resourceService, IMessageService messageService)
        {
            InitializeComponent();

            DataContext = new List<object>()
            {
                new ControlDbSetVM<GradeStatement>(dbContextCreator, resourceService,
                    messageService),
                new DbSetVM<Discipline>(dbContextCreator, resourceService, messageService),
                new DbSetVM<Student>(dbContextCreator, resourceService, messageService),
                new DbSetVM<Teacher>(dbContextCreator, resourceService, messageService),
                new DbSetVM<Grade>(dbContextCreator, resourceService, messageService)
            };
        }
    }
}