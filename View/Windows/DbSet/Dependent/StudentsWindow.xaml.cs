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
    /// Класс окна для работы со студентами.
    /// </summary>
    public partial class StudentsWindow : Window
    {
        /// <summary>
        /// Создаёт экземпляр класса <see cref="StudentsWindow"/>.
        /// </summary>
        /// <param name="dbContextBuilder">Создатель контекста базы данных.</param>
        /// <param name="resourceService">Сервис ресурсов.</param>
        /// <param name="messageService">Сервис сообщений.</param>
        public StudentsWindow(IDbContextBuilder dbContextBuilder, IResourceService resourceService,
            IMessageService messageService)
        {
            InitializeComponent();

            DataContext = new List<object>()
            {
                new ControlDbSetVM<Student>(dbContextBuilder, resourceService, messageService),
                new DbSetVM<Group>(dbContextBuilder, resourceService, messageService),
                new DbSetVM<Scholarship>(dbContextBuilder, resourceService, messageService),
                new DbSetVM<Person>(dbContextBuilder, resourceService, messageService)
            };
        }
    }
}