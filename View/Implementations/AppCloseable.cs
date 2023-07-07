using System.Windows;

using ViewModel.Interfaces;

namespace View.Implementations
{
    /// <summary>
    /// Класс закрытия приложения с методом закрытия приложения.
    /// </summary>
    public class AppCloseable : IAppCloseable
    {
        /// <summary>
        /// Закрывает приложение.
        /// </summary>
        public void CloseApp() => Application.Current.Shutdown();
    }
}