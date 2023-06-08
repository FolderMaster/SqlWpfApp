using System.Windows;

using SQLiteWpfApp.ViewModels.VMs;
using SQLiteWpfApp.Views.MessageBoxes;
using SQLiteWpfApp.Views.Windows.DbSet.Dependent;
using SQLiteWpfApp.Views.Windows.DbSet.Independent;

namespace SQLiteWpfApp.Views.Windows
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            DataContext = new MainVM(new WindowConfiguration(this),
                new QuestionMessageBoxService(), new InformationMessageBoxService(),
                DepartmentsWindow.Action, PassportsWindow.Action, PositionsWindow.ActionService,
                GradeModesWindow.Action, RolesWindow.Action, ScholarshipsWindow.ActionService,
                DisciplinesWindow.Action, GradesWindow.Action, GradeStatementsWindow.Action,
                PersonsWindow.Action, SpecialtiesWindow.Action, StudentsWindow.Action,
                GroupsWindow.Action, StudyFormsWindow.Action, TeachersWindow.Action,
                StudentDisciplineConnectionsWindow.Action,
                TeacherDisciplineConnectionsWindow.Action, RequestsWindow.Action,
                ReportsWindow.Action);
        }
    }
}