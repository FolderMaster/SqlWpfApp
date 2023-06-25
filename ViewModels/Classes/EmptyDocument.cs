using ViewModel.Services;

namespace ViewModel.Classes
{
    public class EmptyDocument : IDocument
    {
        public object? Source { get => null; set => throw new NotImplementedException(); }

        public object? DocumentPaginator => null;
    }
}