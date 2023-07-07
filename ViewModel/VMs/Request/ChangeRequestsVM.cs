using ViewModel.Interfaces.DbContext;
using ViewModel.Interfaces.Services;
using ViewModel.Interfaces.Services.Messages;

namespace ViewModel.VMs.Request
{
    /// <summary>
    /// Класс представления модели для выполнения запросов изменения с результатом выполнения и
    /// методами выполнения, созданий команд для запросов изменений и просмотра.
    /// </summary>
    public class ChangeRequestsVM : ViewRequestsVM
    {
        /// <summary>
        /// Создаёт команду для запроса вставки.
        /// </summary>
        /// <param name="table">Таблица.</param>
        /// <param name="columns">Столбцы.</param>
        /// <param name="values">Значения.</param>
        /// <returns>Команда для запроса вставки.</returns>
        protected string CreateInsertCommand(string table, string columns, string values) =>
            $"INSERT INTO {table} ({columns}) VALUES ({values})";

        /// <summary>
        /// Создаёт команду для запроса обновления.
        /// </summary>
        /// <param name="table">Таблица.</param>
        /// <param name="updating">Обновление.</param>
        /// <param name="condition">Условие.</param>
        /// <returns>Команда для запроса обновления.</returns>
        protected string CreateUpdateCommand(string table, string updating,
            string condition = "") =>
            $"UPDATE {table} SET {updating}" +
            (string.IsNullOrEmpty(condition) ? "" : $" WHERE {condition}");

        /// <summary>
        /// Создаёт команду для запроса удаления.
        /// </summary>
        /// <param name="table">Таблица.</param>
        /// <param name="condition">Условие.</param>
        /// <returns>Команда для запроса удаления.</returns>
        protected string CreateDeleteCommand(string table, string condition) =>
            $"DELETE FROM {table} WHERE {condition}";

        /// <summary>
        /// Создаёт экземпляр класса <see cref="ChangeRequestsVM"/>.
        /// </summary>
        /// <param name="dbContextBuilder">Создатель контекста базы данных.</param>
        /// <param name="resourceService">Сервис ресурсов.</param>
        /// <param name="messageService">Сервис сообщений.</param>
        public ChangeRequestsVM(IDbContextBuilder dbContextBuilder,
            IResourceService resourceService, IMessageService messageService) :
            base(dbContextBuilder, resourceService, messageService) { }
    }
}