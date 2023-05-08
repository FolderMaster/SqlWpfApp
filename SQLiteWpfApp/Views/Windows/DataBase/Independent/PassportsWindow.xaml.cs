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
using System.IO;

using Microsoft.Win32;
using System.Security.Cryptography.Xml;
using DataObject = System.Windows.DataObject;

namespace SQLiteWpfApp.Views.Windows.DataBase.Independent
{
    /// <summary>
    /// Interaction logic for PassportWindow.xaml
    /// </summary>
    public partial class PassportsWindow : Window
    {
        private static PassportsWindow? _instance = null;

        private static DataBaseConnectionService<Passport> _actionService =
            new DataBaseConnectionService<Passport>((dbContext, dbSet) =>
            {
                _dbContext = dbContext;
                _dbSet = dbSet;
                PassportsWindow instance = Instance;
                instance.Show();
            });

        private static DataBaseContext _dbContext = null!;

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

        public static DataBaseConnectionService<Passport> ActionService => _actionService;

        private PassportsWindow(DataBaseContext dbContext, DbSet<Passport> dbSet)
        {
            InitializeComponent();

            DataContext = new PassportsVM(new ErrorMessageBoxService(), dbContext, dbSet);
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            _instance = null;
        }
    }
}