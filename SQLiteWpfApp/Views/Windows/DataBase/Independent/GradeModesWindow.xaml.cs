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
    /// Interaction logic for GradeModesWindow.xaml
    /// </summary>
    public partial class GradeModesWindow : Window
    {
        private static GradeModesWindow? _instance = null;

        private static DataBaseConnectionService<GradeMode> _actionService =
            new DataBaseConnectionService<GradeMode>((dbContext, dbSet) =>
            {
                _dbContext = dbContext;
                _dbSet = dbSet;
                GradeModesWindow instance = Instance;
                instance.Show();
            });

        private static DataBaseContext _dbContext = null!;

        private static DbSet<GradeMode> _dbSet = null!;

        public static GradeModesWindow Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new GradeModesWindow(_dbContext, _dbSet);
                }
                return _instance;
            }
        }

        public static DataBaseConnectionService<GradeMode> ActionService => _actionService;

        private GradeModesWindow(DataBaseContext dbContext, DbSet<GradeMode> dbSet)
        {
            InitializeComponent();

            DataContext = new DataBaseVM<GradeMode>(new ErrorMessageBoxService(), dbContext,
                dbSet);
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            _instance = null;
        }
    }
}