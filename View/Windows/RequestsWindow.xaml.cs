using System.Collections.Generic;
using System.Windows;

using ViewModel.Interfaces;
using ViewModel.Interfaces.Services;
using ViewModel.VMs.Request;

namespace View.Windows
{
    /// <summary>
    /// Класс окна для работы с запросами.
    /// </summary>
    public partial class RequestsWindow : Window
    {
        /// <summary>
        /// Создаёт экземпляр класса <see cref="RequestsWindow"/>.
        /// </summary>
        /// <param name="session">Создатель контекста базы данных.</param>
        /// <param name="resourceService">Сервис ресурсов.</param>
        /// <param name="messageService">Сервис сообщений.</param>
        public RequestsWindow(ISession session, IResourceService resourceService,
            IMessageService messageService)
        {
            InitializeComponent();

            DataContext = new List<object>()
            {
                new ChangeDataRequestsVM(session, resourceService, messageService),
                new SpecialChangeDataRequestsVM(session, resourceService, messageService),
                new ViewDataRequestsVM(session, resourceService, messageService),
                new SpecialViewDataRequestsVM(session, resourceService, messageService)
            };
        }
    }
}