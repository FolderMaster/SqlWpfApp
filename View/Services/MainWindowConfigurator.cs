using View.Windows;

using ViewModel.Interfaces;
using ViewModel.Services;

namespace View.Services
{
    public class MainWindowConfigurator
    {
        public MainWindow MainWindow { set; get; }

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
        
        public IProc GradeStatementsProc { get; set; }
        
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

        public void Confugure()
        {
            var creator = new MainVMCreator()
            {
                DbContextCreator = DbContextCreator,
                ResourceService = ResourceService,
                Configurational = Configurational,
                QuestionMessageService = QuestionMessageService,
                InformationMessageService = InformationMessageService,
                ErrorMessageService = ErrorMessageService,
                AppCloseable = AppCloseable,
                DepartmentsProc = DepartmentsProc,
                PassportsProc = PassportsProc,
                PositionsProc = PositionsProc,
                DisciplinesProc = DisciplinesProc,
                GradesProc = GradesProc,
                GradeModesProc = GradeModesProc,
                RolesProc = RolesProc,
                ScholarshipsProc = ScholarshipsProc,
                GradeStatementsWindowProc = GradeStatementsProc,
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