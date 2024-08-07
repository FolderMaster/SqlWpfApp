using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Documents;

using ViewModel.Interfaces.Services.Document;

namespace View.Implementations.Document
{
    public class Document : IDocument
    {
        private readonly FlowDocument _flowDocument;

        public double PageWidth
        {
            get => _flowDocument.PageWidth;
            set => _flowDocument.PageWidth = value;
        }

        public double PageHeight
        {
            get => _flowDocument.PageHeight;
            set => _flowDocument.PageHeight = value;
        }

        public FlowDocument FlowDocument => _flowDocument;

        public object DocumentPaginator
        {
            get
            {
                var cloneDocument = CloneFlowDocument(_flowDocument);
                var source = (IDocumentPaginatorSource)cloneDocument;
                return source.DocumentPaginator;
            }
        }

        public object EndRange =>
            new TextRange(_flowDocument.ContentEnd, _flowDocument.ContentEnd);

        public object Range =>
            new TextRange(_flowDocument.ContentStart, _flowDocument.ContentEnd);

        public Document(FlowDocument flowDocument) => _flowDocument = flowDocument;

        public void Clear() => _flowDocument.Blocks.Clear();

        public void Replace(object? value, object range)
        {
            var textRange = (TextRange)range;
            textRange.Text = value != null ? value.ToString() : "";
        }

        public IEnumerable<object> Search(object value)
        {
            ArgumentNullException.ThrowIfNull(value, nameof(value));
            var pattern = value.ToString();
            var result = new List<TextRange>();
            foreach (var block in _flowDocument.Blocks)
            {
                ProcessBlock(block, pattern, result);
            }
            /**foreach(var range in result)
            {
                range.ApplyPropertyValue(TextElement.BackgroundProperty, Brushes.Yellow);
            }**/
            return result;
        }

        private void ProcessBlock(Block block, string pattern, IList result)
        {
            var navigator = block.ContentStart;
            while (navigator.CompareTo(block.ContentEnd) < 0)
            {
                if (navigator.GetPointerContext(LogicalDirection.Forward) == TextPointerContext.Text)
                {
                    var text = navigator.GetTextInRun(LogicalDirection.Forward);
                    var regex = new Regex(pattern, RegexOptions.IgnoreCase);
                    foreach (Match match in regex.Matches(text))
                    {
                        var start = navigator.GetPositionAtOffset(match.Index);
                        var end = start.GetPositionAtOffset(match.Length);
                        result.Add(new TextRange(start, end));
                    }
                }
                navigator = navigator.GetNextContextPosition(LogicalDirection.Forward);
            }
        }

        private FlowDocument CloneFlowDocument(FlowDocument originalDocument)
        {
            var newDocument = new FlowDocument();
            var originalContent = new TextRange(originalDocument.ContentStart,
                originalDocument.ContentEnd);
            var newContent = new TextRange(newDocument.ContentStart, newDocument.ContentEnd);
            using (var stream = new MemoryStream())
            {
                originalContent.Save(stream, DataFormats.Xaml);
                stream.Seek(0, SeekOrigin.Begin);
                newContent.Load(stream, DataFormats.Xaml);
            }
            newDocument.PageWidth = PageWidth;
            newDocument.PageHeight = PageHeight;
            return newDocument;
        }
    }
}
