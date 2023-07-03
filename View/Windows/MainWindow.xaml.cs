using System.Windows;

using ViewModel.VMs;
using ViewModel.Interfaces;
using View.Implementations;

namespace View.Windows
{
    public partial class MainWindow : Window
    {
        public MainWindow(IDbContextCreator dbContextCreator, /*IConfigurational configurational,*/
            IMessageService questionMessageService, IMessageService informationMessageService,
            IMessageService errorMessageService, IAppCloseable appCloseable,
            IProc departmentsWindowProc, IProc passportsWindowProc, IProc positionsWindowProc,
            IProc gradeModesWindowProc, IProc rolesWindowProc, IProc scholarshipsWindowProc,
            IProc disciplinesWindowProc, IProc gradesWindowProc, IProc gradeStatementsWindowProc,
            IProc personsWindowProc, IProc specialtiesWindowProc, IProc studentsWindowProc,
            IProc groupsWindowProc, IProc studyFormsWindowProc, IProc teachersWindowProc,
            IProc studentDisciplineConnectionsWindowProc,
            IProc teacherDisciplineConnectionsWindowProc, IProc requestsWindowProc,
            IProc reportsWindowProc)
        {
            InitializeComponent();

            DataContext = new MainVM(dbContextCreator, /*configurational*/ new WindowConfiguration(this),
                questionMessageService, informationMessageService, errorMessageService,
                appCloseable, departmentsWindowProc, passportsWindowProc,
                positionsWindowProc, gradeModesWindowProc, rolesWindowProc,
                scholarshipsWindowProc, disciplinesWindowProc, gradesWindowProc,
                gradeStatementsWindowProc, personsWindowProc, specialtiesWindowProc,
                studentsWindowProc, groupsWindowProc, studyFormsWindowProc, teachersWindowProc,
                studentDisciplineConnectionsWindowProc, teacherDisciplineConnectionsWindowProc,
                requestsWindowProc, reportsWindowProc);
        }
    }
}