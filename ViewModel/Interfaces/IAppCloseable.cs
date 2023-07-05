namespace ViewModel.Interfaces
{
    /// <summary>
    /// Интерфейс закрытия приложения с методом.
    /// </summary>
    public interface IAppCloseable
    {
        /// <summary>
        /// Закрывает приложение.
        /// </summary>
        public void CloseApp();
    }
}