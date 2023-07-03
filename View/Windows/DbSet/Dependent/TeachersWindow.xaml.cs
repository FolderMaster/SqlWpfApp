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
        public TeachersWindow(IDbContextCreator dbContextCreator, IMessageService messageService)
        {
            InitializeComponent();

            DataContext = new List<object>()
            {
                new ControlDbSetVM<Teacher>(dbContextCreator, messageService),
                new DbSetVM<Department>(dbContextCreator, messageService),
                new DbSetVM<Position>(dbContextCreator, messageService),
                new DbSetVM<Person>(dbContextCreator, messageService)
            };
        }
    }
}