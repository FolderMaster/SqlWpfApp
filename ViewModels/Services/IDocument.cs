namespace ViewModel.Services
{
    public interface IDocument
    {
        public object? Source { get; set; }

        public object? DocumentPaginator { get; }
    }
}