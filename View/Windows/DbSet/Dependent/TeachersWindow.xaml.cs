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
    /// Класс окна для работы с преподавателями.
    /// </summary>
    public partial class TeachersWindow : Window
    {
        /// <summary>
        /// Создаёт экземпляр класса <see cref="TeachersWindow"/>.
        /// </summary>
        /// <param name="dbContextBuilder">Создатель контекста базы данных.</param>
        /// <param name="resourceService">Сервис ресурсов.</param>
        /// <param name="messageService">Сервис сообщений.</param>
        public TeachersWindow(IDbContextBuilder dbContextBuilder, IResourceService resourceService,
            IMessageService messageService)
        {
            InitializeComponent();

            DataContext = new List<object>()
            {
                new ControlDbSetVM<Teacher>(dbContextBuilder, resourceService, messageService),
                new DbSetVM<Department>(dbContextBuilder, resourceService, messageService),
                new DbSetVM<Position>(dbContextBuilder, resourceService, messageService),
                new DbSetVM<Person>(dbContextBuilder, resourceService, messageService)
            };
        }
    }
}