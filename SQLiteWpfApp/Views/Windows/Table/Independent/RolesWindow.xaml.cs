using Microsoft.EntityFrameworkCore;
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

using SQLiteWpfApp.Models.Independent;
using SQLiteWpfApp.ViewModels;
using SQLiteWpfApp.ViewModels.VMs;
using SQLiteWpfApp.Views.MessageBoxes;

namespace SQLiteWpfApp.Views.Windows.Table.Independent
{
    /// <summary>
    /// Interaction logic for RolesWindow.xaml
    /// </summary>
    public partial class RolesWindow : Window
    {
        private static RolesWindow? _instance = null;

        private static DbConnectionService<Role> _actionService = new DbConnectionService<Role>
            ((dbContext, dbSet) =>
            {
                _dbSet = dbSet;
                _dbContext = dbContext;
                var instance = Instance;
                instance.Show();
            });

        private static DbContext _dbContext = null!;

        private static DbSet<Role> _dbSet = null!;

        public static RolesWindow Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new RolesWindow(_dbContext, _dbSet);
                }
                return _instance;
            }
        }

        public static DbConnectionService<Role> ActionService => _actionService;

        private RolesWindow(DbContext dbContext, DbSet<Role> dataBase)
        {
            InitializeComponent();

            DataContext = new DataBaseVM<Role>(new ErrorMessageBoxService(), dbContext, dataBase);
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            _instance = null;
        }
    }
}