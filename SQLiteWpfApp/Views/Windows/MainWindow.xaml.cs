using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using SQLiteWpfApp.ViewModels.VMs;
using SQLiteWpfApp.Views.MessageBoxes;
using SQLiteWpfApp.Views.Windows.DataBase.Independent;

namespace SQLiteWpfApp.Views.Windows
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            DataContext = new MainVM(new WindowConfiguration(this),
                new QuestionMessageBoxService(), new InformationMessageBoxService(),
                DepartmentsWindow.ActionService, PassportsWindow.ActionService,
                PositionsWindow.ActionService, GradeModesWindow.ActionService,
                RolesWindow.ActionService, ScholarshipsWindow.ActionService);
        }
    }
}