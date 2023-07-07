using ViewModel.Interfaces.DbContext;

namespace ViewModel.Dependencies
{
    /// <summary>
    /// Класс создателя контекста базы данных SQLite <seealso cref="SQLiteDbContext"/> с методом
    /// создания и результатом. Реализует <see cref="IDbContextBuilder"/>.
    /// </summary>
    public class SQLiteDbContextBuilder : IDbContextBuilder
    {
        /// <summary>
        /// Возвращает результат создания контекста базы данных.
        /// </summary>
        public IDbContext? Result { get; private set; }

        /// <summary>
        /// Создаёт контекст базы данных.
        /// </summary>
        /// <param name="connectionString">Строка подключения.</param>
        public void Create(string connectionString) =>
            Result = new SQLiteDbContext(connectionString);
    }
}