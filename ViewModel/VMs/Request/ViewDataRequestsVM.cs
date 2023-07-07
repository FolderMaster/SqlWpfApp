using ViewModel.Enums;
using ViewModel.Interfaces.DbContext;
using ViewModel.Interfaces.Services;
using ViewModel.Interfaces.Services.Messages;

namespace ViewModel.VMs.Request
{
    /// <summary>
    /// Класс представления модели для выполнения запросов просмотра данных с названием таблицы.
    /// </summary>
    public class ViewDataRequestsVM : ViewRequestsVM
    {
        /// <summary>
        /// Название таблицы.
        /// </summary>
        private TableName _tableName;

        /// <summary>
        /// Возвращает и задаёт название таблицы.
        /// </summary>
        public TableName TableName
        {
            get => _tableName;
            set
            {
                if (SetProperty(ref _tableName, value))
                {
                    ExecuteCommand(CreateSelectCommand("*", $"{TableName}"));
                }
            }
        }

        /// <summary>
        /// Создаёт экземпляр класса <see cref="ViewDataRequestsVM"/>.
        /// </summary>
        /// <param name="dbContextBuilder">Создатель контекста базы данных.</param>
        /// <param name="resourceService">Сервис ресурсов.</param>
        /// <param name="messageService">Сервис сообщений.</param>
        public ViewDataRequestsVM(IDbContextBuilder dbContextBuilder,
            IResourceService resourceService, IMessageService messageService) :
            base(dbContextBuilder, resourceService, messageService) { }
    }
}