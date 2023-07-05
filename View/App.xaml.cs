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

namespace View
{
    public partial class App : Application
    {
        private readonly IHost _host;

        public App()
        {
            _host = Host.CreateDefaultBuilder().ConfigureServices((services) =>
            {
                services.AddSingleton<IAppCloseable, AppCloseable>();
                services.AddSingleton<IFileService, FileService>();

                services.AddSingleton<IDbContextBuilder, SQLiteDbContextBuilder>();

                services.AddSingleton<IPrintService, PrintDialogService>();

                services.AddSingleton<ErrorMessageBoxService>();
                services.AddSingleton<IMessageService>(provider =>
                    provider.GetRequiredService<ErrorMessageBoxService>());
                services.AddSingleton<InformationMessageBoxService>();
                services.AddSingleton<QuestionMessageBoxService>();

                services.AddSingleton<IWindowResourceService, WindowResourceService>();
                services.AddSingleton<IResourceService>(provider =>
                    provider.GetRequiredService<IWindowResourceService>());

                services.AddSingleton<OpenFileDialogService>();
                services.AddSingleton<SaveFileDialogService>();

                services.AddSingleton<DepartmentsWindowProc>();
                services.AddSingleton<GradeModesWindowProc>();
                services.AddSingleton<PassportsWindowProc>((s) =>
                    new PassportsWindowProc(s.GetRequiredService<IDbContextBuilder>(),
                        s.GetRequiredService<IWindowResourceService>(),
                        s.GetRequiredService<ErrorMessageBoxService>(),
                        s.GetRequiredService<OpenFileDialogService>(),
                        s.GetRequiredService<SaveFileDialogService>(),
                        s.GetRequiredService<IFileService>()));
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

                services.AddSingleton<IConfiguration, WindowConfiguration>((s) =>
                    new WindowConfiguration(s.GetRequiredService<MainWindow>()));

                services.AddSingleton<MainWindowConfigurator>((s) => new MainWindowConfigurator()
                {
                    MainWindow = s.GetRequiredService<MainWindow>(),

                    DbContextCreator = s.GetRequiredService<IDbContextBuilder>(),
                    ResourceService = s.GetRequiredService<IResourceService>(),
                    Configurational = s.GetRequiredService<IConfiguration>(),
                    QuestionMessageService = s.GetRequiredService<QuestionMessageBoxService>(),
                    InformationMessageService =
                            s.GetRequiredService<InformationMessageBoxService>(),
                    ErrorMessageService = s.GetRequiredService<ErrorMessageBoxService>(),
                    AppCloseable = s.GetRequiredService<IAppCloseable>(),

                    DepartmentsProc = s.GetRequiredService<DepartmentsWindowProc>(),
                    PassportsProc = s.GetRequiredService<PassportsWindowProc>(),
                    PositionsProc = s.GetRequiredService<PositionsWindowProc>(),
                    GradeModesProc = s.GetRequiredService<GradeModesWindowProc>(),
                    RolesProc = s.GetRequiredService<RolesWindowProc>(),
                    ScholarshipsProc = s.GetRequiredService<ScholarshipsWindowProc>(),

                    DisciplinesProc = s.GetRequiredService<DisciplinesWindowProc>(),
                    GradesProc = s.GetRequiredService<GradesWindowProc>(),
                    GradeStatementsProc = s.GetRequiredService<GradeStatementsWindowProc>(),
                    PersonsProc = s.GetRequiredService<PersonsWindowProc>(),
                    SpecialtiesProc = s.GetRequiredService<SpecialtiesWindowProc>(),
                    StudentsProc = s.GetRequiredService<StudentsWindowProc>(),
                    GroupsProc = s.GetRequiredService<GroupsWindowProc>(),
                    StudyFormsProc = s.GetRequiredService<StudyFormsWindowProc>(),
                    TeachersProc = s.GetRequiredService<TeachersWindowProc>(),

                    StudentDisciplineConnectionsProc =
                            s.GetRequiredService<StudentDisciplineConnectionsWindowProc>(),
                    TeacherDisciplineConnectionsProc =
                            s.GetRequiredService<TeacherDisciplineConnectionsWindowProc>(),

                    RequestsProc = s.GetRequiredService<RequestsWindowProc>(),
                    ReportsProc = s.GetRequiredService<ReportsWindowProc>()
                });

            }).Build();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            _host.Start();

            var configurator = _host.Services.GetRequiredService<MainWindowConfigurator>();
            configurator.Confugure();

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