using CommunityToolkit.Mvvm.Input;

using Model.Independent;

using ViewModel.Interfaces;
using ViewModel.Interfaces.Services;
using ViewModel.Interfaces.Services.Files;
using ViewModel.Services;

namespace ViewModel.VMs.DbSet
{
    /// <summary>
    /// Класс представления модели представления таблицы паспортов <seealso cref="Passport"/> из
    /// базы данных с финальным и локальными представлениями таблицы из базы данных, количеством
    /// элементов, выбранным элементом, выбранным индексом, выбранным номером, текстом поиска,
    /// текстом фильтра, названием выбранного свойства, файловым сервисом, сервисом путей,
    /// сервисом послания сообщений, сервисом ресурсов и командами сохранения, переходами к
    /// первому, последнему, предыдущему, следующему элементу, удаления, добавления элемента,
    /// загрузки и сохранения изображения.
    /// </summary>
    public class PassportsVM : ControlDbSetVM<Passport>
    {
        /// <summary>
        /// Ключ ресурса фильтра открытия файла диалога.
        /// </summary>
        private static readonly string _openFileDialogFilterResourceKey = "ImageOpenFileDialogFilter";

        /// <summary>
        /// Ключ ресурса фильтра сохранения файла диалога.
        /// </summary>
        private static readonly string _saveFileDialogFilterResourceKey = "ImageSaveFileDialogFilter";

        /// <summary>
        /// Возвращает и задаёт файловый сервис.
        /// </summary>
        public IFileService FileService { get; private set; }

        /// <summary>
        /// Возвращает сервис послания сообщений.
        /// </summary>
        public IMessageService MessageService => _messageService;

        /// <summary>
        /// Возвращает и задаёт команду загрузки изображения.
        /// </summary>
        public RelayCommand LoadImageCommand { get; private set; }

        /// <summary>
        /// Возвращает и задаёт команду сохранения изображения.
        /// </summary>
        public RelayCommand SaveImageCommand { get; private set; }

        /// <summary>
        /// Создаёт экземпляр класса <see cref="PassportsVM"/>.
        /// </summary>
        /// <param name="session">Создатель контекста базы данных.</param>
        /// <param name="resourceService">Сервис ресурсов.</param>
        /// <param name="messageService">Сервис сообщений.</param>
        /// <param name="openFileService">Сервис открытия файлов.</param>
        /// <param name="saveFileService">Сервис сохранения файлов.</param>
        /// <param name="fileService">Файловый сервис.</param>
        public PassportsVM(ISession session,
            IResourceService resourceService, IMessageService messageService,
            IGettingFileService openFileService, IGettingFileService saveFileService,
            IFileService fileService) :
            base(session, resourceService, messageService)
        {
            FileService = fileService;

            LoadImageCommand = new RelayCommand(() =>
            {
                var filePath = openFileService.GetFilePath
                    (_resourceService.GetString(_openFileDialogFilterResourceKey));
                if (filePath != null)
                {
                    MessengerService.ExecuteWithExceptionMessage(_resourceService,
                        _messageService, () => SelectedItem.Scan = FileService.Load(filePath));
                }
            }, () => SelectedItem != null);
            SaveImageCommand = new RelayCommand(() =>
            {
                var filePath = saveFileService.GetFilePath
                    (_resourceService.GetString(_saveFileDialogFilterResourceKey));
                if (filePath != null)
                {
                    MessengerService.ExecuteWithExceptionMessage(_resourceService,
                        _messageService, () => FileService.Save(filePath, SelectedItem.Scan));
                }
            }, () => SelectedItem != null);
        }

        /// <summary>
        /// Метод, выполняющийся после изменения выбранного элемента.
        /// </summary>
        protected override void OnSelectedItemChanged()
        {
            SaveImageCommand?.NotifyCanExecuteChanged();
            LoadImageCommand?.NotifyCanExecuteChanged();
        }
    }
}