using System;
using System.Collections;
using System.Linq;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;

using ViewModel.Interfaces.Services.Document;

namespace View.Implementations.Document
{
    public class DocumentService : IDocumentService
    {
        private static readonly IEnumerable _fontFamilies =
            Fonts.SystemFontFamilies.Select(s => s.Source);

        private static readonly IEnumerable _markerStyles =
            Enum.GetValues(typeof(TextMarkerStyle));

        private static readonly IEnumerable _horizontalTextAlignments =
            Enum.GetValues(typeof(TextAlignment));

        public IEnumerable FontFamilies => _fontFamilies;

        public IEnumerable MarkerStyles => _markerStyles;

        public IEnumerable HorizontalTextAlignments =>
            _horizontalTextAlignments;

        public ISelection GetSelection(object selection) =>
            new Selection((TextSelection)selection);

        public IDocument GetDocument(object document) =>
            new Document((FlowDocument)document);
    }
}
