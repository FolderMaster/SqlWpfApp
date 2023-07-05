using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using ViewModel.Interfaces;
using ViewModel.Services;

namespace ViewModel.VMs
{
    public class MainVM : ObservableObject
    {
        private static string _exitTitleResourceKey = "ImageOpenFileDialogFilter";

        private static string _exitDescriptionResourceKey = "ExitMessageDescription";

        private static string _informationTitleResourceKey = "InformationMessageTitle";

        private static string _informationDescriptionResourceKey =
            "InformationMessageDescription";

        private MessengerService _messengerService;

        private IConfiguration _configurational;

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

        public MainVM(IDbContextBuilder dbContextCreator, IResourceService resourceService,
            IConfiguration configurational, IMessageService exitMessageService,
            IMessageService informationMessageService, IMessageService errorMessageService,
            IAppCloseable appCloseable, IProc departmentsProc, IProc passportsProc,
            IProc positionsProc, IProc gradeModesProc, IProc rolesProc, IProc scholarshipsProc,
            IProc disciplinesProc, IProc gradesProc, IProc gradeStatementsProc, IProc personsProc,
            IProc specialtiesProc, IProc studentsProc, IProc groupsProc, IProc studyFormsProc,
            IProc teachersProc, IProc studentDisciplineConnectionsProc,
            IProc teacherDisciplineConnectionsProc, IProc requestsProc, IProc reportsProc)
        {
            _configurational = configurational;
            _messengerService = new MessengerService(resourceService, errorMessageService);

            SaveCommand = new RelayCommand(() =>
                _messengerService.ExecuteWithExceptionMessage(_configurational.Save));
            LoadCommand = new RelayCommand(() =>
                _messengerService.ExecuteWithExceptionMessage(_configurational.Load));

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
                dbContextCreator.Create(_configurational.DataBaseConnectionString));
        }
    }
}