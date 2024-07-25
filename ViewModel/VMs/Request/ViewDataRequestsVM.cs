using ViewModel.Enums;
using ViewModel.Interfaces;
using ViewModel.Interfaces.Services;

namespace ViewModel.VMs.Request
{
    /// <summary>
    /// Класс представления модели для выполнения запросов просмотра данных с названием таблицы.
    /// </summary>
    public class ViewDataRequestsVM : RequestsVM
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
                    ExecuteCommand($"SELECT * FROM {TableName}");
                }
            }
        }

        /// <summary>
        /// Создаёт экземпляр класса <see cref="ViewDataRequestsVM"/>.
        /// </summary>
        /// <param name="session">Создатель контекста базы данных.</param>
        /// <param name="resourceService">Сервис ресурсов.</param>
        /// <param name="messageService">Сервис сообщений.</param>
        public ViewDataRequestsVM(ISession session,
            IResourceService resourceService, IMessageService messageService) :
            base(session, resourceService, messageService) { }
    }
}