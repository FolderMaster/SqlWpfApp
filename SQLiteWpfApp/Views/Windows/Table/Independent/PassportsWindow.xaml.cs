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
    /// Interaction logic for PassportWindow.xaml
    /// </summary>
    public partial class PassportsWindow : Window
    {
        private static PassportsWindow? _instance = null;

        private static DbConnectionService<Passport> _actionService =
            new DbConnectionService<Passport>((dbContext, dbSet) =>
            {
                _dbContext = dbContext;
                _dbSet = dbSet;
                PassportsWindow instance = Instance;
                instance.Show();
            });

        private static DbContext _dbContext = null!;

        private static DbSet<Passport> _dbSet = null!;

        public static PassportsWindow Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new PassportsWindow(_dbContext, _dbSet);
                }
                return _instance;
            }
        }

        public static DbConnectionService<Passport> ActionService => _actionService;

        private PassportsWindow(DbContext dbContext, DbSet<Passport> dbSet)
        {
            InitializeComponent();

            DataContext = new DataBaseVM<Passport>(new ErrorMessageBoxService(), dbContext, dbSet);
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            _instance = null;
        }
    }
}