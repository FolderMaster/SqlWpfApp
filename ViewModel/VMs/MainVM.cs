using System;
using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using ViewModel.Services;

namespace ViewModel.VMs
{
    public class MainVM : ObservableObject
    {
        public IConfigurational Configurational { get; private set; }

        public RelayCommand SaveCommand { get; private set; }

        public RelayCommand LoadCommand { get; private set; }

        public RelayCommand ExitCommand { get; private set; }

        public RelayCommand InformationCommand { get; private set; }

        public RelayCommand DepartmentsCommand { get; private set; }

        public RelayCommand PassportsCommand { get; private set; }

        public RelayCommand PositionsCommand { get; private set; }

        public RelayCommand GradeModesCommand { get; private set; }

        public RelayCommand RolesCommand { get; private set; }

        public RelayCommand ScholarshipsCommand { get; private set; }

        public RelayCommand DisciplinesCommand { get; private set; }

        public RelayCommand GradesCommand { get; private set; }

        public RelayCommand GradeStatementsCommand { get; private set; }

        public RelayCommand PersonsCommand { get; private set; }

        public RelayCommand SpecialtiesCommand { get; private set; }

        public RelayCommand StudentsCommand { get; private set; }

        public RelayCommand GroupsCommand { get; private set; }

        public RelayCommand StudyFormsCommand { get; private set; }

        public RelayCommand TeachersCommand { get; private set; }

        public RelayCommand StudentDisciplineConnectionsCommand { get; private set; }

        public RelayCommand TeacherDisciplineConnectionsCommand { get; private set; }

        public RelayCommand RequestsCommand { get; private set; }

        public RelayCommand ReportsCommand { get; private set; }

        public MainVM(IConfigurational configurational, IMessageService exitMessageService,
            IMessageService informationMessageService, Action departmentsAction,
            Action passportsAction, Action positionsAction, Action gradeModesAction,
            Action rolesAction, Action scholarshipsAction, Action disciplinesAction,
            Action gradesAction, Action gradeStatementsAction, Action personsAction,
            Action specialtiesAction, Action studentsAction, Action groupsAction,
            Action studyFormsAction, Action teachersAction,
            Action studentDisciplineConnectionsAction, Action teacherDisciplineConnectionsAction,
            Action requestsAction, Action reportsAction)
        {
            Configurational = configurational;

            SaveCommand = new RelayCommand(() => Configurational.Save(), () => true);
            LoadCommand = new RelayCommand(() => Configurational.Load(), () => true);

            ExitCommand = new RelayCommand(() =>
            {
                if (exitMessageService.ShowMessage("Do you want to close the program?", "Exit"))
                {
                    Application.Current.Shutdown();
                }
            });
            InformationCommand = new RelayCommand(() =>
                informationMessageService.ShowMessage("(C)TUSUR, KSUB, Pchelintsev Andrew" +
                    " Alexandrovich, group 571-2, 2023.", "About program"));

            DepartmentsCommand = new RelayCommand(() => departmentsAction());
            PassportsCommand = new RelayCommand(() => passportsAction());
            PositionsCommand = new RelayCommand(() => positionsAction());
            GradeModesCommand = new RelayCommand(() => gradeModesAction());
            ScholarshipsCommand = new RelayCommand(() => scholarshipsAction());
            RolesCommand = new RelayCommand(() => rolesAction());

            DisciplinesCommand = new RelayCommand(() => disciplinesAction());
            GradesCommand = new RelayCommand(() => gradesAction());
            GradeStatementsCommand = new RelayCommand(() => gradeStatementsAction());
            PersonsCommand = new RelayCommand(() => personsAction());
            SpecialtiesCommand = new RelayCommand(() => specialtiesAction());
            StudentsCommand = new RelayCommand(() => studentsAction());
            GroupsCommand = new RelayCommand(() => groupsAction());
            StudyFormsCommand = new RelayCommand(() => studyFormsAction());
            TeachersCommand = new RelayCommand(() => teachersAction());

            StudentDisciplineConnectionsCommand = new RelayCommand(() =>
                studentDisciplineConnectionsAction());
            TeacherDisciplineConnectionsCommand = new RelayCommand(() =>
                teacherDisciplineConnectionsAction());

            RequestsCommand = new RelayCommand(() => requestsAction());
            ReportsCommand = new RelayCommand(() => reportsAction());

            LoadCommand.Execute(null);
            DataBaseContext.DataBaseConnection = Configurational.DataBasePath;
        }
    }
}