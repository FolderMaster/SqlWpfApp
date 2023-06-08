using CommunityToolkit.Mvvm.ComponentModel;

using SQLiteWpfApp.ViewModels.Enums;

namespace SQLiteWpfApp.ViewModels.Classes
{
    public partial class SortItem : ObservableObject
    {
        [ObservableProperty]
        private string columnName;

        [ObservableProperty]
        private SortMode sortMode;
    }
}