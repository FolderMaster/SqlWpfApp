using System.Collections.Generic;
using System.Windows;

using ViewModel.VMs.DbSet;
using ViewModel.Interfaces.DataBase;
using ViewModel.Interfaces.Services;
using ViewModel.Interfaces.Services.Messages;

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
        /// <param name="dbContextBuilder">Создатель контекста базы данных.</param>
        /// <param name="resourceService">Сервис ресурсов.</param>
        /// <param name="messageService">Сервис сообщений.</param>
        public GradeStatementsWindow(IDbContextBuilder dbContextBuilder,
            IResourceService resourceService, IMessageService messageService)
        {
            InitializeComponent();

            DataContext = new List<object>()
            {
                new ControlDbSetVM<GradeStatement>(dbContextBuilder, resourceService,
                    messageService),
                new DbSetVM<Discipline>(dbContextBuilder, resourceService, messageService),
                new DbSetVM<Student>(dbContextBuilder, resourceService, messageService),
                new DbSetVM<Teacher>(dbContextBuilder, resourceService, messageService),
                new DbSetVM<Grade>(dbContextBuilder, resourceService, messageService)
            };
        }
    }
}