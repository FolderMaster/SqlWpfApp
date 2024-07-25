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
    /// Класс окна для работы со студентами.
    /// </summary>
    public partial class StudentsWindow : Window
    {
        /// <summary>
        /// Создаёт экземпляр класса <see cref="StudentsWindow"/>.
        /// </summary>
        /// <param name="session">Создатель контекста базы данных.</param>
        /// <param name="resourceService">Сервис ресурсов.</param>
        /// <param name="messageService">Сервис сообщений.</param>
        public StudentsWindow(ISession session, IResourceService resourceService,
            IMessageService messageService)
        {
            InitializeComponent();

            DataContext = new List<object>()
            {
                new ControlDbSetVM<Student>(session, resourceService, messageService),
                new DbSetVM<Group>(session, resourceService, messageService),
                new DbSetVM<Scholarship>(session, resourceService, messageService),
                new DbSetVM<Person>(session, resourceService, messageService)
            };
        }
    }
}