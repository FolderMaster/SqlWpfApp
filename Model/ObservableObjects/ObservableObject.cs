using System.Collections;
using System.ComponentModel;

namespace Model.ObservableObjects
{
    /// <summary>
    /// Класс сервис для обновления свойств, который реализует интерфейс
    /// <see cref="INotifyPropertyChanged"/>, у которого есть метод для работы с ним.
    /// </summary>
    public class ObservableObject : INotifyPropertyChanged, INotifyDataErrorInfo
    {
        private static Dictionary<Type, List<ObservableProperty>> _classProperties = new();

        private Dictionary<string, IEnumerable<string>> _errors = new();

        public bool HasErrors => _errors.Any();

        /// <summary>
        /// Обработчик события изменения свойства.
        /// </summary>
        public event PropertyChangedEventHandler? PropertyChanged;

        public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;

        protected ObservableObject()
        {
            var ownerType = GetType();
            if (_classProperties.ContainsKey(ownerType))
            {
                var properties = _classProperties[ownerType];
                foreach (var property in properties)
                {
                    property.CreateValueForOwner(this);
                    UpdateErrors(property);
                }
            }
        }

        protected static ObservableProperty RegisterProperty(Type ownerType, string name,
            object? defaultValue, IEnumerable<Func<ObservableArgs, string?>>? validations = null,
            Action<ObservableArgs>? callback = null)
        {
            var property = new ObservableProperty(name, defaultValue, callback, validations);
            if (_classProperties.ContainsKey(ownerType))
            {
                var properties = _classProperties[ownerType];
                properties.Add(property);
            }
            else
            {
                _classProperties[ownerType] = new() { property };
            }
            return property;
        }

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

        protected object? GetProperty(ObservableProperty property) =>
            property.GetValueForOwner(this);

        protected void SetProperty(object? value, ObservableProperty property)
        {
            if (property.UpdateValueForOwner(this, value))
            {
                PropertyChangedEventInvoke(property.Name);
                UpdateErrors(property);
                ErrorsChangedEventInvoke(property.Name);
            }
        }

        private void UpdateErrors(ObservableProperty property)
        {
            var errors = property.Validate(this);
            if (errors != null)
            {
                _errors[property.Name] = errors;
            }
            else
            {
                _errors.Remove(property.Name);
            }
        }

        /// <summary>
        /// Вызывает событие <see cref="PropertyChanged"/>.
        /// </summary>
        /// <param name="propertyName">Название свойства.</param>
        public void PropertyChangedEventInvoke(string propertyName)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        public void ErrorsChangedEventInvoke(string propertyName)
            => ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
    }
}