using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

using SQLiteWpfApp.ViewModels;

namespace SQLiteWpfApp.Views.MessageBoxes
{
    class ErrorMessageBoxService : IMessageService
    {
        public bool ShowMessage(string message, string title) => MessageBox.Show(message, title,
            MessageBoxButton.OK, MessageBoxImage.Error) == MessageBoxResult.OK;
    }
}
