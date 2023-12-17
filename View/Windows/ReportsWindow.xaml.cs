using System.Windows;

using ViewModel.VMs.Request;
using ViewModel.Interfaces.DataBase;
using ViewModel.Interfaces.Services;
using ViewModel.Interfaces.Services.Messages;

namespace View.Windows
{
    /// <summary>
    /// Класс окна для работы с отчётами.
    /// </summary>
    public partial class ReportsWindow : Window
    {
        /// <summary>
        /// Создаёт экземпляр класса <see cref="ReportsWindow"/>.
        /// </summary>
        /// <param name="dbContextBuilder">Создатель контекста базы данных.</param>
        /// <param name="resourceService">Сервис ресурсов.</param>
        /// <param name="messageService">Сервис сообщений.</param>
        /// <param name="printDialogService">Сервис печати.</param>
        public ReportsWindow(IDbContextBuilder dbContextBuilder, IResourceService resourceService,
            IMessageService messageService, IPrintService printDialogService)
        {
            InitializeComponent();

            DataContext = new ReportsVM(dbContextBuilder, resourceService, messageService,
                printDialogService);
        }
    }
}