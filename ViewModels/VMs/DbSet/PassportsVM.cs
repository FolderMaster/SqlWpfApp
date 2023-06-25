using CommunityToolkit.Mvvm.Input;

using Model.Independent;
using ViewModel.Services;

namespace ViewModel.VMs.DbSet
{
    public class PassportsVM : ControlDbSetVM<Passport>
    {
        public RelayCommand LoadImageCommand { get; private set; }

        public RelayCommand SaveImageCommand { get; private set; }

        public PassportsVM(IMessageService messageService, IGettingFileService openFileService,
            IGettingFileService saveFileService) : base(messageService)
        {
            LoadImageCommand = new RelayCommand(() =>
            {
                var filePath = openFileService.GetFilePath();
                if (filePath != null)
                {
                    try
                    {
                        SelectedItem.Scan = File.ReadAllBytes(filePath);
                    }
                    catch (Exception ex)
                    {
                        MessageService.ShowMessage(ex.Message, "Error!");
                    }
                }
            }, () => SelectedItem != null);
            SaveImageCommand = new RelayCommand(() =>
            {
                var filePath = saveFileService.GetFilePath();
                if (filePath != null)
                {
                    try
                    {
                        File.WriteAllBytes(filePath, SelectedItem.Scan);
                    }
                    catch (Exception ex)
                    {
                        MessageService.ShowMessage(ex.Message, "Error!");
                    }
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