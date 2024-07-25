using System.Collections.Generic;
using System.Windows;

using ViewModel.VMs.DbSet;
using ViewModel.Interfaces;
using ViewModel.Interfaces.Services;

using Model.Dependent;

namespace View.Windows.DbSet.Dependent
{
    /// <summary>
    /// Класс окна для работы с ведомостями оценивания.
    /// </summary>
    public partial class GradeStatementsWindow : Window
    {
        /// <summary>
        /// Создаёт экземпляр класса <see cref="GradeStatementsWindow"/>.
        /// </summary>
        /// <param name="session">Создатель контекста базы данных.</param>
        /// <param name="resourceService">Сервис ресурсов.</param>
        /// <param name="messageService">Сервис сообщений.</param>
        public GradeStatementsWindow(ISession session,
            IResourceService resourceService, IMessageService messageService)
        {
            InitializeComponent();

            DataContext = new List<object>()
            {
                new ControlDbSetVM<GradeStatement>(session, resourceService,
                    messageService),
                new DbSetVM<Discipline>(session, resourceService, messageService),
                new DbSetVM<Student>(session, resourceService, messageService),
                new DbSetVM<Teacher>(session, resourceService, messageService),
                new DbSetVM<Grade>(session, resourceService, messageService)
            };
        }
    }
}