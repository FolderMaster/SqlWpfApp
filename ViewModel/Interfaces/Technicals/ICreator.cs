namespace ViewModel.Interfaces.Technicals
{
    /// <summary>
    /// Интерфейс создателя с методом создания.
    /// </summary>
    /// <typeparam name="T">Тип экземпляра.</typeparam>
    public interface ICreator<T>
    {
        /// <summary>
        /// Создаёт экземпляр.
        /// </summary>
        /// <returns>Экземпляр.</returns>
        public T Create();
    }
}