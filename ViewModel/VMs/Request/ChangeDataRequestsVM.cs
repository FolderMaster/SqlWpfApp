using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using System;

using ViewModel.Enums;
using ViewModel.Interfaces;
using ViewModel.Interfaces.Services;
using ViewModel.VMs.Request.ParameterRequest;
using ViewModel.VMs.Request.ParameterRequest.ChangeData;

namespace ViewModel.VMs.Request
{
    /// <summary>
    /// Класс представления модели для выполнения запросов изменения данных с режимом изменения
    /// данных, названием таблицы, коллекцией представлений модели для параметров и командой
    /// выполнения.
    /// </summary>
    public partial class ChangeDataRequestsVM : RequestsVM
    {
        /// <summary>
        /// Режим изменения данных.
        /// </summary>
        [ObservableProperty]
        private ChangeDataMode changeDataMode = ChangeDataMode.Insert;

        /// <summary>
        /// Название таблицы.
        /// </summary>
        [ObservableProperty]
        private TableName tableName = TableName.Students;

        public InsertStudentRequestVM InsertStudentRequestVM { get; } = new();

        public UpdateStudentRequestVM UpdateStudentRequestVM { get; } = new();

        public DeleteStudentRequestVM DeleteStudentRequestVM { get; } = new();

        public InsertGradeStatementRequestVM InsertGradeStatementRequestVM { get; } = new();

        public UpdateGradeStatementRequestVM UpdateGradeStatementRequestVM { get; } = new();

        public DeleteGradeStatementRequestVM DeleteGradeStatementRequestVM { get; } = new();

        /// <summary>
        /// Возвращает и задаёт команду выполнения.
        /// </summary>
        public RelayCommand ExecuteCommand { get; private set; }

        /// <summary>
        /// Создаёт экземпляр класса <see cref="ChangeDataRequestsVM"/>.
        /// </summary>
        /// <param name="session">Создатель контекста базы данных.</param>
        /// <param name="resourceService">Сервис ресурсов.</param>
        /// <param name="messageService">Сервис сообщений.</param>
        public ChangeDataRequestsVM(ISession session,
            IResourceService resourceService, IMessageService messageService) :
            base(session, resourceService, messageService) => 
            ExecuteCommand = new RelayCommand(() =>
            {
                ParameterRequestVM requestVM = TableName switch
                {
                    TableName.Students => ChangeDataMode switch
                    {
                        ChangeDataMode.Insert => InsertStudentRequestVM,
                        ChangeDataMode.Update => UpdateStudentRequestVM,
                        ChangeDataMode.Delete => DeleteStudentRequestVM,
                        _ => throw new NotImplementedException()
                    },
                    TableName.GradeStatements => ChangeDataMode switch
                    {
                        ChangeDataMode.Insert => InsertGradeStatementRequestVM,
                        ChangeDataMode.Update => UpdateGradeStatementRequestVM,
                        ChangeDataMode.Delete => DeleteGradeStatementRequestVM,
                        _ => throw new NotImplementedException()
                    },
                    _ => throw new NotImplementedException()
                };
                var command = $"{requestVM.GetRequest()}; SELECT * FROM {requestVM.Table}";
                ExecuteCommand(command, requestVM.GetParameters());
            });
    }
}