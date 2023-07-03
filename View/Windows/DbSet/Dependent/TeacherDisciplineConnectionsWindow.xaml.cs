﻿using System.Collections.Generic;
using System.Windows;

using ViewModel.VMs.DbSet;
using ViewModel.Interfaces;

using Model.Dependent;
using Model.Independent;

namespace View.Windows.DbSet.Dependent
{
    public partial class TeacherDisciplineConnectionsWindow : Window
    {
        public TeacherDisciplineConnectionsWindow(IDbContextCreator dbContextCreator,
            IMessageService messageService)
        {
            InitializeComponent();

            DataContext = new List<object>()
            {
                new ControlDbSetVM<TeacherDisciplineConnection>(dbContextCreator, messageService),
                new DbSetVM<Teacher>(dbContextCreator, messageService),
                new DbSetVM<Discipline>(dbContextCreator,messageService),
                new DbSetVM<Role>(dbContextCreator, messageService)
            };
        }
    }
}