using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Windows;
using System.Windows.Documents;

using View.Services;

using ViewModel.Interfaces.Services.Data;
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

        public IRange ContentRange => new Range(new TextRange
            (_flowDocument.ContentStart, _flowDocument.ContentEnd));

        public IRange StartRange => new Range(new TextRange
            (_flowDocument.ContentStart, _flowDocument.ContentStart));

        public IRange EndRange => new Range(new TextRange
            (_flowDocument.ContentEnd, _flowDocument.ContentEnd));

        public Document(FlowDocument flowDocument) => _flowDocument = flowDocument;

        public void Replace(object? value, IRange range)
        {
            var textRange = (Range)range;
            switch(value)
            {
                case null:
                    textRange.Text = "";
                    break;
                case string text:
                    textRange.Text = text;
                    break;
                case DataTable dataTable:
                    var table = DocumentEditorService.CreateTable(dataTable);
                    DocumentEditorService.SetBlock(table, textRange.Start, textRange.End);
                    break;
                case IEnumerable<object> list:
                    break;
                default:
                    throw new ArgumentException(nameof(value));
            }
        }

        public IEnumerable<IRange> Search(object pattern,
            IRange range, ISearchService searchService)
        {
            var textRange = (Range)range;
            var result = new List<Range>();
            var navigator = textRange.Start;
            while (navigator.CompareTo(textRange.End) < 0)
            {
                if (navigator.GetPointerContext(LogicalDirection.Forward) ==
                    TextPointerContext.Text)
                {
                    var text = navigator.GetTextInRun(LogicalDirection.Forward);
                    foreach (var match in searchService.GetMatches(text, pattern))
                    {
                        var start = navigator.GetPositionAtOffset(match.Index);
                        var end = start.GetPositionAtOffset(match.Length);
                        result.Add(new Range(new TextRange(start, end)));
                    }
                }
                navigator = navigator.GetNextContextPosition(LogicalDirection.Forward);
            }
            return result;
        }

        private FlowDocument CloneFlowDocument(FlowDocument originalDocument)
        {
            var newDocument = new FlowDocument();
            var originalContent = new TextRange(originalDocument.ContentStart,
                originalDocument.ContentEnd);
            var newContent = new TextRange(newDocument.ContentStart, newDocument.ContentEnd);
            using (var stream = new MemoryStream())
            {
                originalContent.Save(stream, DataFormats.XamlPackage);
                stream.Seek(0, SeekOrigin.Begin);
                newContent.Load(stream, DataFormats.XamlPackage);
            }
            newDocument.PageWidth = PageWidth;
            newDocument.PageHeight = PageHeight;
            newDocument.ColumnWidth = double.PositiveInfinity;
            return newDocument;
        }
    }
}
