using System.Windows;

using View.Windows.DbSet.Dependent;
using View.Windows.DbSet.Independent;
using View.Implementations;
using View.Implementations.MessageBoxes;

using ViewModel.VMs;
using ViewModel.Interfaces;

namespace View.Windows
{
    public partial class MainWindow : Window
    {
        public MainWindow(IDataBaseContextCreator dataBaseContextCreator)
        {
            InitializeComponent();

            DataContext = new MainVM(dataBaseContextCreator, new WindowConfiguration(this),
                new QuestionMessageBoxService(), new InformationMessageBoxService(),
                new AppCloseable(), DepartmentsWindow.Call, PassportsWindow.Call,
                PositionsWindow.Call, GradeModesWindow.Call, RolesWindow.Call,
                ScholarshipsWindow.Call, DisciplinesWindow.Call, GradesWindow.Call,
                GradeStatementsWindow.Call, PersonsWindow.Call, SpecialtiesWindow.Call,
                StudentsWindow.Call, GroupsWindow.Call, StudyFormsWindow.Call, TeachersWindow.Call,
                StudentDisciplineConnectionsWindow.Call, TeacherDisciplineConnectionsWindow.Call,
                RequestsWindow.Call, ReportsWindow.Call);
        }
    }
}