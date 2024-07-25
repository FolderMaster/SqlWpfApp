using View.Implementations.Dialogs;
using View.Implementations.Proces.Windows.DbSet.Independent;
using View.Implementations.ResourceService;

using ViewModel.Interfaces;
using ViewModel.Interfaces.Services;
using ViewModel.Interfaces.Services.Files;
using ViewModel.Interfaces.Technicals;

namespace View.Services
{
    /// <summary>
    /// Класс создателя оконного процесса для работы с паспортами
    /// <seealso cref="PassportsWindowProc"/> с аргументами создания и методом создания. Реализует
    /// <see cref="ICreator{T}"/>.
    /// </summary>
    public class PassportsWindowProcCreator : ICreator<PassportsWindowProc>
    {
        /// <summary>
        /// Возвращает и задаёт создателя контекста базы данных.
        /// </summary>
        public ISession Session { get; set; }

        /// <summary>
        /// Возвращает и задаёт сервис ресурсов.
        /// </summary>
        public IWindowResourceService WindowResourceService { get; set; }

        /// <summary>
        /// Возвращает и задаёт сервис сообщений.
        /// </summary>
        public IMessageService MessageService { get; set; }

        /// <summary>
        /// Возвращает и задаёт сервис открытия файлов.
        /// </summary>
        public IGettingFileService GettingOpenFileService { get; set; }

        /// <summary>
        /// Возвращает и задаёт сервис сохранения файлов.
        /// </summary>
        public IGettingFileService GettingSaveFileService { get; set; }

        /// <summary>
        /// Возвращает и задаёт файловый сервис.
        /// </summary>
        public IFileService FileService { get; set; }

        /// <summary>
        /// Создаёт экземпляр класса <see cref="PassportsWindowProcCreator"/>.
        /// </summary>
        /// <param name="session">Создатель контекста базы данных.</param>
        /// <param name="windowResourceService">Сервис ресурсов.</param>
        /// <param name="messageService">Сервис сообщений.</param>
        /// <param name="openFileDialogService">Сервис диалога открытия файлов.</param>
        /// <param name="saveFileDialogService">Сервис диалога сохранения файлов.</param>
        /// <param name="fileService">Файловый сервис.</param>
        public PassportsWindowProcCreator(ISession session,
            IWindowResourceService windowResourceService, IMessageService messageService,
            OpenFileDialogService openFileDialogService,
            SaveFileDialogService saveFileDialogService, IFileService fileService)
        {
            Session = session;
            WindowResourceService = windowResourceService;
            MessageService = messageService;
            GettingOpenFileService = openFileDialogService;
            GettingSaveFileService = saveFileDialogService;
            FileService = fileService;
        }

        /// <summary>
        /// Создаёт процесс для работы с пасспортами <seealso cref="PassportsWindowProc"/>.
        /// </summary>
        /// <returns>Процесс для работы с пасспортами
        /// <seealso cref="PassportsWindowProc"/>.</returns>
        public PassportsWindowProc Create() => new PassportsWindowProc(Session,
            WindowResourceService, MessageService, GettingOpenFileService, GettingSaveFileService,
            FileService);
    }
}
