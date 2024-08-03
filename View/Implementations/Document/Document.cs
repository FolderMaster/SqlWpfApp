using System.Windows.Documents;

using ViewModel.Interfaces.Services.Document;

namespace View.Implementations.Document
{
    public class Document : IDocument
    {
        private readonly FlowDocument _flowDocument;

        public double Width
        {
            get => _flowDocument.PageWidth;
            set => _flowDocument.PageWidth = value;
        }

        public double Height
        {
            get => _flowDocument.PageHeight;
            set => _flowDocument.PageHeight = value;
        }

        public object DocumentPaginator => 
            ((IDocumentPaginatorSource)_flowDocument).DocumentPaginator;

        public Document(FlowDocument flowDocument) => _flowDocument = flowDocument;
    }
}
