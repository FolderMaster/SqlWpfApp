using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using System.Windows;

using View.Implementations;
using View.Implementations.Dialogs;
using View.Implementations.MessageBoxes;
using View.Implementations.Proces;
using View.Implementations.Proces.DbSet.Dependent;
using View.Implementations.Proces.DbSet.Independent;
using View.Implementations.ResourceService;
using View.Services;
using View.Windows;

using ViewModel.Dependencies;
using ViewModel.Interfaces;
using ViewModel.Interfaces.DbContext;
using ViewModel.Interfaces.Services;
using ViewModel.Interfaces.Services.Files;
using ViewModel.Interfaces.Services.Messages;

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
                services.AddSingleton<IFileService, FileService>();
                services.AddSingleton<IPathService, PathService>();

                services.AddSingleton<IDbContextBuilder, MsSqlServerDbContextBuilder>();

                services.AddSingleton<IPrintService, PrintDialogService>();

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

                services.AddSingleton<PassportsWindowProcCreator>();

                services.AddSingleton<DepartmentsWindowProc>();
                services.AddSingleton<GradeModesWindowProc>();
                services.AddSingleton<PassportsWindowProc>((s) =>
                    s.GetRequiredService<PassportsWindowProcCreator>().Create());
                services.AddSingleton<PositionsWindowProc>();
                services.AddSingleton<RolesWindowProc>();
                services.AddSingleton<ScholarshipsWindowProc>();

                services.AddSingleton<DisciplinesWindowProc>();
                services.AddSingleton<GradeStatementsWindowProc>();
                services.AddSingleton<GradesWindowProc>();
                services.AddSingleton<GroupsWindowProc>();
                services.AddSingleton<PersonsWindowProc>();
                services.AddSingleton<SpecialtiesWindowProc>();
                services.AddSingleton<StudentDisciplineConnectionsWindowProc>();
                services.AddSingleton<StudentsWindowProc>();
                services.AddSingleton<StudyFormsWindowProc>();
                services.AddSingleton<TeachersWindowProc>();
                services.AddSingleton<TeacherDisciplineConnectionsWindowProc>();

                services.AddSingleton<RequestsWindowProc>();
                services.AddSingleton<ReportsWindowProc>();

                services.AddSingleton<MainWindow>();

                services.AddSingleton<ConnectionWindowProc>();

                services.AddSingleton<IConfiguration, WindowConfiguration>((s) =>
                    new WindowConfiguration(s.GetRequiredService<MainWindow>()));

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

            var configurator = _host.Services.GetRequiredService<MainWindowConfigurator>();
            configurator.Configure();

            MainWindow = _host.Services.GetRequiredService<MainWindow>();
            MainWindow.Show();

            _host.Services.GetRequiredService<ConnectionWindowProc>().Invoke();
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