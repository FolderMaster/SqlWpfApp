using System;
using System.Windows;
using System.Windows.Input;

namespace SQLiteWpfApp.ViewModels.Commands
{
    public class ExitCommand : ICommand
    {
        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter) => true;

        public void Execute(object? parameter)
        {
            if(MessageBox.Show("Do you want to close the program?", "Exit", MessageBoxButton.YesNo,
                MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                Application.Current.Shutdown();
            }
        }
    }
}