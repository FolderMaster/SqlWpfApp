using CommunityToolkit.Mvvm.ComponentModel;

using ViewModel.Enums;

namespace ViewModel.Classes
{
    /// <summary>
    /// Класс сортировки с названием столбца и режимом сортировки.
    /// </summary>
    public partial class SortItem : ObservableObject
    {
        /// <summary>
        /// Название столбца.
        /// </summary>
        [ObservableProperty]
        private string columnName;

        /// <summary>
        /// Режим сортировки.
        /// </summary>
        [ObservableProperty]
        private SortMode sortMode;
    }
}