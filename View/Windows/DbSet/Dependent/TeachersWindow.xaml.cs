using System.Collections.Generic;
using System.Windows;

using ViewModel.VMs.DbSet;
using ViewModel.Interfaces;
using ViewModel.Interfaces.Services;

using Model.Dependent;
using Model.Independent;

namespace View.Windows.DbSet.Dependent
{
    /// <summary>
    /// Класс окна для работы с преподавателями.
    /// </summary>
    public partial class TeachersWindow : Window
    {
        /// <summary>
        /// Создаёт экземпляр класса <see cref="TeachersWindow"/>.
        /// </summary>
        /// <param name="session">Создатель контекста базы данных.</param>
        /// <param name="resourceService">Сервис ресурсов.</param>
        /// <param name="messageService">Сервис сообщений.</param>
        public TeachersWindow(ISession session, IResourceService resourceService,
            IMessageService messageService)
        {
            InitializeComponent();

            DataContext = new List<object>()
            {
                new ControlDbSetVM<Teacher>(session, resourceService, messageService),
                new DbSetVM<Department>(session, resourceService, messageService),
                new DbSetVM<Position>(session, resourceService, messageService),
                new DbSetVM<Person>(session, resourceService, messageService)
            };
        }
    }
}