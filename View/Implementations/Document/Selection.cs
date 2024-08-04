using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

using Model.ObservableObjects;

using ViewModel.Interfaces.Services.Document;

namespace View.Implementations.Document
{
    public class Selection : ObservableObject, ISelection
    {
        private const double _cellSpacing = 0;

        private static readonly Brush _borderBrush =
            new SolidColorBrush(Color.FromRgb(0, 0, 0));

        private static readonly Thickness _borderThickness = new Thickness(1);

        public static ObservableProperty BoldProperty = RegisterProperty(typeof(Selection),
            nameof(Bold), null, null, Property_Changed);

        public static ObservableProperty ItalicProperty = RegisterProperty(typeof(Selection),
            nameof(Italic), null, null, Property_Changed);

        public static ObservableProperty FontSizeProperty = RegisterProperty(typeof(Selection),
            nameof(FontSize), null, null, Property_Changed);

        public static ObservableProperty FontFamilyProperty = RegisterProperty(typeof(Selection),
            nameof(FontFamily), null, null, Property_Changed);

        public static ObservableProperty AlignmentProperty = RegisterProperty(typeof(Selection),
            nameof(Alignment), null, null, Property_Changed);

        private readonly TextSelection _textSelection;

        public bool? Bold
        {
            get => (bool?)GetValue(BoldProperty);
            set => SetValue(value, BoldProperty);
        }

        public bool? Italic
        {
            get => (bool?)GetValue(ItalicProperty);
            set => SetValue(value, ItalicProperty);
        }

        public double? FontSize
        {
            get => (double?)GetValue(FontSizeProperty);
            set => SetValue(value, FontSizeProperty);
        }

        public object? FontFamily
        {
            get => GetValue(FontFamilyProperty);
            set => SetValue(value, FontFamilyProperty);
        }

        public object? Alignment
        {
            get => GetValue(AlignmentProperty);
            set => SetValue(value, AlignmentProperty);
        }

        static Selection() { }

        public Selection(TextSelection textSelection)
        {
            _textSelection = textSelection;
            _textSelection.Changed += TextSelection_Changed;
            UpdateProperties();
        }

        public void InsertList(object markerStyle)
        {
            var list = new List(new ListItem(new Paragraph()));
            list.MarkerStyle = (TextMarkerStyle)markerStyle;
            if (FontSize != null)
            {
                list.FontSize = (double)FontSize;
            }
            _textSelection.Start.InsertParagraphBreak();
            _textSelection.Start.Paragraph.SiblingBlocks.
                InsertAfter(_textSelection.Start.Paragraph, list);
        }

        public void InsertTable(int columnsCount, int rowsCount)
        {
            var table = new Table();
            table.CellSpacing = _cellSpacing;
            for (int i = 0; i < columnsCount; i++)
            {
                table.Columns.Add(new TableColumn());
            }
            table.RowGroups.Add(new TableRowGroup());
            for (int i = 0; i < rowsCount; i++)
            {
                var row = new TableRow();
                for (int j = 0; j < columnsCount; j++)
                {
                    TableCell cell = new TableCell(new Paragraph())
                    {
                        BorderBrush = _borderBrush,
                        BorderThickness = _borderThickness
                    };
                    row.Cells.Add(cell);
                }
                table.RowGroups[0].Rows.Add(row);
            }
            _textSelection.Start.InsertParagraphBreak();
            _textSelection.Start.Paragraph.SiblingBlocks.
                InsertAfter(_textSelection.Start.Paragraph, table);
        }

        public void InsertImage(object data)
        {
            var imageSource = (ImageSource)data;
            var image = new Image()
            {
                Source = imageSource,
                Width = imageSource.Width,
                Height = imageSource.Height
            };
            var grid = new Grid();
            grid.Children.Add(image);
            grid.Children.Add(new AdornerDecorator());
            var container = new InlineUIContainer(grid);
            _textSelection.Start.Paragraph.Inlines.Add(container);
        }

        protected void SetSelectionValue(DependencyProperty property, object value) =>
            _textSelection.ApplyPropertyValue(property, value);

        protected object GetSelectionValue(DependencyProperty property) =>
            _textSelection.GetPropertyValue(property);

        protected virtual void UpdateProperties()
        {
            var weight = GetSelectionValue(TextElement.FontWeightProperty) as FontWeight?;
            Bold = null != weight ? FontWeights.Bold == weight : null;

            var style = GetSelectionValue(TextElement.FontStyleProperty) as FontStyle?;
            Italic = style != null ? FontStyles.Italic == style : null;

            FontSize = GetSelectionValue(TextElement.FontSizeProperty) as double?;

            var family = GetSelectionValue(TextElement.FontFamilyProperty) as FontFamily;
            FontFamily = family != null ? family.Source : null;

            Alignment = GetSelectionValue(Block.TextAlignmentProperty) as TextAlignment?;
        }

        private static void Property_Changed(ObservableArgs args)
        {
            var selection = (Selection)args.Owner;
            if (args.NewValue != null)
            {
                if (args.Property == BoldProperty)
                {
                    selection.SetSelectionValue(TextElement.FontWeightProperty,
                        (bool)args.NewValue ? FontWeights.Bold : FontWeights.Normal);
                }
                else if (args.Property == ItalicProperty)
                {
                    selection.SetSelectionValue(TextElement.FontStyleProperty,
                        (bool)args.NewValue ? FontStyles.Italic : FontStyles.Normal);
                }
                else if (args.Property == FontSizeProperty)
                {
                    selection.SetSelectionValue(TextElement.FontSizeProperty, args.NewValue);
                }
                else if (args.Property == FontFamilyProperty)
                {
                    selection.SetSelectionValue(TextElement.FontFamilyProperty, args.NewValue);
                }
                else if (args.Property == AlignmentProperty)
                {
                    selection.SetSelectionValue(Block.TextAlignmentProperty, args.NewValue);
                }
            }
        }

        protected AdornerLayer? GetAdornerLayerFromSelection()
        {
            var current = _textSelection.Start.Parent;
            while (current != null)
            {
                if (current is RichTextBox richTextBox)
                {
                    return AdornerLayer.GetAdornerLayer(richTextBox);
                }
                else if (current is FrameworkContentElement element)
                {
                    current = element.Parent;
                }
            }
            return null;
        }

        private void TextSelection_Changed(object? sender, EventArgs e) =>
            UpdateProperties();
    }
}
