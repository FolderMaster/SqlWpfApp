namespace ViewModel.Interfaces.Services.Document
{
    public interface ISelection
    {
        public bool? Bold { get; set; }

        public bool? Italic { get; set; }

        public double? Size { get; set; }

        public object? Alignment { get; set; }

        public void CreateList();

        public void CreateTable(int columnsCount, int rowsCount);

        public void CreateImage(object data);
    }
}
