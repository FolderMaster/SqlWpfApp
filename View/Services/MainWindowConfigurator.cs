using View.Implementations.MessageBoxes;
using View.Implementations.Proces;
using View.Implementations.Proces.DbSet.Dependent;
using View.Implementations.Proces.DbSet.Independent;
using View.Windows;

using ViewModel.Interfaces;
using ViewModel.Interfaces.DataBase;
using ViewModel.Interfaces.Services;
using ViewModel.Interfaces.Services.Messages;
using ViewModel.Interfaces.Technicals;
using ViewModel.Services;

namespace View.Services
{
    /// <summary>
    /// Класс конфигуратора основного окна <seealso cref="MainWindow"/> с аргументами конфигурации
    /// и методом конфигурации. Реализует <see cref="IConfigurator"/>.
    /// </summary>
    public class MainWindowConfigurator : IConfigurator
    {
        /// <summary>
        /// Возвращает и задаёт основное окно.
        /// </summary>
        public MainWindow MainWindow { set; get; }

        /// <summary>
        /// Возвращает и задаёт создателя контекста базы данных.
        /// </summary>
        public IDbContextBuilder DbContextBuilder { get; set; }

        /// <summary>
        /// Возвращает и задаёт сервис ресурсов.
        /// </summary>
        public IResourceService ResourceService { get; set; }

        /// <summary>
        /// Возвращает и задаёт конфигурацию.
        /// </summary>
        public IConfiguration Configuration { get; set; }

        /// <summary>
        /// Возвращает и задаёт сервис сообщений о вопросе.
        /// </summary>
        public IMessageService QuestionMessageService { get; set; }

        /// <summary>
        /// Возвращает и задаёт сервис информационных сообщений.
        /// </summary>
        public IMessageService InformationMessageService { get; set; }

        /// <summary>
        /// Возвращает и задаёт сервис сообщений об ошибках.
        /// </summary>
        public IMessageService ErrorMessageService { get; set; }

        /// <summary>
        /// Возвращает и задаёт сервис закрытия приложения.
        /// </summary>
        public IAppCloseable AppCloseable { get; set; }

        public IProc ConnectionProc { get; set; }

        /// <summary>
        /// Возвращает и задаёт процедуру для работы с факультетами.
        /// </summary>
        public IProc DepartmentsProc { get; set; }

        /// <summary>
        /// Возвращает и задаёт процедуру для работы с паспортами.
        /// </summary>
        public IProc PassportsProc { get; set; }

        /// <summary>
        /// Возвращает и задаёт процедуру для работы с должностями.
        /// </summary>
        public IProc PositionsProc { get; set; }

        /// <summary>
        /// Возвращает и задаёт процедуру для работы с режимами оценивания.
        /// </summary>
        public IProc GradeModesProc { get; set; }

        /// <summary>
        /// Возвращает и задаёт процедуру для работы с ролями.
        /// </summary>
        public IProc RolesProc { get; set; }

        /// <summary>
        /// Возвращает и задаёт процедуру для работы со стипендиями.
        /// </summary>
        public IProc ScholarshipsProc { get; set; }

        /// <summary>
        /// Возвращает и задаёт процедуру для работы с дисциплинами.
        /// </summary>
        public IProc DisciplinesProc { get; set; }

        /// <summary>
        /// Возвращает и задаёт процедуру для работы с оценками.
        /// </summary>
        public IProc GradesProc { get; set; }

        /// <summary>
        /// Возвращает и задаёт процедуру для работы с ведомостями оценивания.
        /// </summary>
        public IProc GradeStatementsProc { get; set; }

        /// <summary>
        /// Возвращает и задаёт процедуру для работы с персонами.
        /// </summary>
        public IProc PersonsProc { get; set; }

        /// <summary>
        /// Возвращает и задаёт процедуру для работы с специальностями.
        /// </summary>
        public IProc SpecialtiesProc { get; set; }

        /// <summary>
        /// Возвращает и задаёт процедуру для работы со студентами.
        /// </summary>
        public IProc StudentsProc { get; set; }

        /// <summary>
        /// Возвращает и задаёт процедуру для работы с группами.
        /// </summary>
        public IProc GroupsProc { get; set; }

        /// <summary>
        /// Возвращает и задаёт процедуру для работы с режимами обучения.
        /// </summary>
        public IProc StudyFormsProc { get; set; }

        /// <summary>
        /// Возвращает и задаёт процедуру для работы с преподавателями.
        /// </summary>
        public IProc TeachersProc { get; set; }

        /// <summary>
        /// Возвращает и задаёт процедуру для работы с связями между дисциплинами и студентами.
        /// </summary>
        public IProc StudentDisciplineConnectionsProc { get; set; }

        /// <summary>
        /// Возвращает и задаёт процедуру для работы с связями между дисциплинами и преподавателями.
        /// </summary>
        public IProc TeacherDisciplineConnectionsProc { get; set; }

        /// <summary>
        /// Возвращает и задаёт процедуру для работы с запросами.
        /// </summary>
        public IProc RequestsProc { get; set; }

        /// <summary>
        /// Возвращает и задаёт процедуру для работы с отчётами.
        /// </summary>
        public IProc ReportsProc { get; set; }

        /// <summary>
        /// Создаёт экземпляр класса <see cref="MainWindowConfigurator"/>.
        /// </summary>
        /// <param name="mainWindow">Основное окно.</param>
        /// <param name="dbContextBuilder">Создатель контекста базы данных.</param>
        /// <param name="resourceService">Сервис ресурсов.</param>
        /// <param name="configuration">Конфигурация.</param>
        /// <param name="questionMessageService">Сервис сообщений о вопросах.</param>
        /// <param name="informationMessageService">Сервис информационных сообщений.</param>
        /// <param name="errorMessageService">Сервис сообщений об ошибках.</param>
        /// <param name="appCloseable">Сервис закрытия приложения.</param>
        /// <param name="departmentsProc">Оконная процедура работы с факультетами.</param>
        /// <param name="passportsProc">Оконная процедура работы с паспортами.</param>
        /// <param name="positionsProc">Оконная процедура работы с должностями.</param>
        /// <param name="gradeModesProc">Оконная процедура работы с режимами оценивания.</param>
        /// <param name="rolesProc">Оконная процедура работы с ролями.</param>
        /// <param name="scholarshipsProc">Оконная процедура работы со стипендиями.</param>
        /// <param name="disciplinesProc">Оконная процедура работы с дисциплинами.</param>
        /// <param name="gradesProc">Оконная процедура работы с оценками.</param>
        /// <param name="gradeStatementsProc">Оконная процедура работы с ведомостями
        /// оценивания.</param>
        /// <param name="personsProc">Оконная процедура работы с персонами.</param>
        /// <param name="specialtiesProc">Оконная процедура работы со специальностями.</param>
        /// <param name="studentsProc">Оконная процедура работы со студентами.</param>
        /// <param name="groupsProc">Оконная процедура работы с группами.</param>
        /// <param name="studyFormsProc">Оконная процедура работы с режимами обучения.</param>
        /// <param name="teachersProc">Оконная процедура работы с преподавателями.</param>
        /// <param name="studentDisciplineConnectionsProc">Оконная процедура работы со связями
        /// между дисциплинами и студентами.</param>
        /// <param name="teacherDisciplineConnectionsProc">Оконная процедура работы со связями
        /// между дисциплинами и преподавателями.</param>
        /// <param name="requestsProc">Оконная процедура работы с запросами.</param>
        /// <param name="reportsProc">Оконная процедура работы с отчётами.</param>
        public MainWindowConfigurator(MainWindow mainWindow, IDbContextBuilder dbContextBuilder,
            IResourceService resourceService, IConfiguration configuration,
            QuestionMessageBoxService questionMessageService,
            InformationMessageBoxService informationMessageService,
            ErrorMessageBoxService errorMessageService, IAppCloseable appCloseable,
            ConnectionWindowProc connectionProc,
            DepartmentsWindowProc departmentsProc, PassportsWindowProc passportsProc,
            PositionsWindowProc positionsProc, GradeModesWindowProc gradeModesProc,
            RolesWindowProc rolesProc, ScholarshipsWindowProc scholarshipsProc,
            DisciplinesWindowProc disciplinesProc, GradesWindowProc gradesProc,
            GradeStatementsWindowProc gradeStatementsProc, PersonsWindowProc personsProc,
            SpecialtiesWindowProc specialtiesProc, StudentsWindowProc studentsProc,
            GroupsWindowProc groupsProc, StudyFormsWindowProc studyFormsProc,
            TeachersWindowProc teachersProc,
            StudentDisciplineConnectionsWindowProc studentDisciplineConnectionsProc,
            TeacherDisciplineConnectionsWindowProc teacherDisciplineConnectionsProc,
            RequestsWindowProc requestsProc, ReportsWindowProc reportsProc)
        {
            MainWindow = mainWindow;
            DbContextBuilder = dbContextBuilder;
            ResourceService = resourceService;
            Configuration = configuration;
            QuestionMessageService = questionMessageService;
            InformationMessageService = informationMessageService;
            ErrorMessageService = errorMessageService;
            AppCloseable = appCloseable;
            ConnectionProc = connectionProc;
            DepartmentsProc = departmentsProc;
            PassportsProc = passportsProc;
            PositionsProc = positionsProc;
            GradeModesProc = gradeModesProc;
            RolesProc = rolesProc;
            ScholarshipsProc = scholarshipsProc;
            DisciplinesProc = disciplinesProc;
            GradesProc = gradesProc;
            GradeStatementsProc = gradeStatementsProc;
            PersonsProc = personsProc;
            SpecialtiesProc = specialtiesProc;
            StudentsProc = studentsProc;
            GroupsProc = groupsProc;
            StudyFormsProc = studyFormsProc;
            TeachersProc = teachersProc;
            StudentDisciplineConnectionsProc = studentDisciplineConnectionsProc;
            TeacherDisciplineConnectionsProc = teacherDisciplineConnectionsProc;
            RequestsProc = requestsProc;
            ReportsProc = reportsProc;
        }

        /// <summary>
        /// Конфигурирует основное окно <seealso cref="MainWindow"/>.
        /// </summary>
        public void Configure()
        {
            var creator = new MainVMCreator()
            {
                DbContextBuilder = DbContextBuilder,
                ResourceService = ResourceService,
                Configuration = Configuration,
                QuestionMessageService = QuestionMessageService,
                InformationMessageService = InformationMessageService,
                ErrorMessageService = ErrorMessageService,
                AppCloseable = AppCloseable,
                ConnectionProc = ConnectionProc,
                DepartmentsProc = DepartmentsProc,
                PassportsProc = PassportsProc,
                PositionsProc = PositionsProc,
                DisciplinesProc = DisciplinesProc,
                GradesProc = GradesProc,
                GradeModesProc = GradeModesProc,
                RolesProc = RolesProc,
                ScholarshipsProc = ScholarshipsProc,
                GradeStatementsProc = GradeStatementsProc,
                PersonsProc = PersonsProc,
                SpecialtiesProc = SpecialtiesProc,
                StudentsProc = StudentsProc,
                GroupsProc = GroupsProc,
                StudyFormsProc = StudyFormsProc,
                TeachersProc = TeachersProc,
                StudentDisciplineConnectionsProc = StudentDisciplineConnectionsProc,
                TeacherDisciplineConnectionsProc = TeacherDisciplineConnectionsProc,
                RequestsProc = RequestsProc,
                ReportsProc = ReportsProc
            };

            MainWindow.DataContext = creator.Create();
        }
    }
}