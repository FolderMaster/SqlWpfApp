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

namespace SQLiteWpfApp.Views.Windows.DataBase.Independent
{
    /// <summary>
    /// Interaction logic for ScholarshipsWindow.xaml
    /// </summary>
    public partial class ScholarshipsWindow : Window
    {
        private static ScholarshipsWindow? _instance = null;

        private static DataBaseConnectionService<Scholarship> _actionService =
            new DataBaseConnectionService<Scholarship>((dbContext, dbSet) =>
            {
                _dbSet = dbSet;
                _dbContext = dbContext;
                var instance = Instance;
                instance.Show();
            });

        private static DataBaseContext _dbContext = null!;

        private static DbSet<Scholarship> _dbSet = null!;

        public static ScholarshipsWindow Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new ScholarshipsWindow(_dbContext, _dbSet);
                }
                return _instance;
            }
        }

        public static DataBaseConnectionService<Scholarship> ActionService => _actionService;

        private ScholarshipsWindow(DataBaseContext dbContext, DbSet<Scholarship> dbSet)
        {
            InitializeComponent();

            DataContext = new DataBaseVM<Scholarship>(new ErrorMessageBoxService(), dbContext, dbSet);
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            _instance = null;
        }
    }
}