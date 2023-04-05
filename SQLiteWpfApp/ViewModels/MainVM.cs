using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

using SQLiteWpfApp.ViewModels.Commands;

namespace SQLiteWpfApp.ViewModels
{
    public class MainVM
    {
        private ICommand _exitCommand = new ExitCommand();

        private ICommand _aboutProgramCommand = new AboutProgramCommand();

        public ICommand ExitCommand => _exitCommand;

        public ICommand AboutProgramCommand => _aboutProgramCommand;
    }
}