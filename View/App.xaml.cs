using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using System.Windows;

using View.Implementations;
using View.Implementations.Dialogs;
using View.Implementations.MessageBoxes;
using View.Implementations.Proces;
using View.Implementations.Proces.DbSet.Dependent;
using View.Implementations.Proces.DbSet.Independent;
using View.Windows;

using ViewModel.Dependencies;
using ViewModel.Interfaces;

namespace View
{
    public partial class App : Application
    {
        private readonly IHost _host;

        private static string _openFileDialogFilterService =
            "Images (*.png;*.jpeg;*.jpg)|*.png;*.jpeg;*.jpg";

        private static string _saveFileDialogFilterService =
            "PNG image|*.png|JPEG image|*.jpeg|JPG image|*.jpg";

        public App()
        {
            _host = Host.CreateDefaultBuilder().ConfigureServices((services) =>
            {
                services.AddSingleton<IDbContextCreator, SQLiteDbContextCreator>();

                services.AddSingleton<IPrintDialogService, PrintDialogService>();

                services.AddSingleton<IAppCloseable, AppCloseable>();

                services.AddSingleton<ErrorMessageBoxService>();
                services.AddSingleton<InformationMessageBoxService>();
                services.AddSingleton<QuestionMessageBoxService>();

                services.AddSingleton<OpenFileDialogService>((s) =>
                    new OpenFileDialogService(_openFileDialogFilterService));
                services.AddSingleton<SaveFileDialogService>((s) =>
                    new SaveFileDialogService(_saveFileDialogFilterService));

                services.AddSingleton<DepartmentsWindowProc>((s) =>
                    new DepartmentsWindowProc(s.GetRequiredService<IDbContextCreator>(),
                        s.GetRequiredService<ErrorMessageBoxService>()));
                services.AddSingleton<GradeModesWindowProc>((s) =>
                    new GradeModesWindowProc(s.GetRequiredService<IDbContextCreator>(),
                        s.GetRequiredService<ErrorMessageBoxService>()));
                services.AddSingleton<PassportsWindowProc>((s) =>
                    new PassportsWindowProc(s.GetRequiredService<IDbContextCreator>(),
                        s.GetRequiredService<ErrorMessageBoxService>(),
                        s.GetRequiredService<OpenFileDialogService>(),
                        s.GetRequiredService<SaveFileDialogService>()));
                services.AddSingleton<PositionsWindowProc>((s) =>
                    new PositionsWindowProc(s.GetRequiredService<IDbContextCreator>(),
                        s.GetRequiredService<ErrorMessageBoxService>()));
                services.AddSingleton<RolesWindowProc>((s) =>
                    new RolesWindowProc(s.GetRequiredService<IDbContextCreator>(),
                        s.GetRequiredService<ErrorMessageBoxService>()));
                services.AddSingleton<ScholarshipsWindowProc>((s) =>
                    new ScholarshipsWindowProc(s.GetRequiredService<IDbContextCreator>(),
                        s.GetRequiredService<ErrorMessageBoxService>()));

                services.AddSingleton<DisciplinesWindowProc>((s) =>
                    new DisciplinesWindowProc(s.GetRequiredService<IDbContextCreator>(),
                        s.GetRequiredService<ErrorMessageBoxService>()));
                services.AddSingleton<GradeStatementsWindowProc>((s) =>
                    new GradeStatementsWindowProc(s.GetRequiredService<IDbContextCreator>(),
                        s.GetRequiredService<ErrorMessageBoxService>()));
                services.AddSingleton<GradesWindowProc>((s) =>
                    new GradesWindowProc(s.GetRequiredService<IDbContextCreator>(),
                        s.GetRequiredService<ErrorMessageBoxService>()));
                services.AddSingleton<GroupsWindowProc>((s) =>
                    new GroupsWindowProc(s.GetRequiredService<IDbContextCreator>(),
                        s.GetRequiredService<ErrorMessageBoxService>()));
                services.AddSingleton<PersonsWindowProc>((s) =>
                    new PersonsWindowProc(s.GetRequiredService<IDbContextCreator>(),
                        s.GetRequiredService<ErrorMessageBoxService>()));
                services.AddSingleton<SpecialtiesWindowProc>((s) =>
                    new SpecialtiesWindowProc(s.GetRequiredService<IDbContextCreator>(),
                        s.GetRequiredService<ErrorMessageBoxService>()));
                services.AddSingleton<StudentDisciplineConnectionsWindowProc>((s) =>
                    new StudentDisciplineConnectionsWindowProc
                        (s.GetRequiredService<IDbContextCreator>(),
                        s.GetRequiredService<ErrorMessageBoxService>()));
                services.AddSingleton<StudentsWindowProc>((s) =>
                    new StudentsWindowProc(s.GetRequiredService<IDbContextCreator>(),
                        s.GetRequiredService<ErrorMessageBoxService>()));
                services.AddSingleton<StudyFormsWindowProc>((s) =>
                    new StudyFormsWindowProc(s.GetRequiredService<IDbContextCreator>(),
                        s.GetRequiredService<ErrorMessageBoxService>()));
                services.AddSingleton<TeachersWindowProc>((s) =>
                    new TeachersWindowProc(s.GetRequiredService<IDbContextCreator>(),
                        s.GetRequiredService<ErrorMessageBoxService>()));
                services.AddSingleton<TeacherDisciplineConnectionsWindowProc>((s) =>
                    new TeacherDisciplineConnectionsWindowProc
                        (s.GetRequiredService<IDbContextCreator>(),
                        s.GetRequiredService<ErrorMessageBoxService>()));

                services.AddSingleton<RequestsWindowProc>((s) =>
                    new RequestsWindowProc(s.GetRequiredService<IDbContextCreator>(),
                        s.GetRequiredService<ErrorMessageBoxService>()));
                services.AddSingleton<ReportsWindowProc>((s) =>
                    new ReportsWindowProc(s.GetRequiredService<IDbContextCreator>(),
                        s.GetRequiredService<ErrorMessageBoxService>(),
                        s.GetRequiredService<IPrintDialogService>()));

                services.AddSingleton<IConfigurational, WindowConfiguration>((s) =>
                    new WindowConfiguration(s.GetRequiredService<MainWindow>()));

                services.AddSingleton<MainWindow>((s) =>
                    new MainWindow(s.GetRequiredService<IDbContextCreator>(),
                    //s.GetRequiredService<IConfigurational>(),
                    s.GetRequiredService<QuestionMessageBoxService>(),
                    s.GetRequiredService<InformationMessageBoxService>(),
                    s.GetRequiredService<ErrorMessageBoxService>(),
                    s.GetRequiredService<IAppCloseable>(),
                    s.GetRequiredService<DepartmentsWindowProc>(),
                    s.GetRequiredService<PassportsWindowProc>(),
                    s.GetRequiredService<PositionsWindowProc>(),
                    s.GetRequiredService<GradeModesWindowProc>(),
                    s.GetRequiredService<RolesWindowProc>(),
                    s.GetRequiredService<ScholarshipsWindowProc>(),
                    s.GetRequiredService<DisciplinesWindowProc>(),
                    s.GetRequiredService<GradesWindowProc>(),
                    s.GetRequiredService<GradeStatementsWindowProc>(),
                    s.GetRequiredService<PersonsWindowProc>(),
                    s.GetRequiredService<SpecialtiesWindowProc>(),
                    s.GetRequiredService<StudentsWindowProc>(),
                    s.GetRequiredService<GroupsWindowProc>(),
                    s.GetRequiredService<StudyFormsWindowProc>(),
                    s.GetRequiredService<TeachersWindowProc>(),
                    s.GetRequiredService<StudentDisciplineConnectionsWindowProc>(),
                    s.GetRequiredService<TeacherDisciplineConnectionsWindowProc>(),
                    s.GetRequiredService<RequestsWindowProc>(),
                    s.GetRequiredService<ReportsWindowProc>()));
            }).Build();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            _host.Start();

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