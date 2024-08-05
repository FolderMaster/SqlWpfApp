using Microsoft.Win32;

using System.Collections.Generic;

using ViewModel.Classes;
using ViewModel.Interfaces.Services.Files;

namespace View.Implementations.Dialogs
{
    /// <summary>
    /// Класс сервиса диалога сохранения файлов с методом получения пути к файлу. Реализует
    /// <see cref="IGettingFileService"/>.
    /// </summary>
    public class SaveFileDialogService : BaseFileDialogService
    {
        /// <inheritdoc/>
        public override string? GetFilePath
            (IEnumerable<FileFormat>? filter = null)
        {
            var dialog = new SaveFileDialog() { Filter = GetFilter(filter) };
            return dialog.ShowDialog() == true ? dialog.FileName : null;
        }
    }
}