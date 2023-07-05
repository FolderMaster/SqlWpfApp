using Microsoft.EntityFrameworkCore;

using System.Data;

namespace ViewModel.Interfaces
{
    /// <summary>
    /// Интерфейс контекста базы данных с методами создания представления таблицы из базы данных,
    /// сохранения изменения в таблице и выполения команды.
    /// </summary>
    public interface IDbContext
    {
        /// <summary>
        /// Создаёт представление таблицы из базы данных.
        /// </summary>
        /// <typeparam name="TEntity">Тип сущности таблицы.</typeparam>
        /// <returns>Представление таблицы из базы данных.</returns>
        public DbSet<TEntity> Set<TEntity>() where TEntity : class;

        /// <summary>
        /// Сохраняет изменения в таблице.
        /// </summary>
        /// <typeparam name="TEntity">Тип сущности таблицы.</typeparam>
        /// <returns>Количество сохранённых изменённых строк.</returns>
        public int SaveChanges<TEntity>() where TEntity : class;

        /// <summary>
        /// Выполняет команду.
        /// </summary>
        /// <param name="commandString">Строка команды.</param>
        /// <returns>Результат выполнения команды.</returns>
        public DataTable ExecuteCommand(string commandString);
    }
}