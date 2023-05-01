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
    /// Interaction logic for PositionsWindow.xaml
    /// </summary>
    public partial class PositionsWindow : Window
    {
        private static PositionsWindow? _instance = null;

        private static DbConnectionService<Position> _actionService =
            new DbConnectionService<Position>((dbContext, dbSet) =>
            {
                _dbContext = dbContext;
                _dbSet = dbSet;
                var instance = Instance;
                instance.Show();
            });

        private static DbContext _dbContext = null!;

        private static DbSet<Position> _dbSet = null!;

        public static PositionsWindow Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new PositionsWindow(_dbContext, _dbSet);
                }
                return _instance;
            }
        }

        public static DbConnectionService<Position> ActionService => _actionService;

        private PositionsWindow(DbContext dbContext, DbSet<Position> dbSet) : base()
        {
            InitializeComponent();

            DataContext = new DataBaseVM<Position>(new ErrorMessageBoxService(), dbContext, dbSet);
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            _instance = null;
        }
    }
}