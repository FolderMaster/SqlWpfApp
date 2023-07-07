using ViewModel.Interfaces.DbContext;
using ViewModel.Interfaces.Services;
using ViewModel.Interfaces.Services.Messages;

namespace ViewModel.VMs.Request
{
    /// <summary>
    /// Класс представления модели для выполнения запросов просмотра с результатом выполнения и
    /// методами выполнения, создания команды для запроса просмотра.
    /// </summary>
    public class ViewRequestsVM : RequestsVM
    {
        /// <summary>
        /// Создаёт команду для запроса просмотра.
        /// </summary>
        /// <param name="selectContent">Предложение SELECT.</param>
        /// <param name="fromContent">Предложение FROM.</param>
        /// <param name="whereContent">Предложение WHERE.</param>
        /// <param name="groupByContent">Предложение GROUP BY.</param>
        /// <param name="havingContent">Предложение HAVING.</param>
        /// <param name="orderByContent">Предложение ORDER BY.</param>
        /// <returns>Команда для запроса просмотра.</returns>
        protected string CreateSelectCommand(string selectContent, string fromContent,
            string whereContent = "", string groupByContent = "", string havingContent = "",
            string orderByContent = "") =>
            $"SELECT {selectContent} " +
            $"FROM {fromContent}" +
            (string.IsNullOrEmpty(whereContent) ? "" : $" WHERE {whereContent}") +
            (string.IsNullOrEmpty(groupByContent) ? "" : $" GROUP BY {groupByContent}") +
            (string.IsNullOrEmpty(havingContent) ? "" : $" HAVING {havingContent}") +
            (string.IsNullOrEmpty(orderByContent) ? "" : $" ORDER BY {orderByContent}");

        /// <summary>
        /// Создаёт экземпляр класса <see cref="ViewRequestsVM"/>.
        /// </summary>
        /// <param name="dbContextBuilder">Создатель контекста базы данных.</param>
        /// <param name="resourceService">Сервис ресурсов.</param>
        /// <param name="messageService">Сервис сообщений.</param>
        public ViewRequestsVM(IDbContextBuilder dbContextBuilder,
            IResourceService resourceService, IMessageService messageService) :
            base(dbContextBuilder, resourceService, messageService) { }
    }
}