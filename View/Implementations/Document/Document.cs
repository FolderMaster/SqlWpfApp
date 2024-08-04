using System.IO;
using System.Windows;
using System.Windows.Documents;

using ViewModel.Interfaces.Services.Document;

namespace View.Implementations.Document
{
    public class Document : IDocument
    {
        private readonly FlowDocument _flowDocument;

        public double PageWidth
        {
            get => _flowDocument.PageWidth;
            set => _flowDocument.PageWidth = value;
        }

        public double PageHeight
        {
            get => _flowDocument.PageHeight;
            set => _flowDocument.PageHeight = value;
        }

        public FlowDocument FlowDocument => _flowDocument;

        public object DocumentPaginator
        {
            get
            {
                var cloneDocument = CloneFlowDocument(_flowDocument);
                var source = (IDocumentPaginatorSource)cloneDocument;
                return source.DocumentPaginator;
            }
        }

        public Document(FlowDocument flowDocument) => _flowDocument = flowDocument;

        public void Clear() => _flowDocument.Blocks.Clear();

        public void InsertValue(object value) =>
            _flowDocument.Blocks.Add(new Paragraph(new Run(value.ToString())));

        private FlowDocument CloneFlowDocument(FlowDocument originalDocument)
        {
            var newDocument = new FlowDocument();
            var originalContent = new TextRange(originalDocument.ContentStart,
                originalDocument.ContentEnd);
            var newContent = new TextRange(newDocument.ContentStart, newDocument.ContentEnd);
            using (var stream = new MemoryStream())
            {
                originalContent.Save(stream, DataFormats.Xaml);
                stream.Seek(0, SeekOrigin.Begin);
                newContent.Load(stream, DataFormats.Xaml);
            }
            newDocument.PageWidth = PageWidth;
            newDocument.PageHeight = PageHeight;
            return newDocument;
        }
    }
}
