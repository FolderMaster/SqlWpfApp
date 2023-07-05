using CommunityToolkit.Mvvm.ComponentModel;

using System;

namespace ViewModel.Classes
{
    /// <summary>
    /// Класс параметра с названием, значением и типом.
    /// </summary>
    public partial class Parameter : ObservableObject
    {
        /// <summary>
        /// Название.
        /// </summary>
        [ObservableProperty]
        private string name;

        /// <summary>
        /// Значение.
        /// </summary>
        [ObservableProperty]
        private object value;

        /// <summary>
        /// Тип.
        /// </summary>
        [ObservableProperty]
        private Type type;
    }
}