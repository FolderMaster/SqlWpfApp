using CommunityToolkit.Mvvm.Input;

using ViewModel.Interfaces;

using Model.Independent;

namespace ViewModel.VMs.DbSet
{
    public class PassportsVM : ControlDbSetVM<Passport>
    {
        private static string _openFileDialogFilterResourceKey = "ImageOpenFileDialogFilter";

        private static string _saveFileDialogFilterResourceKey = "ImageSaveFileDialogFilter";

        private IFileService _fileService;

        public RelayCommand LoadImageCommand { get; private set; }

        public RelayCommand SaveImageCommand { get; private set; }

        public PassportsVM(IDbContextBuilder dataBaseContextCreator,
            IResourceService resourceService, IMessageService messageService,
            IGettingFileService openFileService, IGettingFileService saveFileService,
            IFileService fileService) :
            base(dataBaseContextCreator, resourceService, messageService)
        {
            _fileService = fileService;

            LoadImageCommand = new RelayCommand(() =>
            {
                var filePath = openFileService.GetFilePath
                    (_resourceService.GetString(_openFileDialogFilterResourceKey));
                if (filePath != null)
                {
                    _messengerService.ExecuteWithExceptionMessage(() =>
                        SelectedItem.Scan = _fileService.Load(filePath));
                }
            }, () => SelectedItem != null);
            SaveImageCommand = new RelayCommand(() =>
            {
                var filePath = saveFileService.GetFilePath
                    (_resourceService.GetString(_saveFileDialogFilterResourceKey));
                if (filePath != null)
                {
                    _messengerService.ExecuteWithExceptionMessage(() =>
                        _fileService.Save(filePath, SelectedItem.Scan));
                }
            }, () => SelectedItem != null);
        }

        protected override void SelectedItemChanged()
        {
            SaveImageCommand?.NotifyCanExecuteChanged();
            LoadImageCommand?.NotifyCanExecuteChanged();
        }
    }
}