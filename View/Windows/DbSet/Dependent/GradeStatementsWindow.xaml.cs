using System.Collections.Generic;
using System.Windows;

using ViewModel.Interfaces;
using ViewModel.VMs.DbSet;

using Model.Dependent;

namespace View.Windows.DbSet.Dependent
{
    public partial class GradeStatementsWindow : Window
    {
        public GradeStatementsWindow(IDbContextCreator dbContextCreator,
            IMessageService messageService)
        {
            InitializeComponent();

            DataContext = new List<object>()
            {
                new ControlDbSetVM<GradeStatement>(dbContextCreator, messageService),
                new DbSetVM<Discipline>(dbContextCreator, messageService),
                new DbSetVM<Student>(dbContextCreator, messageService),
                new DbSetVM<Teacher>(dbContextCreator, messageService),
                new DbSetVM<Grade>(dbContextCreator, messageService)
            };
        }
    }
}