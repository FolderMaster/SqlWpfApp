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
        public StudentsWindow(IDbContextCreator dbContextCreator, IMessageService messageService)
        {
            InitializeComponent();

            DataContext = new List<object>()
            {
                new ControlDbSetVM<Student>(dbContextCreator, messageService),
                new DbSetVM<Group>(dbContextCreator, messageService),
                new DbSetVM<Scholarship>(dbContextCreator, messageService),
                new DbSetVM<Person>(dbContextCreator, messageService)
            };
        }
    }
}