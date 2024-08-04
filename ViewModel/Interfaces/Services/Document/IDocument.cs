namespace ViewModel.Interfaces.Services.Document
{
    public interface IDocument
    {
        public double PageWidth { get; set; }

        public double PageHeight { get; set; }

        public object DocumentPaginator { get; }

        public void Clear();

        public void InsertValue(object value);
    }
}
