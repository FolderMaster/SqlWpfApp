using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Windows;
using View.Controls.Request;
using View.Windows;
using View.Windows.DbSet.Dependent;
using View.Windows.DbSet.Independent;
using ViewModel.Dependencies;
using ViewModel.Interfaces;

namespace View
{
    public partial class App : Application
    {
        private readonly IHost _host;

        public App()
        {
            _host = Host.CreateDefaultBuilder().ConfigureServices((services) =>
            {
                services.AddSingleton<MainWindow>();
                services.AddSingleton<IDataBaseContextCreator, SQLiteDataBaseContextCreator>();
            }).Build();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            _host.Start();

            var dataBaseContextCreator =
                _host.Services.GetRequiredService<IDataBaseContextCreator>();

            ReportsWindow.DataBaseContextCreator = dataBaseContextCreator;

            DepartmentsWindow.DataBaseContextCreator = dataBaseContextCreator;
            GradeModesWindow.DataBaseContextCreator = dataBaseContextCreator;
            PassportsWindow.DataBaseContextCreator = dataBaseContextCreator;
            PositionsWindow.DataBaseContextCreator = dataBaseContextCreator;
            RolesWindow.DataBaseContextCreator = dataBaseContextCreator;
            ScholarshipsWindow.DataBaseContextCreator = dataBaseContextCreator;
            GradeModesWindow.DataBaseContextCreator = dataBaseContextCreator;

            DisciplinesWindow.DataBaseContextCreator = dataBaseContextCreator;
            GradeStatementsWindow.DataBaseContextCreator = dataBaseContextCreator;
            GradesWindow.DataBaseContextCreator = dataBaseContextCreator;
            GroupsWindow.DataBaseContextCreator = dataBaseContextCreator;
            PersonsWindow.DataBaseContextCreator = dataBaseContextCreator;
            SpecialtiesWindow.DataBaseContextCreator = dataBaseContextCreator;
            StudentDisciplineConnectionsWindow.DataBaseContextCreator = dataBaseContextCreator;
            StudentsWindow.DataBaseContextCreator = dataBaseContextCreator;
            StudyFormsWindow.DataBaseContextCreator = dataBaseContextCreator;
            TeachersWindow.DataBaseContextCreator = dataBaseContextCreator;
            TeacherDisciplineConnectionsWindow.DataBaseContextCreator = dataBaseContextCreator;

            ChangeDataRequestsControl.DataBaseContextCreator = dataBaseContextCreator;
            SpecialChangeDataRequestsControl.DataBaseContextCreator = dataBaseContextCreator;
            SpecialViewDataRequestsControl.DataBaseContextCreator = dataBaseContextCreator;
            ViewDataRequestsControl.DataBaseContextCreator = dataBaseContextCreator;

            MainWindow = _host.Services.GetRequiredService<MainWindow>();
            MainWindow.Show();
        }

        protected override void OnExit(ExitEventArgs e)
        {
            _host.StopAsync();
            _host.Dispose();

            base.OnExit(e);
        }
    }
}