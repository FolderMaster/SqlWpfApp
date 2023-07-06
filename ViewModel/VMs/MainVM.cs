using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using ViewModel.Interfaces;
using ViewModel.Services;

namespace ViewModel.VMs
{
    /// <summary>
    /// Класс основного представления модели с командами.
    /// </summary>
    public class MainVM : ObservableObject
    {
        /// <summary>
        /// Ключ ресурса заголовка выхода.
        /// </summary>
        private static string _exitTitleResourceKey = "QuestionMessageTitle";

        /// <summary>
        /// Ключ ресурса описания выхода.
        /// </summary>
        private static string _exitDescriptionResourceKey = "ExitMessageDescription";

        /// <summary>
        /// Ключ ресурса заголовка информации.
        /// </summary>
        private static string _informationTitleResourceKey = "InformationMessageTitle";

        /// <summary>
        /// Ключ ресурса описания информации.
        /// </summary>
        private static string _informationDescriptionResourceKey =
            "InformationMessageDescription";

        /// <summary>
        /// Сервис послания сообщений.
        /// </summary>
        private MessengerService _messengerService;

        /// <summary>
        /// Конфигурация.
        /// </summary>
        private IConfiguration _configuration;

        /// <summary>
        /// Возвращает и задаёт команду сохранения.
        /// </summary>
        public RelayCommand SaveCommand { get; private set; }

        /// <summary>
        /// Возвращает и задаёт команду загрузки.
        /// </summary>
        public RelayCommand LoadCommand { get; private set; }

        /// <summary>
        /// Возвращает и задаёт команду выхода.
        /// </summary>
        public RelayCommand ExitCommand { get; private set; }

        /// <summary>
        /// Возвращает и задаёт команду получения информации.
        /// </summary>
        public RelayCommand InformationCommand { get; private set; }

        /// <summary>
        /// Возвращает и задаёт команду вызова процедуры работы с факультетами.
        /// </summary>
        public RelayCommand DepartmentsProcInvokeCommand { get; private set; }

        /// <summary>
        /// Возвращает и задаёт команду вызова процедуры работы с паспортами.
        /// </summary>
        public RelayCommand PassportsProcInvokeCommand { get; private set; }

        /// <summary>
        /// Возвращает и задаёт команду вызова процедуры работы с должностями.
        /// </summary>
        public RelayCommand PositionsProcInvokeCommand { get; private set; }

        /// <summary>
        /// Возвращает и задаёт команду вызова процедуры работы с режимами оценивания.
        /// </summary>
        public RelayCommand GradeModesProcInvokeCommand { get; private set; }

        /// <summary>
        /// Возвращает и задаёт команду вызова процедуры работы с ролями.
        /// </summary>
        public RelayCommand RolesProcInvokeCommand { get; private set; }

        /// <summary>
        /// Возвращает и задаёт команду вызова процедуры работы с стипендиями.
        /// </summary>
        public RelayCommand ScholarshipsProcInvokeCommand { get; private set; }

        /// <summary>
        /// Возвращает и задаёт команду вызова процедуры работы с дисциплинами.
        /// </summary>
        public RelayCommand DisciplinesProcInvokeCommand { get; private set; }

        /// <summary>
        /// Возвращает и задаёт команду вызова процедуры работы с оценками.
        /// </summary>
        public RelayCommand GradesProcInvokeCommand { get; private set; }

        /// <summary>
        /// Возвращает и задаёт команду вызова процедуры работы с ведомостями оценивания.
        /// </summary>
        public RelayCommand GradeStatementsProcInvokeCommand { get; private set; }

        /// <summary>
        /// Возвращает и задаёт команду вызова процедуры работы с персонами.
        /// </summary>
        public RelayCommand PersonsProcInvokeCommand { get; private set; }

        /// <summary>
        /// Возвращает и задаёт команду вызова процедуры работы со специальностями.
        /// </summary>
        public RelayCommand SpecialtiesProcInvokeCommand { get; private set; }

        /// <summary>
        /// Возвращает и задаёт команду вызова процедуры работы со студентами.
        /// </summary>
        public RelayCommand StudentsProcInvokeCommand { get; private set; }

        /// <summary>
        /// Возвращает и задаёт команду вызова процедуры работы с группами.
        /// </summary>
        public RelayCommand GroupsProcInvokeCommand { get; private set; }

        /// <summary>
        /// Возвращает и задаёт команду вызова процедуры работы с режимами обучения.
        /// </summary>
        public RelayCommand StudyFormsProcInvokeCommand { get; private set; }

        /// <summary>
        /// Возвращает и задаёт команду вызова процедуры работы с преподавателями.
        /// </summary>
        public RelayCommand TeachersProcInvokeCommand { get; private set; }

        /// <summary>
        /// Возвращает и задаёт команду вызова процедуры работы со связями между дисциплинами и
        /// студентами.
        /// </summary>
        public RelayCommand StudentDisciplineConnectionsProcInvokeCommand { get; private set; }

        /// <summary>
        /// Возвращает и задаёт команду вызова процедуры работы со связями между дисциплинами и
        /// преподавателями.
        /// </summary>
        public RelayCommand TeacherDisciplineConnectionsProcInvokeCommand { get; private set; }

        /// <summary>
        /// Возвращает и задаёт команду вызова процедуры работы с запросами.
        /// </summary>
        public RelayCommand RequestsProcInvokeCommand { get; private set; }

        /// <summary>
        /// Возвращает и задаёт команду вызова процедуры работы с отчётами.
        /// </summary>
        public RelayCommand ReportsProcInvokeCommand { get; private set; }

        /// <summary>
        /// Создаёт экземпляр класса <see cref="MainVM"/>.
        /// </summary>
        /// <param name="dbContextBuilder">Создатель контекста базы данных.</param>
        /// <param name="resourceService">Сервис ресурсов.</param>
        /// <param name="configuration">Конфигурация.</param>
        /// <param name="exitMessageService">Сервис сообщений о выходе.</param>
        /// <param name="informationMessageService">Сервис информационных сообщений.</param>
        /// <param name="errorMessageService">Сервис сообщений об ошибках.</param>
        /// <param name="appCloseable">Сервис закрытия приложения.</param>
        /// <param name="departmentsProc">Процедура работы с факультетами.</param>
        /// <param name="passportsProc">Процедура работы с паспортами.</param>
        /// <param name="positionsProc">Процедура работы с должностями.</param>
        /// <param name="gradeModesProc">Процедура работы с режимами оценивания.</param>
        /// <param name="rolesProc">Процедура работы с ролями.</param>
        /// <param name="scholarshipsProc">Процедура работы со стипендиями.</param>
        /// <param name="disciplinesProc">Процедура работы с дисциплинами.</param>
        /// <param name="gradesProc">Процедура работы с оценками.</param>
        /// <param name="gradeStatementsProc">Процедура работы с ведомостями оценивания.</param>
        /// <param name="personsProc">Процедура работы с персонами.</param>
        /// <param name="specialtiesProc">Процедура работы со специальностями.</param>
        /// <param name="studentsProc">Процедура работы со студентами.</param>
        /// <param name="groupsProc">Процедура работы с группами.</param>
        /// <param name="studyFormsProc">Процедура работы с режимами обучения.</param>
        /// <param name="teachersProc">Процедура работы с преподавателями.</param>
        /// <param name="studentDisciplineConnectionsProc">Процедура работы со связями между
        /// дисциплинами и студентами.</param>
        /// <param name="teacherDisciplineConnectionsProc">Процедура работы со связями между
        /// дисциплинами и преподавателями.</param>
        /// <param name="requestsProc">Процедура работы с запросами.</param>
        /// <param name="reportsProc">Процедура работы с отчётами.</param>
        public MainVM(IDbContextBuilder dbContextBuilder, IResourceService resourceService,
            IConfiguration configuration, IMessageService exitMessageService,
            IMessageService informationMessageService, IMessageService errorMessageService,
            IAppCloseable appCloseable, IProc departmentsProc, IProc passportsProc,
            IProc positionsProc, IProc gradeModesProc, IProc rolesProc, IProc scholarshipsProc,
            IProc disciplinesProc, IProc gradesProc, IProc gradeStatementsProc, IProc personsProc,
            IProc specialtiesProc, IProc studentsProc, IProc groupsProc, IProc studyFormsProc,
            IProc teachersProc, IProc studentDisciplineConnectionsProc,
            IProc teacherDisciplineConnectionsProc, IProc requestsProc, IProc reportsProc)
        {
            _configuration = configuration;
            _messengerService = new MessengerService(resourceService, errorMessageService);

            SaveCommand = new RelayCommand(() =>
                _messengerService.ExecuteWithExceptionMessage(_configuration.Save));
            LoadCommand = new RelayCommand(() =>
                _messengerService.ExecuteWithExceptionMessage(_configuration.Load));

            ExitCommand = new RelayCommand(() =>
            {
                if (_messengerService.ShowMessage(exitMessageService, _exitTitleResourceKey,
                    _exitDescriptionResourceKey))
                {
                    appCloseable.CloseApp();
                }
            });
            InformationCommand = new RelayCommand(() =>
                _messengerService.ShowMessage(informationMessageService,
                _informationTitleResourceKey, _informationDescriptionResourceKey));

            DepartmentsProcInvokeCommand = new RelayCommand(departmentsProc.Invoke);
            PassportsProcInvokeCommand = new RelayCommand(passportsProc.Invoke);
            PositionsProcInvokeCommand = new RelayCommand(positionsProc.Invoke);
            GradeModesProcInvokeCommand = new RelayCommand(gradeModesProc.Invoke);
            ScholarshipsProcInvokeCommand = new RelayCommand(scholarshipsProc.Invoke);
            RolesProcInvokeCommand = new RelayCommand(rolesProc.Invoke);

            DisciplinesProcInvokeCommand = new RelayCommand(disciplinesProc.Invoke);
            GradesProcInvokeCommand = new RelayCommand(gradesProc.Invoke);
            GradeStatementsProcInvokeCommand = new RelayCommand(gradeStatementsProc.Invoke);
            PersonsProcInvokeCommand = new RelayCommand(personsProc.Invoke);
            SpecialtiesProcInvokeCommand = new RelayCommand(specialtiesProc.Invoke);
            StudentsProcInvokeCommand = new RelayCommand(studentsProc.Invoke);
            GroupsProcInvokeCommand = new RelayCommand(groupsProc.Invoke);
            StudyFormsProcInvokeCommand = new RelayCommand(studyFormsProc.Invoke);
            TeachersProcInvokeCommand = new RelayCommand(teachersProc.Invoke);

            StudentDisciplineConnectionsProcInvokeCommand =
                new RelayCommand(studentDisciplineConnectionsProc.Invoke);
            TeacherDisciplineConnectionsProcInvokeCommand =
                new RelayCommand(teacherDisciplineConnectionsProc.Invoke);

            RequestsProcInvokeCommand = new RelayCommand(requestsProc.Invoke);
            ReportsProcInvokeCommand = new RelayCommand(reportsProc.Invoke);

            LoadCommand.Execute(null);

            _messengerService.ExecuteWithExceptionMessage(() =>
                dbContextBuilder.Create(_configuration.DataBaseConnectionString));
        }
    }
}