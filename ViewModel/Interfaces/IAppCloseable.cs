namespace ViewModel.Interfaces
{
    /// <summary>
    /// Интерфейс закрытия приложения с методом закрытия приложения.
    /// </summary>
    public interface IAppCloseable
    {
        /// <summary>
        /// Закрывает приложение.
        /// </summary>
        public void CloseApp();
    }
}