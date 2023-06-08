using System.Windows.Documents;

namespace SQLiteWpfApp.ViewModels.Services
{
    public interface IPrintDialogService
    {
        void Print(DocumentPaginator documentPaginator);
    }
}