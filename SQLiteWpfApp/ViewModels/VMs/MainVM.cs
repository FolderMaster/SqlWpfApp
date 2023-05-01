using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

using SQLiteWpfApp.ViewModels;
using SQLiteWpfApp.Models.Independent;

namespace SQLiteWpfApp.ViewModels.VMs
{
    public class MainVM
    {
        private DataBaseContext _dataBaseContext;

        private IConfigurational _configurational;

        private ICommand _saveCommand;

        private ICommand _loadCommand;

        private ICommand _exitCommand;

        private ICommand _informationCommand;

        private ICommand _departmentsCommand;

        private ICommand _passportsCommand;

        private ICommand _positionsCommand;

        private ICommand _gradeModesCommand;

        private ICommand _rolesCommand;

        private ICommand _scholarshipsCommand;

        public DataBaseContext DataBaseContext => _dataBaseContext;

        public IConfigurational Configurational => _configurational;

        public ICommand SaveCommand => _saveCommand;

        public ICommand LoadCommand => _loadCommand;

        public ICommand ExitCommand => _exitCommand;

        public ICommand InformationCommand => _informationCommand;

        public ICommand DepartmentsCommand => _departmentsCommand;

        public ICommand PassportsCommand => _passportsCommand;

        public ICommand PositionsCommand => _positionsCommand;

        public ICommand GradeModesCommand => _gradeModesCommand;

        public ICommand RolesCommand => _rolesCommand;

        public ICommand ScholarshipsCommand => _scholarshipsCommand;

        public MainVM(IConfigurational configurational, IMessageService exitMessageService,
            IMessageService informationMessageService,
            DbConnectionService<Department> departmentsConnectionService,
            DbConnectionService<Passport> passportsConnectionService,
            DbConnectionService<Position> positionsConnectionService,
            DbConnectionService<GradeMode> gradeModesConnectionService,
            DbConnectionService<Role> rolesConnectionService,
            DbConnectionService<Scholarship> scholarshipsConnectionService)
        {
            _configurational = configurational;

            _saveCommand = new RelayCommand((parameter) =>
                Configurational.Save());
            _loadCommand = new RelayCommand((parameter) =>
                Configurational.Load());
            _exitCommand = new RelayCommand((parameter) =>
            {
                if (exitMessageService.ShowMessage("Do you want to close the program?", "Exit"))
                {
                    Application.Current.Shutdown();
                }
            });
            _informationCommand = new RelayCommand((parameter) =>
                informationMessageService.ShowMessage("(C)TUSUR, KSUB, Pchelintsev Andrew" +
                    " Alexandrovich, group 571-2, 2023.", "About program"));
            _departmentsCommand = new RelayCommand((parameter) =>
                departmentsConnectionService.ConnectDb(DataBaseContext,
                DataBaseContext.Departments));
            _passportsCommand = new RelayCommand((parameter) =>
                passportsConnectionService.ConnectDb(DataBaseContext, DataBaseContext.Passports));
            _positionsCommand = new RelayCommand((parameter) =>
                positionsConnectionService.ConnectDb(DataBaseContext, DataBaseContext.Positions));
            _gradeModesCommand = new RelayCommand((parameter) =>
                gradeModesConnectionService.ConnectDb(DataBaseContext,
                    DataBaseContext.GradeModes));
            _scholarshipsCommand = new RelayCommand((parameter) =>
                scholarshipsConnectionService.ConnectDb(DataBaseContext,
                    DataBaseContext.Scholarships));
            _rolesCommand = new RelayCommand((parameter) =>
                rolesConnectionService.ConnectDb(DataBaseContext, DataBaseContext.Roles));

            LoadCommand.Execute(null);

            _dataBaseContext = new DataBaseContext(Configurational.DataBasePath);
        }
    }
}