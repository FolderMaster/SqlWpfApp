using System.Windows;

using ViewModel.VMs.DbSet;
using ViewModel.Interfaces.Services;
using ViewModel.Interfaces.Services.Files;
using ViewModel.Interfaces.Services.Messages;
using ViewModel.Interfaces;

namespace View.Windows.DbSet.Independent
{
    /// <summary>
    /// Класс окна для работы с паспортами.
    /// </summary>
    public partial class PassportsWindow : Window
    {
        /// <summary>
        /// Создаёт экземпляр класса <see cref="PassportsWindow"/>.
        /// </summary>
        /// <param name="dbContextBuilder">Создатель контекста базы данных.</param>
        /// <param name="resourceService">Сервис ресурсов.</param>
        /// <param name="messageService">Сервис сообщений.</param>
        /// <param name="gettingOpenFileService">Сервис получения файла открытия.</param>
        /// <param name="gettingSaveFileService">Сервис получения файла сохранения.</param>
        /// <param name="fileService">Файловый сервис.</param>
        /// <param name="pathService">Сервис путей.</param>
        public PassportsWindow(ISession dbContextBuilder,
            IResourceService resourceService, IMessageService messageService,
            IGettingFileService gettingOpenFileService, IGettingFileService gettingSaveFileService,
            IFileService fileService, IPathService pathService)
        {
            InitializeComponent();

            DataContext = new PassportsVM(dbContextBuilder, resourceService, messageService,
                gettingOpenFileService, gettingSaveFileService, fileService, pathService);
        }
    }
}