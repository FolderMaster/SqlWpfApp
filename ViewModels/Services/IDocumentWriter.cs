using System.Data;

namespace ViewModel.Services
{
    public interface IDocumentWriter
    {
        public IDocument CreateDocument();

        public void WriteParagraph(IDocument document, string text);

        public void WriteTable(IDocument document, DataTable table);
    }
}