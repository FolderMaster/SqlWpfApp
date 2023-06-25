using CommunityToolkit.Mvvm.ComponentModel;

using ViewModel.Enums;

namespace ViewModel.Classes
{
    public partial class SortItem : ObservableObject
    {
        [ObservableProperty]
        private string columnName;

        [ObservableProperty]
        private SortMode sortMode;
    }
}