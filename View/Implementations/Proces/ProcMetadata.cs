namespace View.Implementations.Proces
{
    public class ProcMetadata
    {
        public string Title { get; set; }

        public string Path { get; set; }

        public object Icon { get; set; }

        public object Input { get; set; }

        public string InputText { get; set; }

        public ProcMetadata(string title, string path,
            object icon, object gesture, string gestureText)
        {
            Title = title;
            Path = path;
            Icon = icon;
            Input = gesture;
            InputText = gestureText;
        }
    }
}
