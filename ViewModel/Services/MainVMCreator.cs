using ViewModel.Interfaces;
using ViewModel.VMs;

namespace ViewModel.Services
{
    public class MainVMCreator
    {
        public IDbContextBuilder DbContextCreator { get; set; }

        public IResourceService ResourceService { get; set; }

        public IConfiguration Configurational { get; set; }

        public IMessageService QuestionMessageService { get; set; }

        public IMessageService InformationMessageService { get; set; }

        public IMessageService ErrorMessageService { get; set; }

        public IAppCloseable AppCloseable { get; set; }

        public IProc DepartmentsProc { get; set; }

        public IProc PassportsProc { get; set; }

        public IProc PositionsProc { get; set; }

        public IProc GradeModesProc { get; set; }

        public IProc RolesProc { get; set; }

        public IProc ScholarshipsProc { get; set; }

        public IProc DisciplinesProc { get; set; }

        public IProc GradesProc { get; set; }

        public IProc GradeStatementsWindowProc { get; set; }

        public IProc PersonsProc { get; set; }

        public IProc SpecialtiesProc { get; set; }

        public IProc StudentsProc { get; set; }

        public IProc GroupsProc { get; set; }

        public IProc StudyFormsProc { get; set; }

        public IProc TeachersProc { get; set; }

        public IProc StudentDisciplineConnectionsProc { get; set; }

        public IProc TeacherDisciplineConnectionsProc { get; set; }

        public IProc RequestsProc { get; set; }

        public IProc ReportsProc { get; set; }

        public MainVM Create() => new MainVM(DbContextCreator, ResourceService, Configurational,
            QuestionMessageService, InformationMessageService, ErrorMessageService, AppCloseable,
            DepartmentsProc, PassportsProc, PositionsProc, GradeModesProc, RolesProc,
            ScholarshipsProc, DisciplinesProc, GradesProc, GradeStatementsWindowProc, PersonsProc,
            SpecialtiesProc, StudentsProc, GroupsProc, StudyFormsProc, TeachersProc,
            StudentDisciplineConnectionsProc, TeacherDisciplineConnectionsProc, RequestsProc,
            ReportsProc);
    }
}