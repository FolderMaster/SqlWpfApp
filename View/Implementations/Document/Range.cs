using System.Windows.Documents;

using ViewModel.Interfaces.Services.Document;

namespace View.Implementations.Document
{
    public class Range : IRange
    {
        public object Start { get; private set; }

        public object End { get; private set; }

        public Range(TextPointer start, TextPointer end)
        {
            Start = start;
            End = end;
        }
    }
}
