using System.Collections;

namespace ViewModel.Interfaces.Services.Document
{
    public interface IDocumentService
    {
        public IEnumerable FontFamilies { get; }

        public IEnumerable MarkerStyles { get; }

        public IEnumerable HorizontalTextAlignments { get; }

        public ISelection GetSelection(object selection);

        public IDocument GetDocument(object document);
    }
}
