using System;
using System.Windows;
using System.Windows.Input;

namespace SQLiteWpfApp.ViewModels.Commands
{
    class AboutProgramCommand : ICommand
    {
        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter) => true;

        public void Execute(object? parameter) => MessageBox.Show("(C)TUSUR, KSUB, Pchelintsev " +
            "Andrew Alexandrovich, group 571-2, 2023.", "About program", MessageBoxButton.OK,
            MessageBoxImage.Information);
    }
}