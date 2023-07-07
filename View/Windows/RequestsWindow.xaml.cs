using System.Collections.Generic;
using System.Windows;

using ViewModel.Interfaces.DbContext;
using ViewModel.Interfaces.Services;
using ViewModel.Interfaces.Services.Messages;
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
        /// <param name="dbContextBuilder">Создатель контекста базы данных.</param>
        /// <param name="resourceService">Сервис ресурсов.</param>
        /// <param name="messageService">Сервис сообщений.</param>
        public RequestsWindow(IDbContextBuilder dbContextBuilder, IResourceService resourceService,
            IMessageService messageService)
        {
            InitializeComponent();

            DataContext = new List<object>()
            {
                new ChangeDataRequestsVM(dbContextBuilder, resourceService, messageService),
                new SpecialChangeDataRequestsVM(dbContextBuilder, resourceService, messageService),
                new ViewDataRequestsVM(dbContextBuilder, resourceService, messageService),
                new SpecialViewDataRequestsVM(dbContextBuilder, resourceService, messageService)
            };
        }
    }
}