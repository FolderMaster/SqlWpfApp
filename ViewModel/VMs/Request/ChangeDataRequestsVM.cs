using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using System;
using System.Collections.ObjectModel;

using ViewModel.Enums;
using ViewModel.Interfaces.DbContext;
using ViewModel.Interfaces.Services;
using ViewModel.Interfaces.Services.Messages;

namespace ViewModel.VMs.Request
{
    /// <summary>
    /// Класс представления модели для выполнения запросов изменения данных с режимом изменения
    /// данных, названием таблицы, коллекцией представлений модели для параметров и командой
    /// выполнения.
    /// </summary>
    public partial class ChangeDataRequestsVM : ChangeRequestsVM
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

        /// <summary>
        /// Коллекция представлений модели для параметров.
        /// </summary>
        [ObservableProperty]
        private ObservableCollection<ParametersVM> parametersVMs = new()
        {
            new ParametersVM(new object[] { 0, 0, 0, "", DateTime.Now.Year, "" }),
            new ParametersVM(new object[] { 0, 0, 0, "", DateTime.Now.Year, "" }),
            new ParametersVM(new object[] { 0 }),
            new ParametersVM(new object[] { 0, 0, 0, 0, "", DateTime.Now }),
            new ParametersVM(new object[] { 0, 0, 0, 0, "", DateTime.Now }),
            new ParametersVM(new object[] { 0 }),
        };

        /// <summary>
        /// Возвращает и задаёт команду выполнения.
        /// </summary>
        public RelayCommand ExecuteCommand { get; private set; }

        /// <summary>
        /// Создаёт экземпляр класса <see cref="ChangeDataRequestsVM"/>.
        /// </summary>
        /// <param name="dbContextBuilder">Создатель контекста базы данных.</param>
        /// <param name="resourceService">Сервис ресурсов.</param>
        /// <param name="messageService">Сервис сообщений.</param>
        public ChangeDataRequestsVM(IDbContextBuilder dbContextBuilder,
            IResourceService resourceService, IMessageService messageService) :
            base(dbContextBuilder, resourceService, messageService) => 
            ExecuteCommand = new RelayCommand(() => ExecuteCommand(CreateCommand()));

        /// <summary>
        /// Создаёт команду.
        /// </summary>
        /// <returns>Команда.</returns>
        private string CreateCommand()
        {
            var parametersIndex = 3 * ((int)TableName - 1) + ((int)ChangeDataMode - 1);
            var parametersVM = ParametersVMs[parametersIndex];
            var command = "";
            switch (TableName)
            {
                case TableName.Students: switch(ChangeDataMode)
                    {
                        case ChangeDataMode.Insert: command = CreateInsertCommand($"{TableName}",
                            "ID, RecordBookNumber, IsDeductible, GroupNumber, " +
                            "GroupFormationYear, ScholarshipName",
                            $"{parametersVM.Parameters[0]}, {parametersVM.Parameters[1]}, " +
                            $"{parametersVM.Parameters[2]}, '{parametersVM.Parameters[3]}', " +
                            $"{parametersVM.Parameters[4]}, '{parametersVM.Parameters[5]}'"); break;
                        case ChangeDataMode.Update: command = CreateUpdateCommand($"{TableName}",
                            $"RecordBookNumber = {parametersVM.Parameters[1]}, " +
                            $"IsDeductible = {parametersVM.Parameters[2]}, " +
                            $"GroupNumber = '{parametersVM.Parameters[3]}', " +
                            $"GroupFormationYear = {parametersVM.Parameters[4]}, " +
                            $"ScholarshipName = '{parametersVM.Parameters[5]}'",
                            $"ID = {ParametersVMs[parametersIndex].Parameters[0]}"); break;
                        case ChangeDataMode.Delete: command = CreateDeleteCommand($"{TableName}",
                            $"ID = {ParametersVMs[parametersIndex].Parameters[0]}"); break;
                    } break;
                case TableName.GradeStatements: switch (ChangeDataMode)
                    {
                        case ChangeDataMode.Insert: command = CreateInsertCommand($"{TableName}",
                            "ID, StudentID, DisciplineID, TeacherID, GradeName, PassingDate",
                            $"{parametersVM.Parameters[0]}, {parametersVM.Parameters[1]}, " +
                            $"{parametersVM.Parameters[2]}, {parametersVM.Parameters[3]}, " +
                            $"'{parametersVM.Parameters[4]}', '{parametersVM.Parameters[5]}'"); break;
                        case ChangeDataMode.Update: command = CreateUpdateCommand($"{TableName}",
                            $"StudentID = {parametersVM.Parameters[1]}, " +
                            $"DisciplineID = {parametersVM.Parameters[2]}, " +
                            $"TeacherID = {parametersVM.Parameters[3]}, " +
                            $"GradeName = '{parametersVM.Parameters[4]}', " +
                            $"PassingDate = '{parametersVM.Parameters[5]}'",
                            $"ID = {parametersVM.Parameters[0]}"); break;
                        case ChangeDataMode.Delete: command = CreateDeleteCommand($"{TableName}",
                            $"ID = {parametersVM.Parameters[0]}"); break;
                    } break;
            }
            return command + ";" + CreateSelectCommand("*", $"{TableName}");
        }
    }
}