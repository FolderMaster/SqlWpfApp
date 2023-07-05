using Microsoft.Win32;

using ViewModel.Interfaces;

namespace View.Implementations.Dialogs
{
    public class SaveFileDialogService : IGettingFileService
    {
        public string? GetFilePath(string? filter = null)
        {
            var dialog = new SaveFileDialog() { Filter = filter != null ? filter : "" };
            return dialog.ShowDialog() == true ? dialog.FileName : null;
        }
    }
}