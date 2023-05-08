using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

using SQLiteWpfApp.Models.Independent;

namespace SQLiteWpfApp.ViewModels.VMs
{
    public class MainVM
    {
        public DataBaseContext DataBaseContext { get; private set; }

        public IConfigurational Configurational { get; private set; }

        public ICommand SaveCommand { get; private set; }

        public ICommand LoadCommand { get; private set; }

        public ICommand ExitCommand { get; private set; }

        public ICommand InformationCommand { get; private set; }

        public ICommand DepartmentsCommand { get; private set; }

        public ICommand PassportsCommand { get; private set; }

        public ICommand PositionsCommand { get; private set; }

        public ICommand GradeModesCommand { get; private set; }

        public ICommand RolesCommand { get; private set; }

        public ICommand ScholarshipsCommand { get; private set; }

        public MainVM(IConfigurational configurational, IMessageService exitMessageService,
            IMessageService informationMessageService,
            DataBaseConnectionService<Department> departmentsConnectionService,
            DataBaseConnectionService<Passport> passportsConnectionService,
            DataBaseConnectionService<Position> positionsConnectionService,
            DataBaseConnectionService<GradeMode> gradeModesConnectionService,
            DataBaseConnectionService<Role> rolesConnectionService,
            DataBaseConnectionService<Scholarship> scholarshipsConnectionService)
        {
            Configurational = configurational;

            SaveCommand = new RelayCommand((parameter) =>
                Configurational.Save());
            LoadCommand = new RelayCommand((parameter) =>
                Configurational.Load());
            ExitCommand = new RelayCommand((parameter) =>
            {
                if (exitMessageService.ShowMessage("Do you want to close the program?", "Exit"))
                {
                    Application.Current.Shutdown();
                }
            });
            InformationCommand = new RelayCommand((parameter) =>
                informationMessageService.ShowMessage("(C)TUSUR, KSUB, Pchelintsev Andrew" +
                    " Alexandrovich, group 571-2, 2023.", "About program"));
            DepartmentsCommand = new RelayCommand((parameter) =>
                departmentsConnectionService.ConnectDb(DataBaseContext,
                DataBaseContext.Departments));
            PassportsCommand = new RelayCommand((parameter) =>
                passportsConnectionService.ConnectDb(DataBaseContext, DataBaseContext.Passports));
            PositionsCommand = new RelayCommand((parameter) =>
                positionsConnectionService.ConnectDb(DataBaseContext, DataBaseContext.Positions));
            GradeModesCommand = new RelayCommand((parameter) =>
                gradeModesConnectionService.ConnectDb(DataBaseContext,
                    DataBaseContext.GradeModes));
            ScholarshipsCommand = new RelayCommand((parameter) =>
                scholarshipsConnectionService.ConnectDb(DataBaseContext,
                    DataBaseContext.Scholarships));
            RolesCommand = new RelayCommand((parameter) =>
                rolesConnectionService.ConnectDb(DataBaseContext, DataBaseContext.Roles));

            LoadCommand.Execute(null);
            DataBaseContext = new DataBaseContext(Configurational.DataBasePath);
        }
    }
}