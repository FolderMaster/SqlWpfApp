using System.Windows.Documents;
using System.Windows.Media;

using ViewModel.Interfaces.Services.Document;

namespace View.Implementations.Document
{
    public class Range : IRange
    {
        private readonly Brush _highlightingBrush = Brushes.Yellow;

        private readonly TextRange _textRange;

        private bool _isHighlighted = false;

        public string Text
        {
            get => _textRange.Text;
            set => _textRange.Text = value;
        }

        public bool IsHighlighted
        {
            get => _isHighlighted;
            set
            {
                if (IsHighlighted != value)
                {
                    _isHighlighted = value;
                    ChangeHighlighting();
                }
            }
        }

        public TextPointer Start => _textRange.Start;

        public TextPointer End => _textRange.End;

        public Range(TextRange textRange) => _textRange = textRange;

        private void ChangeHighlighting()
        {
            if (IsHighlighted)
            {
                _textRange.ApplyPropertyValue(TextElement.BackgroundProperty, _highlightingBrush);
            }
            else
            {
                _textRange.ApplyPropertyValue(TextElement.BackgroundProperty, null);
            }
        }
    }
}
