namespace ViewModel.Interfaces.Services.Document
{
    public interface IRange
    {
        public string Text { get; }

        public bool IsHighlighted { get; set; }
    }
}
