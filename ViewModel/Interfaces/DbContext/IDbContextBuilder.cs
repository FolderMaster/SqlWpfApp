namespace ViewModel.Interfaces.DbContext
{
    /// <summary>
    /// Интерфейс создателя контекста базы данных с методом создания и результатом.
    /// </summary>
    public interface IDbContextBuilder
    {
        /// <summary>
        /// Возвращает результат создания контекста базы данных.
        /// </summary>
        public IDbContext? Result { get; }

        /// <summary>
        /// Создаёт контекст базы данных.
        /// </summary>
        /// <param name="connectionString">Строка подключения.</param>
        public void Create(string connectionString);
    }
}