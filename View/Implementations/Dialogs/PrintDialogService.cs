using System.Windows.Controls;
using System.Windows.Documents;

using ViewModel.Interfaces;

namespace View.Implementations.Dialogs
{
    public class PrintDialogService : IPrintService
    {
        public void Print(object document, string description)
        {
            var dialog = new PrintDialog();
            if (dialog.ShowDialog() == true)
            {
                dialog.PrintDocument(document as DocumentPaginator, description);
            }
        }
    }
}