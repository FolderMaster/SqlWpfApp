using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;
using Microsoft.EntityFrameworkCore;

using SQLiteWpfApp.Models.Independent;
using SQLiteWpfApp.ViewModels;
using SQLiteWpfApp.ViewModels.VMs;
using SQLiteWpfApp.Views.MessageBoxes;

namespace SQLiteWpfApp.Views.Windows.DataBase.Independent
{
    /// <summary>
    /// Interaction logic for DepartmentsWindow.xaml
    /// </summary>
    public partial class DepartmentsWindow : Window
    {
        private static DepartmentsWindow? _instance = null;

        private static DataBaseConnectionService<Department> _actionService =
            new DataBaseConnectionService<Department>((dbContext, dbSet) =>
            {
                _dbContext = dbContext;
                _dbSet = dbSet;
                var instance = Instance;
                instance.Show();
            });

        private static DataBaseContext _dbContext = null!;

        private static DbSet<Department> _dbSet = null!;

        public static DepartmentsWindow Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new DepartmentsWindow(_dbContext, _dbSet);
                }
                return _instance;
            }
        }

        public static DataBaseConnectionService<Department> ActionService => _actionService;

        private DepartmentsWindow(DataBaseContext dbContext, DbSet<Department> dbSet)
        {
            InitializeComponent();

            DataContext = new DataBaseVM<Department>(new ErrorMessageBoxService(), dbContext,
                dbSet);
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            _instance = null;
        }
    }
}