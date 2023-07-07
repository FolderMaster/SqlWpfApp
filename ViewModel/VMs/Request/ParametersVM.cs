using CommunityToolkit.Mvvm.ComponentModel;

namespace ViewModel.VMs.Request
{
    /// <summary>
    /// Класс представления модели для параметров.
    /// </summary>
    public partial class ParametersVM : ObservableObject
    {
        /// <summary>
        /// Параметры.
        /// </summary>
        [ObservableProperty]
        private object[] parameters;

        /// <summary>
        /// Возвращает параметр по индексу.
        /// </summary>
        /// <param name="index">Индекс параметра.</param>
        /// <returns>Параметр.</returns>
        public object this[int index]
        {
            get => Parameters[index];
            set
            {
                if (Parameters[index].GetType() == value.GetType())
                {
                    Parameters[index] = value;
                }
                OnPropertyChanged($"[{index}]");
            }
        }

        /// <summary>
        /// Создаёт экземпляр класса <see cref="ParametersVM"/>.
        /// </summary>
        /// <param name="parameters">Параметры.</param>
        public ParametersVM(object[] parameters)
        {
            Parameters = parameters;
        }
    }
}