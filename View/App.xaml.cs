using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using System.Windows;

using View.Implementations;
using View.Implementations.Configuration;
using View.Implementations.Dialogs;
using View.Implementations.Document;
using View.Implementations.MessageBoxes;
using View.Implementations.Proces.MessageBoxes;
using View.Implementations.Proces.Windows;
using View.Implementations.Proces.Windows.DbSet.Dependent;
using View.Implementations.Proces.Windows.DbSet.Independent;
using View.Implementations.ResourceService;
using View.Services;
using View.Windows;

using ViewModel.Dependencies;
using ViewModel.Dependencies.Data;
using ViewModel.Dependencies.DataBase.MsSqlServer;
using ViewModel.Dependencies.DataBase.Sqlite;
using ViewModel.Interfaces;
using ViewModel.Interfaces.DataBase;
using ViewModel.Interfaces.Proces;
using ViewModel.Interfaces.Services;
using ViewModel.Interfaces.Services.Data;
using ViewModel.Interfaces.Services.Document;
using ViewModel.Interfaces.Services.Files;

namespace View
{
    /// <summary>
    /// Класс приложения.
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// Хост.
        /// </summary>
        private readonly IHost _host;

        /// <summary>
        /// Создаёт экземпляр класса <see cref="App"/> по умолчанию.
        /// </summary>
        public App()
        {
            _host = Host.CreateDefaultBuilder().ConfigureServices((services) =>
            {
                services.AddSingleton<IAppCloseable, AppCloseable>();

                services.AddSingleton<ErrorMessageBoxService>();
                services.AddSingleton<IMessageService>((s) =>
                    s.GetRequiredService<ErrorMessageBoxService>());
                services.AddSingleton<InformationMessageBoxService>();
                services.AddSingleton<QuestionMessageBoxService>();

                services.AddSingleton<IWindowResourceService, WindowResourceService>();
                services.AddSingleton<IResourceService>((s) =>
                    s.GetRequiredService<IWindowResourceService>());

                services.AddSingleton<OpenFileDialogService>();
                services.AddSingleton<SaveFileDialogService>();
                services.AddSingleton<IFileService, FileService>();

                services.AddSingleton<ISession, Session>();

                services.AddSingleton<IDbConnection, MsSqlServerDbConnection>();
                services.AddSingleton<IDbConnection, SqliteDbConnection>();

                services.AddSingleton<IPrintService, PrintDialogService>();
                services.AddSingleton<IDocumentService, DocumentService>();

                services.AddSingleton<IEncryptionService, AesEncryptionService>();
                services.AddSingleton<ISerializer, JsonSerializer>();

                services.AddSingleton<IProc, ExitMessageBoxProc>();
                services.AddSingleton<IProc, InformationMessageBoxProc>();

                services.AddSingleton<IProc, ConnectionWindowProc>();

                services.AddSingleton<IProc, DepartmentsWindowProc>();
                services.AddSingleton<IProc, GradeModesWindowProc>();
                services.AddSingleton<IProc, PassportsWindowProc>();
                services.AddSingleton<IProc, PositionsWindowProc>();
                services.AddSingleton<IProc, RolesWindowProc>();
                services.AddSingleton<IProc, ScholarshipsWindowProc>();

                services.AddSingleton<IProc, DisciplinesWindowProc>();
                services.AddSingleton<IProc, GradeStatementsWindowProc>();
                services.AddSingleton<IProc, GradesWindowProc>();
                services.AddSingleton<IProc, GroupsWindowProc>();
                services.AddSingleton<IProc, PersonsWindowProc>();
                services.AddSingleton<IProc, SpecialtiesWindowProc>();
                services.AddSingleton<IProc, StudentDisciplineConnectionsWindowProc>();
                services.AddSingleton<IProc, StudentsWindowProc>();
                services.AddSingleton<IProc, StudyFormsWindowProc>();
                services.AddSingleton<IProc, TeachersWindowProc>();
                services.AddSingleton<IProc, TeacherDisciplineConnectionsWindowProc>();

                services.AddSingleton<IProc, RequestsWindowProc>();
                services.AddSingleton<IProc, ReportsWindowProc>();

                services.AddSingleton<MainWindow>();

                services.AddSingleton<IConfiguration, WindowConfiguration>();

                services.AddSingleton<MainWindowConfigurator>();
            }).Build();
        }

        /// <summary>
        /// Метод, выполняющийся при запуске.
        /// </summary>
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            _host.Start();

            MainWindow = _host.Services.GetRequiredService<MainWindow>();

            var configurator = _host.Services.GetRequiredService<MainWindowConfigurator>();
            configurator.Configure();

            MainWindow.Show();
        }

        /// <summary>
        /// Метод, выполняющийся при выходе.
        /// </summary>
        protected override void OnExit(ExitEventArgs e)
        {
            _host.StopAsync();
            _host.Dispose();

            base.OnExit(e);
        }
    }
}