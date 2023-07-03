using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using ViewModel.Interfaces;

namespace ViewModel.VMs
{
    public class MainVM : ObservableObject
    {
        public IDbContextCreator DataBaseContextCreator { get; private set; }

        public IConfigurational Configurational { get; private set; }

        public RelayCommand SaveCommand { get; private set; }

        public RelayCommand LoadCommand { get; private set; }

        public RelayCommand ExitCommand { get; private set; }

        public RelayCommand InformationCommand { get; private set; }

        public RelayCommand DepartmentsProcInvokeCommand { get; private set; }

        public RelayCommand PassportsProcInvokeCommand { get; private set; }

        public RelayCommand PositionsProcInvokeCommand { get; private set; }

        public RelayCommand GradeModesProcInvokeCommand { get; private set; }

        public RelayCommand RolesProcInvokeCommand { get; private set; }

        public RelayCommand ScholarshipsProcInvokeCommand { get; private set; }

        public RelayCommand DisciplinesProcInvokeCommand { get; private set; }

        public RelayCommand GradesProcInvokeCommand { get; private set; }

        public RelayCommand GradeStatementsProcInvokeCommand { get; private set; }

        public RelayCommand PersonsProcInvokeCommand { get; private set; }

        public RelayCommand SpecialtiesProcInvokeCommand { get; private set; }

        public RelayCommand StudentsProcInvokeCommand { get; private set; }

        public RelayCommand GroupsProcInvokeCommand { get; private set; }

        public RelayCommand StudyFormsProcInvokeCommand { get; private set; }

        public RelayCommand TeachersProcInvokeCommand { get; private set; }

        public RelayCommand StudentDisciplineConnectionsProcInvokeCommand { get; private set; }

        public RelayCommand TeacherDisciplineConnectionsProcInvokeCommand { get; private set; }

        public RelayCommand RequestsProcInvokeCommand { get; private set; }

        public RelayCommand ReportsProcInvokeCommand { get; private set; }

        public MainVM(IDbContextCreator dataBaseContextCreator,
            IConfigurational configurational, IMessageService exitMessageService,
            IMessageService informationMessageService, IMessageService errorMessageService,
            IAppCloseable appCloseable, IProc departmentsProc, IProc passportsProc,
            IProc positionsProc, IProc gradeModesProc, IProc rolesProc, IProc scholarshipsProc,
            IProc disciplinesProc, IProc gradesProc, IProc gradeStatementsProc, IProc personsProc,
            IProc specialtiesProc, IProc studentsProc, IProc groupsProc, IProc studyFormsProc,
            IProc teachersProc, IProc studentDisciplineConnectionsProc,
            IProc teacherDisciplineConnectionsProc, IProc requestsProc, IProc reportsProc)
        {
            Configurational = configurational;

            SaveCommand = new RelayCommand(() => Configurational.Save(), () => true);
            LoadCommand = new RelayCommand(() => Configurational.Load(), () => true);

            ExitCommand = new RelayCommand(() =>
            {
                if (exitMessageService.ShowMessage("Do you want to close the program?", "Exit"))
                {
                    appCloseable.CloseApp();
                }
            });
            InformationCommand = new RelayCommand(() =>
                informationMessageService.ShowMessage("(C)TUSUR, KSUB, Pchelintsev Andrew" +
                    " Alexandrovich, group 571-2, 2023.", "About program"));

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
            DataBaseContextCreator = dataBaseContextCreator;
            DataBaseContextCreator.Create(Configurational.DataBaseConnectionString);
        }
    }
}