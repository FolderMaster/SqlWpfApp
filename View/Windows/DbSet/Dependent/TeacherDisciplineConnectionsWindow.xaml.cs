using System.Collections.Generic;
using System.Windows;

using ViewModel.VMs.DbSet;
using ViewModel.Interfaces.DbContext;
using ViewModel.Interfaces.Services;
using ViewModel.Interfaces.Services.Messages;

using Model.Dependent;
using Model.Independent;

namespace View.Windows.DbSet.Dependent
{
    /// <summary>
    /// Класс окна для работы со связями между дисциплинами и преподавателями.
    /// </summary>
    public partial class TeacherDisciplineConnectionsWindow : Window
    {
        /// <summary>
        /// Создаёт экземпляр класса <see cref="TeacherDisciplineConnectionsWindow"/>.
        /// </summary>
        /// <param name="dbContextBuilder">Создатель контекста базы данных.</param>
        /// <param name="resourceService">Сервис ресурсов.</param>
        /// <param name="messageService">Сервис сообщений.</param>
        public TeacherDisciplineConnectionsWindow(IDbContextBuilder dbContextBuilder,
            IResourceService resourceService, IMessageService messageService)
        {
            InitializeComponent();

            DataContext = new List<object>()
            {
                new ControlDbSetVM<TeacherDisciplineConnection>(dbContextBuilder, resourceService,
                    messageService),
                new DbSetVM<Teacher>(dbContextBuilder, resourceService, messageService),
                new DbSetVM<Discipline>(dbContextBuilder, resourceService, messageService),
                new DbSetVM<Role>(dbContextBuilder, resourceService, messageService)
            };
        }
    }
}