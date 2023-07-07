using Microsoft.Win32;

using ViewModel.Interfaces.Services.Files;

namespace View.Implementations.Dialogs
{
    /// <summary>
    /// Класс сервиса диалога сохранения файлов с методом получения пути к файлу. Реализует
    /// <see cref="IGettingFileService"/>.
    /// </summary>
    public class SaveFileDialogService : IGettingFileService
    {
        /// <summary>
        /// Получает путь к файлу.
        /// </summary>
        /// <param name="filter">Фильтр для файлов.</param>
        /// <returns>Путь к файлу.</returns>
        public string? GetFilePath(string? filter = null)
        {
            var dialog = new SaveFileDialog() { Filter = filter != null ? filter : "" };
            return dialog.ShowDialog() == true ? dialog.FileName : null;
        }
    }
}