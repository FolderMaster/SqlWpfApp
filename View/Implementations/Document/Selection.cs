﻿using System;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;

using Model.ObservableObjects;

using View.Services;

using ViewModel.Interfaces.Services.Document;

namespace View.Implementations.Document
{
    public class Selection : ObservableObject, ISelection
    {
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

        public IRange Range => new Range(new TextRange
            (_textSelection.Start, _textSelection.End));

        static Selection() { }

        public Selection(TextSelection textSelection)
        {
            _textSelection = textSelection;
            _textSelection.Changed += TextSelection_Changed;
            UpdateProperties();
        }

        public void InsertList(object markerStyle)
        {
            var list = DocumentEditorService.CreateList(1);
            list.MarkerStyle = (TextMarkerStyle)markerStyle;
            if (FontSize != null)
            {
                list.FontSize = (double)FontSize;
            }
            DocumentEditorService.SetBlock(list, _textSelection.Start, _textSelection.End);
        }

        public void InsertTable(int columnsCount, int rowsCount)
        {
            var table = DocumentEditorService.CreateTable(columnsCount, rowsCount);
            DocumentEditorService.SetBlock(table, _textSelection.Start, _textSelection.End);
        }

        public void InsertImage(object data)
        {
            var image = DocumentEditorService.CreateImage((ImageSource)data);
            DocumentEditorService.SetBlock(image, _textSelection.Start, _textSelection.End);
        }

        public void Select(IRange range)
        {
            var textRange = (Range)range;
            _textSelection.Select(textRange.Start, textRange.End);
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

        private void TextSelection_Changed(object? sender, EventArgs e) => UpdateProperties();
    }
}
