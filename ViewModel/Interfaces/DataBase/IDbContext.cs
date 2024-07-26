using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;

namespace ViewModel.Interfaces.DataBase
{
    /// <summary>
    /// Интерфейс контекста базы данных с методами создания представления таблицы из базы данных,
    /// сохранения изменения в таблице и выполнения команды.
    /// </summary>
    public interface IDbContext
    {
        public bool CanConnect { get; }

        /// <summary>
        /// Создаёт представление таблицы из базы данных.
        /// </summary>
        /// <typeparam name="TEntity">Тип сущности таблицы.</typeparam>
        /// <returns>Представление таблицы из базы данных.</returns>
        public ObservableCollection<TEntity> GetDbSetLocal<TEntity>() where TEntity : class;

        public void Reload<TEntity>() where TEntity : class;

        public void RejectChanges<TEntity>() where TEntity : class;

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
        public DataTable ExecuteCommand(string commandString,
            IDictionary<string, object>? parameters = null);
    }
}