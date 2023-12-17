using System.Collections;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Model
{
    /// <summary>
    /// Класс сервис для обновления свойств, который реализует интерфейс
    /// <see cref="INotifyPropertyChanged"/>, у которого есть метод для работы с ним.
    /// </summary>
    public class PropertyUpdaterService : INotifyPropertyChanged, INotifyDataErrorInfo
    {
        private Dictionary<string, List<string>> _errors = new();

        public bool HasErrors => _errors.Any();

        /// <summary>
        /// Обработчик события изменения свойства.
        /// </summary>
        public event PropertyChangedEventHandler? PropertyChanged;

        public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;

        public IEnumerable GetErrors(string? propertyName)
        {
            if (propertyName == null)
            {
                return null;
            }
            if (_errors.ContainsKey(propertyName))
            {
                return _errors[propertyName];
            }
            return null;
        }

        /// <summary>
        /// Вызывает событие <see cref="PropertyChanged"/>.
        /// </summary>
        /// <param name="propertyName">Название свойства.</param>
        protected void PropertyChangedEventInvoke([CallerMemberName] string? propertyName = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        protected void AddError(string? error, [CallerMemberName] string? propertyName = null)
        {
            if (error == null)
            {
                return;
            }
            if (_errors.ContainsKey(propertyName))
            {
                _errors[propertyName].Add(error);
            }
            else
            {
                _errors[propertyName] = new() { error };
            }
        }

        protected void ClearErrors([CallerMemberName] string? propertyName = null)
        {
            if (_errors.ContainsKey(propertyName))
            {
                _errors.Remove(propertyName);
            }
        }

        protected void ErrorsChangedEventInvoke([CallerMemberName] string? propertyName = null)
            => ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
    }
}