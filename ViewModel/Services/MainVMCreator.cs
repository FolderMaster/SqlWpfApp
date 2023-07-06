using ViewModel.Interfaces;
using ViewModel.VMs;

namespace ViewModel.Services
{
    /// <summary>
    /// Класс создателя основного представления модели <seealso cref="MainVM"/> с аргументами
    /// создания и методом создания. Реализует <see cref="ICreator{T}"/>.
    /// </summary>
    public class MainVMCreator : ICreator<MainVM>
    {
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

        /// <summary>
        /// Возвращает и задаёт процедуру для работы с факультетами.
        /// </summary>
        public IProc DepartmentsProc { get; set; }

        /// <summary>
        /// Возвращает и задаёт процедуру для работы с пасспортами.
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
        /// Возвращает и задаёт процедуру для работы со связями между дисциплинами и студентами.
        /// </summary>
        public IProc StudentDisciplineConnectionsProc { get; set; }

        /// <summary>
        /// Возвращает и задаёт процедуру для работы со связями между дисциплинами и
        /// преподавателями.
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
        /// Создаёт основное представление модели <seealso cref="MainVM"/>.
        /// </summary>
        /// <returns>Основное представление модели <seealso cref="MainVM"/>.</returns>
        public MainVM Create() => new MainVM(DbContextBuilder, ResourceService, Configuration,
            QuestionMessageService, InformationMessageService, ErrorMessageService, AppCloseable,
            DepartmentsProc, PassportsProc, PositionsProc, GradeModesProc, RolesProc,
            ScholarshipsProc, DisciplinesProc, GradesProc, GradeStatementsProc, PersonsProc,
            SpecialtiesProc, StudentsProc, GroupsProc, StudyFormsProc, TeachersProc,
            StudentDisciplineConnectionsProc, TeacherDisciplineConnectionsProc, RequestsProc,
            ReportsProc);
    }
}