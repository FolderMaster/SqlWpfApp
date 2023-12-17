using System;
using ViewModel.Interfaces.DataBase;

namespace ViewModel.Dependencies.DataBase.MsSqlServer
{
    /// <summary>
    /// Класс создателя контекста базы данных Microsoft SQL Server
    /// <seealso cref="MsSqlServerDbContext"/> с методом создания и результатом. Реализует
    /// <see cref="IDbContextBuilder"/>.
    /// </summary>
    public class MsSqlServerDbContextBuilder : IDbContextBuilder
    {
        /// <summary>
        /// Возвращает результат создания контекста базы данных.
        /// </summary>
        public IDbContext? Result { get; private set; }

        public event EventHandler? ResultConnectionCreated;

        /// <summary>
        /// Создаёт контекст базы данных.
        /// </summary>
        /// <param name="connectionString">Строка подключения.</param>
        public void Create(string connectionString)
        {
            Result = new MsSqlServerDbContext(connectionString);
            ResultConnectionCreated?.Invoke(this, EventArgs.Empty);
        }
    }
}