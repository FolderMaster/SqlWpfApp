using System.Windows.Controls;
using System.Windows.Documents;

using SQLiteWpfApp.ViewModels.Services;

namespace SQLiteWpfApp.Views.PrintDialogs
{
    public class PrintDialogService : IPrintDialogService
    {
        private PrintDialog _dialog;

        private string _description;

        public void Print(DocumentPaginator documentPaginator)
        {
            if(_dialog.ShowDialog() == true)
            {
                _dialog.PrintDocument(documentPaginator, _description);
            }
        }

        public PrintDialogService(string description)
        {
            _dialog = new PrintDialog();
            _description = description;
        }
    }
}