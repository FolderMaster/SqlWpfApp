using Microsoft.Win32;

using ViewModel.Interfaces;

namespace View.Implementations.Dialogs
{
    public class OpenFileDialogService : IGettingFileService
    {
        private OpenFileDialog _dialog;

        public OpenFileDialogService(string? filter = null)
        {
            _dialog = new OpenFileDialog()
            {
                Multiselect = false,
                Filter = filter != null ? filter : ""
            };
        }

        public string? GetFilePath() => _dialog.ShowDialog() == true ? _dialog.FileName : null;
    }
}