using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Model
{
    /// <summary>
    /// Класс сервис для обновления свойств, который реализует интерфейс
    /// <see cref="INotifyPropertyChanged"/>, у которого есть метод для работы с ним.
    /// </summary>
    public class PropertyUpdaterService : INotifyPropertyChanged
    {
        /// <summary>
        /// Обработчик события изменения свойства.
        /// </summary>
        public event PropertyChangedEventHandler? PropertyChanged;

        /// <summary>
        /// Вызывает событие <see cref="PropertyChanged"/>.
        /// </summary>
        /// <param name="propertyName">Название свойства.</param>
        public void PropertyChangedEventInvoke([CallerMemberName] string? propertyName = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}