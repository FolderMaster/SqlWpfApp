using System.Windows;

using ViewModel.VMs;
using View.MessageBoxes;
using View.Windows.DbSet.Dependent;
using View.Windows.DbSet.Independent;
using View.Configurations;

namespace View.Windows
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