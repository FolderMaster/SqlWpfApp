namespace ViewModel.Interfaces.Services.Document
{
    public interface IDocumentService
    {
        public ISelection GetSelection(object selection);

        public object GetDocumentPaginator(object document);
    }
}
