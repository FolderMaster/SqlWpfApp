namespace ViewModel.Interfaces.Services.Document
{
    public interface ISelection
    {
        public bool? Bold { get; set; }

        public bool? Italic { get; set; }

        public double? FontSize { get; set; }

        public object? FontFamily { get; set; }

        public object? Alignment { get; set; }

        public void CreateList(object markerStyle);

        public void CreateTable(int columnsCount, int rowsCount);

        public void CreateImage(object data);
    }
}
