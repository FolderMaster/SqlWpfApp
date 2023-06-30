using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;

using ViewModel.Interfaces;

namespace ViewModel.VMs
{
    public class MainVM : ObservableObject
    {
        public IDataBaseContextCreator DataBaseContextCreator { get; private set; }

        public IConfigurational Configurational { get; private set; }

        public RelayCommand SaveCommand { get; private set; }

        public RelayCommand LoadCommand { get; private set; }

        public RelayCommand ExitCommand { get; private set; }

        public RelayCommand InformationCommand { get; private set; }

        public RelayCommand DepartmentsCallCommand { get; private set; }

        public RelayCommand PassportsCallCommand { get; private set; }

        public RelayCommand PositionsCallCommand { get; private set; }

        public RelayCommand GradeModesCallCommand { get; private set; }

        public RelayCommand RolesCallCommand { get; private set; }

        public RelayCommand ScholarshipsCallCommand { get; private set; }

        public RelayCommand DisciplinesCallCommand { get; private set; }

        public RelayCommand GradesCallCommand { get; private set; }

        public RelayCommand GradeStatementsCallCommand { get; private set; }

        public RelayCommand PersonsCallCommand { get; private set; }

        public RelayCommand SpecialtiesCallCommand { get; private set; }

        public RelayCommand StudentsCallCommand { get; private set; }

        public RelayCommand GroupsCallCommand { get; private set; }

        public RelayCommand StudyFormsCallCommand { get; private set; }

        public RelayCommand TeachersCallCommand { get; private set; }

        public RelayCommand StudentDisciplineConnectionsCallCommand { get; private set; }

        public RelayCommand TeacherDisciplineConnectionsCallCommand { get; private set; }

        public RelayCommand RequestsCallCommand { get; private set; }

        public RelayCommand ReportsCallCommand { get; private set; }

        public MainVM(IDataBaseContextCreator dataBaseContextCreator,
            IConfigurational configurational, IMessageService exitMessageService,
            IMessageService informationMessageService, IAppCloseable appCloseable,
            Action departmentsCall, Action passportsCall, Action positionsCall,
            Action gradeModesCall, Action rolesCall, Action scholarshipsCall,
            Action disciplinesCall, Action gradesCall, Action gradeStatementsCall,
            Action personsCall, Action specialtiesCall, Action studentsCall,
            Action groupsCall, Action studyFormsCall, Action teachersCall,
            Action studentDisciplineConnectionsCall, Action teacherDisciplineConnectionsCall,
            Action requestsCall, Action reportsCall)
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

            DepartmentsCallCommand = new RelayCommand(() => departmentsCall());
            PassportsCallCommand = new RelayCommand(() => passportsCall());
            PositionsCallCommand = new RelayCommand(() => positionsCall());
            GradeModesCallCommand = new RelayCommand(() => gradeModesCall());
            ScholarshipsCallCommand = new RelayCommand(() => scholarshipsCall());
            RolesCallCommand = new RelayCommand(() => rolesCall());

            DisciplinesCallCommand = new RelayCommand(() => disciplinesCall());
            GradesCallCommand = new RelayCommand(() => gradesCall());
            GradeStatementsCallCommand = new RelayCommand(() => gradeStatementsCall());
            PersonsCallCommand = new RelayCommand(() => personsCall());
            SpecialtiesCallCommand = new RelayCommand(() => specialtiesCall());
            StudentsCallCommand = new RelayCommand(() => studentsCall());
            GroupsCallCommand = new RelayCommand(() => groupsCall());
            StudyFormsCallCommand = new RelayCommand(() => studyFormsCall());
            TeachersCallCommand = new RelayCommand(() => teachersCall());

            StudentDisciplineConnectionsCallCommand = new RelayCommand(() =>
                studentDisciplineConnectionsCall());
            TeacherDisciplineConnectionsCallCommand = new RelayCommand(() =>
                teacherDisciplineConnectionsCall());

            RequestsCallCommand = new RelayCommand(() => requestsCall());
            ReportsCallCommand = new RelayCommand(() => reportsCall());

            LoadCommand.Execute(null);
            DataBaseContextCreator = dataBaseContextCreator;
            DataBaseContextCreator.Create(Configurational.DataBaseConnectionString);
        }
    }
}