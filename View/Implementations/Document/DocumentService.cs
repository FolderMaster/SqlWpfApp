using System.Windows.Documents;

using ViewModel.Interfaces.Services.Document;

namespace View.Implementations.Document
{
    public class DocumentService : IDocumentService
    {
        public ISelection GetSelection(object selection) =>
            new Selection((TextSelection)selection);

        public object GetDocumentPaginator(object document)
        {
            var source = (IDocumentPaginatorSource)document;
            return source.DocumentPaginator;
        }
    }
}
