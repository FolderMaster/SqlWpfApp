using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

using ViewModel.Enums;
using ViewModel.Interfaces.DataBase;
using ViewModel.Interfaces.Services;
using ViewModel.Interfaces.Services.Messages;

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

        [ObservableProperty]
        public StudentsUpdateParametersVM studentsUpdateParametersVM = new();

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
            ExecuteCommand = new RelayCommand(() => ExecuteCommand(CreateCommand(), CreateParameters()));

        /// <summary>
        /// Создаёт команду.
        /// </summary>
        /// <returns>Команда.</returns>
        private string CreateCommand()
        {
            var command = "";
            switch (TableName)
            {
                case TableName.Students:
                    switch (ChangeDataMode)
                    {
                        case ChangeDataMode.Insert:
                            command = "INSERT INTO Students" +
                                "(ID, RecordBookNumber, IsDeductible, GroupNumber, " +
                                "GroupFormationYear, ScholarshipName) " +
                                "VALUES (@ID, @RecordBookNumber, @IsDeductible, @GroupNumber, " +
                                "@GroupFormationYear, @ScholarshipName)";
                            break;
                        case ChangeDataMode.Update:
                            command = "UPDATE Students " +
                                "SET RecordBookNumber = @RecordBookNumber, " +
                                "IsDeductible = @IsDeductible, GroupNumber = @GroupNumber, " +
                                "GroupFormationYear = @GroupFormationYear, " +
                                "ScholarshipName = @ScholarshipName " +
                                "WHERE ID = @ID";
                            break;
                        case ChangeDataMode.Delete:
                            command = "DELETE FROM Students " +
                                "WHERE ID = @ID";
                            break;
                    }
                    break;
                case TableName.GradeStatements:
                    switch (ChangeDataMode)
                    {
                        case ChangeDataMode.Insert:
                            command = "INSERT INTO GradeStatements " +
                                "(StudentID, DisciplineID, TeacherID, GradeName, PassingDate) " +
                                "VALUES (@StudentID, @DisciplineID, @TeacherID, @GradeName, " +
                                "@PassingDate)";
                            break;
                        case ChangeDataMode.Update:
                            command = "UPDATE GradeStatements" +
                                "SET StudentID = @StudentID, DisciplineID = @DisciplineID, " +
                                "TeacherID = @TeacherID, GradeName = @GradeName, " +
                                "PassingDate = @PassingDate " +
                                "WHERE ID = @ID";
                            break;
                        case ChangeDataMode.Delete:
                            command = "DELETE FROM GradeStatements " +
                                "WHERE ID = @ID";
                            break;
                    }
                    break;
            }
            return $"{command}; SELECT * FROM {TableName}";
        }

        private Dictionary<string, object> CreateParameters()
        {
            var parameters = new Dictionary<string, object>();
            switch (TableName)
            {
                case TableName.Students:
                    switch (ChangeDataMode)
                    {
                        case ChangeDataMode.Insert:
                            parameters.Add("@ID", null);
                            parameters.Add("@RecordBookNumber", null);
                            parameters.Add("@IsDeductible", null);
                            parameters.Add("@GroupNumber", null);
                            parameters.Add("@GroupFormationYear", null);
                            parameters.Add("@ScholarshipName", null);
                            break;
                        case ChangeDataMode.Update:
                            parameters.Add("@ID", StudentsUpdateParametersVM.ID);
                            parameters.Add("@RecordBookNumber", StudentsUpdateParametersVM.RecordBookNumber);
                            parameters.Add("@IsDeductible", StudentsUpdateParametersVM.IsDeductible);
                            parameters.Add("@GroupNumber", StudentsUpdateParametersVM.GroupNumber);
                            parameters.Add("@GroupFormationYear", StudentsUpdateParametersVM.GroupFormationYear);
                            parameters.Add("@ScholarshipName", StudentsUpdateParametersVM.ScholarshipName);
                            break;
                        case ChangeDataMode.Delete:
                            parameters.Add("@ID", null);
                            break;
                    }
                    break;
                case TableName.GradeStatements:
                    switch (ChangeDataMode)
                    {
                        case ChangeDataMode.Insert:
                            parameters.Add("@StudentID", null);
                            parameters.Add("@DisciplineID", null);
                            parameters.Add("@TeacherID", null);
                            parameters.Add("@GradeName", null);
                            parameters.Add("@PassingDate", null);
                            break;
                        case ChangeDataMode.Update:
                            parameters.Add("@ID", null);
                            parameters.Add("@StudentID", null);
                            parameters.Add("@DisciplineID", null);
                            parameters.Add("@TeacherID", null);
                            parameters.Add("@GradeName", null);
                            parameters.Add("@PassingDate", null);
                            break;
                        case ChangeDataMode.Delete:
                            parameters.Add("@ID", null);
                            break;
                    }
                    break;
            }
            return parameters;
        }
    }
}