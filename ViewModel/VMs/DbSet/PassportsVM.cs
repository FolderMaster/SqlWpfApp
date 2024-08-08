using CommunityToolkit.Mvvm.Input;

using System.Collections.Generic;

using Model.Independent;

using ViewModel.Classes;
using ViewModel.Interfaces;
using ViewModel.Interfaces.Services;
using ViewModel.Interfaces.Services.Data;
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
        private static readonly string _fileDialogFilterKey = "FileDialogFilter";

        private static readonly string _isNotImageMessageKey = "IsNotImageMessage";

        /// <summary>
        /// Возвращает и задаёт файловый сервис.
        /// </summary>
        public IFileService FileService { get; private set; }

        public IImageService ImageService { get; private set; }

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
        public PassportsVM(ISession session, IResourceService resourceService,
            IMessageService messageService, ISearchService searchService,
            IGettingFileService openFileService, IGettingFileService saveFileService,
            IFileService fileService, IImageService imageService) :
            base(session, resourceService, messageService, searchService)
        {
            FileService = fileService;
            ImageService = imageService;

            LoadImageCommand = new RelayCommand(() =>
            {
                var fileFormats = ChangeFileFormats(ImageService.ImageFormats);
                var filePath = openFileService.GetFilePath(fileFormats);
                if (filePath != null)
                {
                    MessengerService.ExecuteWithExceptionMessage(_resourceService,
                        _messageService, () =>
                        {
                            var data = FileService.Load(filePath);
                            if (ImageService.IsImage(data))
                            {
                                SelectedItem.Scan = data;
                            }
                            else
                            {
                                MessengerService.ShowErrorMessage(messageService,
                                    resourceService, _isNotImageMessageKey);
                            }
                        });
                }
            }, () => SelectedItem != null);
            SaveImageCommand = new RelayCommand(() =>
            {
                var fileFormats = ChangeFileFormats
                    ([ImageService.GetImageFormat(SelectedItem.Scan)]);
                var filePath = saveFileService.GetFilePath(fileFormats);
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

        private IEnumerable<FileFormat> ChangeFileFormats(IEnumerable<FileFormat> fileFormats)
        {
            var result = new List<FileFormat>();
            foreach (var fileFormat in fileFormats)
            {
                result.Add(new FileFormat(
                    _resourceService.GetString(fileFormat.Name + _fileDialogFilterKey),
                    fileFormat.Extensions));
            }
            return result;
        }
    }
}