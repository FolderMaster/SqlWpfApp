using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

using SQLiteWpfApp.Services;

namespace SQLiteWpfApp.ViewModels
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
            IMessageService informationMessageService, Action departmentsAction,
            Action passportsAction, Action positionsAction, Action gradeModesAction,
            Action rolesAction, Action scholarshipsAction)
        {
            _configurational = configurational;


            _saveCommand = new RelayCommand((object? parameter) =>
                Configurational.Save());
            _loadCommand = new RelayCommand((object? parameter) =>
                Configurational.Load());
            _exitCommand = new RelayCommand((object? parameter) =>
            {
                if (exitMessageService.ShowMessage("Do you want to close the program?", "Exit"))
                {
                    Application.Current.Shutdown();
                }
            });
            _informationCommand = new RelayCommand((object? parameter) =>
                informationMessageService.ShowMessage("(C)TUSUR, KSUB, Pchelintsev Andrew" +
                    " Alexandrovich, group 571-2, 2023.", "About program"));
            _departmentsCommand = new RelayCommand((parameter) => departmentsAction());
            _passportsCommand = new RelayCommand((parameter) => passportsAction());
            _positionsCommand = new RelayCommand((parameter) => positionsAction());
            _gradeModesCommand = new RelayCommand((parameter) => gradeModesAction());
            _scholarshipsCommand = new RelayCommand((parameter) => scholarshipsAction());
            _rolesCommand = new RelayCommand((parameter) => rolesAction());

            LoadCommand.Execute(null);
            _dataBaseContext = new DataBaseContext(Configurational.DataBasePath);
            _dataBaseContext.Database.EnsureCreated();
        }
    }
}