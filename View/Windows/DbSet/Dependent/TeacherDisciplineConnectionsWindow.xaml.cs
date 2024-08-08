using System.Collections.Generic;
using System.Windows;

using ViewModel.VMs.DbSet;
using ViewModel.Interfaces.Services;
using ViewModel.Interfaces;
using ViewModel.Interfaces.Services.Data;

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
        /// <param name="session">Создатель контекста базы данных.</param>
        /// <param name="resourceService">Сервис ресурсов.</param>
        /// <param name="messageService">Сервис сообщений.</param>
        public TeacherDisciplineConnectionsWindow(ISession session,
            IResourceService resourceService,IMessageService messageService,
            ISearchService searchService)
        {
            InitializeComponent();

            DataContext = new List<object>()
            {
                new ControlDbSetVM<TeacherDisciplineConnection>(session, resourceService,
                    messageService, searchService),
                new DbSetVM<Teacher>(session, resourceService, messageService, searchService),
                new DbSetVM<Discipline>(session, resourceService, messageService, searchService),
                new DbSetVM<Role>(session, resourceService, messageService, searchService)
            };
        }
    }
}