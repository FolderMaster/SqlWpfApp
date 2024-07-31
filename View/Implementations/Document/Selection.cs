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
            nameof(Bold), null, null, PropertyChanged);

        public static ObservableProperty ItalicProperty = RegisterProperty(typeof(Selection),
            nameof(Italic), null, null, PropertyChanged);

        public static ObservableProperty SizeProperty = RegisterProperty(typeof(Selection),
            nameof(Size), null, null, PropertyChanged);

        public static ObservableProperty AlignmentProperty = RegisterProperty(typeof(Selection),
            nameof(Alignment), null, null, PropertyChanged);


        private readonly TextSelection _textSelection;

        static Selection() { }

        public Selection(TextSelection textSelection)
        {
            _textSelection = textSelection;
            _textSelection.Changed += TextSelection_Changed;
            UpdateProperties();
        }

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

        public double? Size
        {
            get => (double?)GetValue(SizeProperty);
            set => SetValue(value, SizeProperty);
        }

        public object? Alignment
        {
            get => GetValue(AlignmentProperty);
            set => SetValue(value, AlignmentProperty);
        }

        public void CreateList()
        {
            var list = new List(new ListItem(new Paragraph()));
            _textSelection.Start.InsertParagraphBreak();
            _textSelection.Start.Paragraph.SiblingBlocks.
                InsertAfter(_textSelection.Start.Paragraph, list);
        }

        public void CreateTable(int columnsCount, int rowsCount)
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

        public void CreateImage(object data)
        {
            var imageSource = (ImageSource)data;
            var image = new Image()
            {
                Source = imageSource,
                Width = imageSource.Width,
                Height = imageSource.Height
            };
            var container = new BlockUIContainer(image);
            _textSelection.Start.InsertParagraphBreak();
            _textSelection.Start.Paragraph.SiblingBlocks.
                InsertAfter(_textSelection.Start.Paragraph, container);
        }

        protected virtual void UpdateProperties()
        {
            Bold = GetSelectionBold();
            Italic = GetSelectionItalic();
            Size = GetSelectionSize();
            Alignment = GetSelectionAlignment();
        }

        private static void PropertyChanged(ObservableArgs args)
        {
            var selection = (Selection)args.Owner;
            if (args.NewValue != null)
            {
                if (args.Property == BoldProperty)
                {
                    selection.SetSelectionBold((bool)args.NewValue);
                }
                else if (args.Property == ItalicProperty)
                {
                    selection.SetSelectionItalic((bool)args.NewValue);
                }
                else if (args.Property == AlignmentProperty)
                {
                    selection.SetSelectionAlignment(args.NewValue);
                }
                else if (args.Property == SizeProperty)
                {
                    selection.SetSelectionSize((double)args.NewValue);
                }
            }
        }

        private bool? GetSelectionBold()
        {
            if (_textSelection.GetPropertyValue(TextElement.FontWeightProperty) is
                FontWeight weight)
            {
                return weight == FontWeights.Bold;
            }
            return null;
        }

        private bool? GetSelectionItalic()
        {
            if (_textSelection.GetPropertyValue(TextElement.FontStyleProperty) is
                FontStyle style)
            {
                return style == FontStyles.Italic;
            }
            return null;
        }

        private double? GetSelectionSize()
        {
            if (_textSelection.GetPropertyValue(TextElement.FontSizeProperty) is
                double size)
            {
                return size;
            }
            return null;
        }

        private object? GetSelectionAlignment()
        {
            if (_textSelection.GetPropertyValue(Block.TextAlignmentProperty) is
                TextAlignment alignment)
            {
                return alignment;
            }
            return null;
        }

        private void SetSelectionBold(bool isBold)
        {
            _textSelection.ApplyPropertyValue(TextElement.FontWeightProperty,
                isBold ? FontWeights.Bold : FontWeights.Normal);
        }

        private void SetSelectionItalic(bool isItalic)
        {
            _textSelection.ApplyPropertyValue(TextElement.FontStyleProperty,
                isItalic ? FontStyles.Italic : FontStyles.Normal);
        }

        private void SetSelectionSize(double size)
        {
            _textSelection.ApplyPropertyValue(TextElement.FontSizeProperty, size);
        }

        private void SetSelectionAlignment(object alignment)
        {
            _textSelection.ApplyPropertyValue(Block.TextAlignmentProperty, alignment);
        }

        private void TextSelection_Changed(object? sender, EventArgs e) =>
            UpdateProperties();
    }
}
