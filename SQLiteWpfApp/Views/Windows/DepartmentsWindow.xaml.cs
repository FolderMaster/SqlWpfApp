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

namespace SQLiteWpfApp.Views.Windows
{
    /// <summary>
    /// Interaction logic for DepartmentsWindow.xaml
    /// </summary>
    public partial class DepartmentsWindow : Window
    {
        private static DepartmentsWindow? _instance;

        public static DepartmentsWindow Instance
        {
            get
            {
                if(_instance == null)
                {
                    _instance = new DepartmentsWindow();
                    _instance.Closed += DepartmentsWindow_Closed;
                }
                return _instance;
            }
        }

        public DepartmentsWindow()
        {
            InitializeComponent();
        }

        private static void DepartmentsWindow_Closed(object? sender, EventArgs e)
        {
            _instance = null;
        }
    }
}
