using Microsoft.Win32;
using ViewModel.Interfaces;

namespace View.Implementations.Dialogs
{
    public class SaveFileDialogService : IGettingFileService
    {
        private SaveFileDialog _dialog;

        public SaveFileDialogService(string? filter = null)
        {
            _dialog = new SaveFileDialog()
            {
                Filter = filter != null ? filter : ""
            };
        }

        public string? GetFilePath() => _dialog.ShowDialog() == true ? _dialog.FileName : null;
    }
}